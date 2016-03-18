using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace InputParserServiceLib.Tests
{
    [TestClass()]
    public class ParserInputServiceTests
    {
        private string filePath = "D:/MyGit/Practice/ConferenceTrackManager/ConferenceTrackManager/AppInputs/input.txt";

        [TestMethod()]
        public void HasContent()
        {
            Assert.IsTrue(File.ReadAllLines(filePath).Length > 0); 
        }

        [TestMethod()]
        public void CheckNoOfRecords()
        { 
            Assert.AreEqual(19, File.ReadAllLines(filePath).Length);
        }

        [TestMethod()]
        public void IsFileExists()
        {
            Assert.IsTrue(File.Exists(filePath));
        }

        [TestMethod()]
        public void ParseFileTest()
        {
            var obj = new ParserInputService();
            Assert.IsNotNull(filePath);
            var eventList = obj.ParseFile(filePath);
            Assert.IsNotNull(eventList); 
        } 
    }
}