using ConferenceModels;
using AppInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace InputParserServiceLib
{
    public class ParserInputService : IParserInputService
    {
        public IEnumerable<ConferenceEvent> ParseFile(string filePath)
        {
            try
            {
                string[] arr = File.ReadAllLines(filePath);

                if (arr.Length == 0) throw new Exception("File is empty");

                var parsedDataList = (from val in
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
                return parsedDataList;
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
}