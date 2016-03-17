using System;
using System.Collections.Generic;

namespace ConferenceModels
{
    public class ConferenceEvent
    {
        public int duration { get; set; }
        public string title { get; set; }
        public DateTime startTime { get; set; }
    }

    public class ConferenceTrack
    {
        public List<ConferenceEvent> conferenceEventList { get; set; }= new List<ConferenceEvent>();
    }
}