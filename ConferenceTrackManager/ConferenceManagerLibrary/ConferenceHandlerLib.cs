using ConferenceModelLib;
using AppInterfacesLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace ConferenceHandlerLib
{
    public class InputParserService : IInputParserService
    {
        public IEnumerable<ConferenceEvent> parseFile(string filePath)
        {
            try
            {
                string[] arr = File.ReadAllLines(filePath);

                if (arr.Length == 0) throw new Exception("File is empty");

                var v2 = (from val in
                              (from temp in arr
                               select new
                               {
                                   t1 = temp.Substring(0, temp.LastIndexOf(' ')).Trim(),
                                   d1 = temp.Substring(temp.LastIndexOf(' ') + 1, temp.Length - temp.LastIndexOf(' ') - 1).ToLower()
                               })
                          select new ConferenceEvent
                          {
                              title = val.t1,
                              duration = val.d1.Contains("min") ? Convert.ToInt32(val.d1.Replace("min", "")) : (val.d1.Equals("lightning") ? 5 : 0)
                          }).ToList();
                return v2;
            }
            catch (FileNotFoundException ex)
            {
                throw ex;
            }
            catch (FormatException ex)
            {
                throw ex;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    public interface IInputParserService
    {
        IEnumerable<ConferenceEvent> parseFile(string path);
    }

    public class ConferenceTrackManagement : IConferenceTrackManager
    {
        IConferenceTrackGenerator conferenceTrackGenerator;
        IInputParserService inputParserService;
        IOutputService outputService;

        public ConferenceTrackManagement(IConferenceTrackGenerator conferenceTrackGenerator, IInputParserService inputParserService, IOutputService outputService)
        {
            this.conferenceTrackGenerator = conferenceTrackGenerator;
            this.inputParserService = inputParserService;
            this.outputService = outputService;
        }

        public IConferenceTrackGenerator ConferenceTrackGenerator
        {
            get
            {
                return conferenceTrackGenerator;
            }

            set
            {
                conferenceTrackGenerator = value;
            }
        }

        public IInputParserService InputParserService
        {
            get
            {
                return inputParserService;
            }

            set
            {
                inputParserService = value;
            }
        }

        public IOutputService OutputService
        {
            get
            {
                return outputService;
            }

            set
            {
                outputService = value;
            }
        }

        public void getConferenceTrack(string filePath)
        {
            IEnumerable<ConferenceEvent> conferenceEvents = inputParserService.parseFile(filePath);
            List<ConferenceTrack> conferenceTrackList = conferenceTrackGenerator.execute(conferenceEvents);

            for (int i = 0; i < conferenceTrackList.Count; i++)
            {
                outputService.writeMessage("* * * * * * * * * * * * * * ");
                outputService.writeMessage("\t Track. 0" + (i + 1));
                outputService.writeMessage("* * * * * * * * * * * * * * \n");
                var list = conferenceTrackList[i].conferenceEventList.OrderBy(x => x.startTime);
                foreach (ConferenceEvent conferenceEvent in list)
                {
                    outputService.writeMessage(conferenceEvent.startTime.ToString("t") + "\t" + conferenceEvent.title);
                }
                outputService.writeMessage("\n\n");
            }
        }
    }

    public class ConferenceTrackGenerator : IConferenceTrackGenerator
    {
        private DateTime getNewTrack
        {
            get
            {
                return new DateTime(0001, 1, 1, 09, 0, 0);
            }
        }

        public List<ConferenceTrack> execute(IEnumerable<ConferenceEvent> conferenceEvents)
        {
            DateTime dateTime = getNewTrack;
            List<ConferenceTrack> conferenceTrackList = new List<ConferenceTrack>();
            TimeSpan ts = new TimeSpan();
            ConferenceTrack conferenceTrack = new ConferenceTrack();
            conferenceTrack.conferenceEventList = new List<ConferenceEvent>();
            DateTime dt = new DateTime();
            scheduleDefalutEvents(conferenceTrack);

            foreach (ConferenceEvent conferenceEvent in conferenceEvents)
            {
                createConferenceTrack(ref dateTime, conferenceTrackList, ref ts, ref conferenceTrack, ref dt, conferenceEvent);
            }

            conferenceTrackList.Add(conferenceTrack);
            return conferenceTrackList;
        }

        private void createConferenceTrack(ref DateTime dateTime, List<ConferenceTrack> conferenceTrackList, ref TimeSpan ts, ref ConferenceTrack conferenceTrack, ref DateTime dt, ConferenceEvent conferenceEvent)
        {
            bool flag = true;
            dt = dateTime.AddMinutes(conferenceEvent.duration);

            // Go inside if time is in between less 9:00 AM to 12:00 PM 
            if (dateTime.Hour >= 9 && dt.Hour <= 12 && (dt.Hour == 12 ? (dt.Minute == 0 ? true : false) : true))
            {
                conferenceEvent.startTime = dateTime;
            }
            else
            {
                if (dt.Hour < 13)
                {
                    ts = new TimeSpan(13, 00, 0);
                    dateTime = dateTime.Date + ts;
                    dt = dateTime.AddMinutes(conferenceEvent.duration);
                }

                if (dateTime.Hour >= 13 && dt.Hour <= 17 && (dt.Hour == 17 ? (dt.Minute == 0 ? true : false) : true))
                {
                    conferenceEvent.startTime = dateTime;
                }
                else
                {
                    conferenceTrackList.Add(conferenceTrack);
                    conferenceTrack = new ConferenceTrack();
                    conferenceTrack.conferenceEventList = new List<ConferenceEvent>();
                    dateTime = getNewTrack;
                    scheduleDefalutEvents(conferenceTrack);
                    createConferenceTrack(ref dateTime, conferenceTrackList, ref ts, ref conferenceTrack, ref dt, conferenceEvent);
                    flag = false;
                }
            }

            if (flag)
            {
                dateTime = dt;
                conferenceTrack.conferenceEventList.Add(conferenceEvent);
            }
        }

        private void scheduleDefalutEvents(ConferenceTrack conferenceTrack)
        {
            //lunch Time
            conferenceTrack.conferenceEventList.Add(new ConferenceEvent()
            {
                startTime = new DateTime(0001, 1, 1, 12, 0, 0),
                title = "Lunch"
            });

            //Networking Event
            conferenceTrack.conferenceEventList.Add(new ConferenceEvent()
            {
                startTime = new DateTime(0001, 1, 1, 17, 0, 0),
                title = "Networking Event"
            });
        }
    }
}