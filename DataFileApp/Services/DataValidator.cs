using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataFileApp.Models;
using DataFileApp.Services;


namespace DataProcessingApp.Services
{
    public class DataValidator : IDataValidator
    {
        public (List<DataObject> validData, List<DataObject> invalidData) ValidateDataObjects(List<DataObject> dataObjects)
        {
            var validData = new List<DataObject>();
            var invalidData = new List<DataObject>();
            var invalidPathChars = Path.GetInvalidPathChars();

            foreach (var dataObject in dataObjects)
            {
                if (!dataObject.Parameters.ContainsKey("Connect"))
                {
                    invalidData.Add(dataObject);
                    continue;
                }

                var connectValue = dataObject.Parameters["Connect"];
                if (connectValue.StartsWith("File=\""))
                {
                    var path = connectValue.Substring(6, connectValue.Length - 7);
                    if (path.IndexOfAny(invalidPathChars) >= 0)
                    {
                        invalidData.Add(dataObject);
                    }
                    else
                    {
                        validData.Add(dataObject);
                    }
                }
                else if (connectValue.StartsWith("Srvr=\""))
                {
                    var parts = connectValue.Split(';');
                    var server = parts.FirstOrDefault(p => p.StartsWith("Srvr="))?.Substring(6);
                    var database = parts.FirstOrDefault(p => p.StartsWith("Ref="))?.Substring(5);
                    if (string.IsNullOrEmpty(server) || string.IsNullOrEmpty(database))
                    {
                        invalidData.Add(dataObject);
                    }
                    else
                    {
                        validData.Add(dataObject);
                    }
                }
                else
                {
                    invalidData.Add(dataObject);
                }
            }

            return (validData, invalidData);
        }
    }
}
