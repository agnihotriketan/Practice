using ConferenceModels;
using System;
using System.Collections.Generic;

namespace AppInterfaces
{
    public interface IConferenceTrackGenerator
    {
        List<ConferenceTrack> GenerateConferenceTrack(IEnumerable<ConferenceEvent> conferenceEvent);
    }

    public interface IConferenceTrackManager : IDisposable
    {
        void GetConferenceTrack(string filePath);
    } 

    public interface IOutputWriterService
    {
        void WriteMessage(string message);
    }

    public interface IParserInputService
    {
        IEnumerable<ConferenceEvent> ParseFile(string filePath);
    }
}