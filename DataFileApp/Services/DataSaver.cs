using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataFileApp.Models;


namespace DataFileApp.Services
{
    public class DataSaver : IDataSaver
    {
        public void SaveDataObjects(List<DataObject> dataObjects, string fileName)
        {
            var lines = new List<string>();

            foreach (var dataObject in dataObjects)
            {
                lines.Add($"[{dataObject.Title}]");
                foreach (var parameter in dataObject.Parameters)
                {
                    lines.Add($"{parameter.Key}={parameter.Value}");
                }
                lines.Add(string.Empty);
            }

            File.WriteAllLines(fileName, lines);
        }

        public void SaveValidDataObjects(List<DataObject> dataObjects, int partsCount)
        {
            var partSize = (int)Math.Ceiling(dataObjects.Count / (double)partsCount);

            for (int i = 0; i < partsCount; i++)
            {
                var partData = dataObjects.Skip(i * partSize).Take(partSize).ToList();
                SaveDataObjects(partData, $"base_{i + 1}.txt");
            }
        }
    }
}
