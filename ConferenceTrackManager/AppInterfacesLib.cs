using ConferenceModelLib;
using System.Collections.Generic;

namespace AppInterfacesLib
{
    public interface IConferenceTrackGenerator
    {
        List<ConferenceTrack> execute(IEnumerable<ConferenceEvent> conferenceEvent);
    }

    public interface IConferenceTrackManager
    {
        void getConferenceTrack(string filePath);
    } 

    public interface IOutputService
    {
        void writeMessage(string message);
    }
}