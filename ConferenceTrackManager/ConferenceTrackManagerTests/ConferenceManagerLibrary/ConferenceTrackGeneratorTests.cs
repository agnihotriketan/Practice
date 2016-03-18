using ConferenceModels;
using InputParserServiceLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ConferenceTrackHandler.Tests
{
    [TestClass()]
    public class ConferenceTrackGeneratorTests
    {
        private string filePath = "D:/MyGit/Practice/ConferenceTrackManager/ConferenceTrackManager/AppInputs/input.txt";

        List<ConferenceTrack> conferenceTrackList;
        private ConferenceTrackGenerator trackGenerator = new ConferenceTrackGenerator();
        private ParserInputService parserService = new ParserInputService();

        public ConferenceTrackGeneratorTests()
        {
            conferenceTrackList = trackGenerator.GenerateConferenceTrack(parserService.ParseFile(filePath));
        }

        [TestMethod()]
        public void GenerateConferenceTrackTest()
        {
            Assert.IsNotNull(conferenceTrackList);
            Assert.IsTrue(conferenceTrackList.Count == 2);

            Assert.IsNotNull(conferenceTrackList[0].conferenceEventList);
            Assert.IsTrue(conferenceTrackList[0].conferenceEventList.Count == 12);

            Assert.IsNotNull(conferenceTrackList[1].conferenceEventList);
            Assert.IsTrue(conferenceTrackList[1].conferenceEventList.Count == 11); 
        }
    }
}