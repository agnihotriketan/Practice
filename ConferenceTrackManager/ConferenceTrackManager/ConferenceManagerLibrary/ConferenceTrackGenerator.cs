using ConferenceModels;
using AppInterfaces;
using System;
using System.Collections.Generic;
using ConferenceManager.AppConstants;

namespace ConferenceTrackHandler
{
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