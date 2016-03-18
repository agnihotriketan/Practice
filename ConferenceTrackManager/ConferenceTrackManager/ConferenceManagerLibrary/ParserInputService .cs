using ConferenceModels;
using AppInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OutputWriter;

namespace InputParserServiceLib
{
    public class ParserInputService : IParserInputService
    {
        private OutputWriterService outputService;

        public IEnumerable<ConferenceEvent> ParseFile(string filePath)
        {
            try
            {
                string[] rawTrackArray = File.ReadAllLines(filePath);

                if (rawTrackArray.Length == 0)
                {
                    outputService = new OutputWriterService();
                    outputService.LogExceptions("File is empty -- " + DateTime.UtcNow.ToLocalTime());
                    throw new Exception("File is empty");
                }

                var parsedDataList = (from val in
                                          (from temp in rawTrackArray
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
                return parsedDataList;
            }
            catch (FileNotFoundException ex) when (ex.Message.ToLower().Contains("could not find file"))
            {
                outputService = new OutputWriterService();
                outputService.LogExceptions(ex.Message + " -- " + DateTime.UtcNow.ToLocalTime());
                throw ex;
            }
            catch (FormatException ex)
            {
                outputService = new OutputWriterService();
                outputService.LogExceptions(ex.Message + " -- " + DateTime.UtcNow.ToLocalTime());
                throw ex;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                outputService = new OutputWriterService();
                outputService.LogExceptions(ex.Message + " -- " + DateTime.UtcNow.ToLocalTime());
                throw ex;
            }
            catch (Exception ex)
            {
                outputService = new OutputWriterService();
                outputService.LogExceptions(ex.Message + " -- " + DateTime.UtcNow.ToLocalTime());
                throw ex;
            }
        }
    }
}