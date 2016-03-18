using ConferenceModels;
using AppInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using ConferenceManager.AppConstants;

namespace ConferenceTrackHandler
{
    public class ConferenceTrackManager : IConferenceTrackManager, IDisposable
    {
        private IConferenceTrackGenerator conferenceTrackGenerator;
        private IParserInputService parserInputService;
        private IOutputWriterService outputService;

        public ConferenceTrackManager(IConferenceTrackGenerator _conferenceTrackGenerator, IParserInputService _parserInputService, IOutputWriterService _outputService)
        {
            conferenceTrackGenerator = _conferenceTrackGenerator;
            parserInputService = _parserInputService;
            outputService = _outputService;
        }

        public void GetConferenceTrack(string filePath)
        {
            try
            {
                IEnumerable<ConferenceEvent> conferenceEvents = parserInputService.ParseFile(filePath);
                List<ConferenceTrack> conferenceTrackList = conferenceTrackGenerator.GenerateConferenceTrack(conferenceEvents);

                var trackCount = conferenceTrackList?.Count;
                for (int i = 0; i < trackCount; i++)
                {
                    outputService.WriteMessage(AppConstants.outputDecorator + "\t Track. 0" + (i + 1) + AppConstants.outputDecorator);
                    DisplaySingleTrack(conferenceTrackList, i);
                } 
            }
            catch (Exception ex)
            {
                outputService.LogExceptions(ex.Message + " -- " + DateTime.UtcNow.ToLocalTime());
                throw;
            }
        }

        private void DisplaySingleTrack(List<ConferenceTrack> conferenceTrackList, int i)
        {
            var list = conferenceTrackList[i]?.conferenceEventList?.OrderBy(x => x.startTime);
            foreach (ConferenceEvent conferenceEvent in list)
            {
                outputService.WriteMessage(conferenceEvent?.startTime.ToString("t") + "\t" + conferenceEvent.title);
            }
            outputService.WriteMessage("\n\n");
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls while disposing

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // ~ConferenceTrackManager() { 
        //   Dispose(false);
        // }

        void IDisposable.Dispose()
        { 
            Dispose(true); 
            // GC.SuppressFinalize(this);
        }
        #endregion
    }

   
}