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
            catch (Exception)
            {
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
        private bool disposedValue = false; // To detect redundant calls

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

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ConferenceTrackManagement() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }

    public class ConferenceTrackGenerator : IConferenceTrackGenerator
    {
        private DateTime getNewTrack { get; } = new DateTime(0001, 1, 1, 09, 0, 0);

        public List<ConferenceTrack> GenerateConferenceTrack(IEnumerable<ConferenceEvent> conferenceEvents)
        {
            DateTime dateTime = getNewTrack;
            List<ConferenceTrack> conferenceTrackList = new List<ConferenceTrack>();
            TimeSpan ts = new TimeSpan();
            ConferenceTrack conferenceTrack = new ConferenceTrack();
            DateTime dt = new DateTime();
            AddDefaultEvents(conferenceTrack);

            foreach (ConferenceEvent conferenceEvent in conferenceEvents)
            {
                CreateConferenceTrack(conferenceTrackList, ref conferenceTrack, conferenceEvent, ref dateTime, ref dt, ref ts);
            }

            conferenceTrackList.Add(conferenceTrack);
            return conferenceTrackList;
        }

        private void CreateConferenceTrack(List<ConferenceTrack> conferenceTrackList, ref ConferenceTrack conferenceTrack, ConferenceEvent conferenceEvent, ref DateTime dateTime, ref DateTime dt, ref TimeSpan ts)
        {
            bool flag = true;
            dt = dateTime.AddMinutes(conferenceEvent.duration);

            if (dateTime.Hour >= 9 && dt.Hour <= 12 && (dt.Hour == 12 ? (dt.Minute == 0 ? true : false) : true))
                conferenceEvent.startTime = dateTime;
            else
            {
                if (dt.Hour < 13)
                {
                    ts = new TimeSpan(13, 00, 0);
                    dateTime = dateTime.Date + ts;
                    dt = dateTime.AddMinutes(conferenceEvent.duration);
                }

                if (dateTime.Hour >= 13 && dt.Hour <= 17 && (dt.Hour == 17 ? (dt.Minute == 0 ? true : false) : true))
                    conferenceEvent.startTime = dateTime;
                else
                {
                    conferenceTrackList.Add(conferenceTrack);
                    conferenceTrack = new ConferenceTrack();
                    dateTime = getNewTrack;
                    AddDefaultEvents(conferenceTrack);
                    CreateConferenceTrack(conferenceTrackList, ref conferenceTrack, conferenceEvent, ref dateTime, ref dt, ref ts);
                    flag = false;
                }
            }

            if (flag)
            {
                dateTime = dt;
                conferenceTrack.conferenceEventList.Add(conferenceEvent);
            }
        }

        private void AddDefaultEvents(ConferenceTrack conferenceTrack)
        {
            conferenceTrack.conferenceEventList.Add(new ConferenceEvent()
            {
                startTime = new DateTime(0001, 1, 1, 12, 0, 0),
                title = AppConstants.lunchEvent
            });

            conferenceTrack.conferenceEventList.Add(new ConferenceEvent()
            {
                startTime = new DateTime(0001, 1, 1, 17, 0, 0),
                title = AppConstants.networkingEvent
            });
        }
    }
}