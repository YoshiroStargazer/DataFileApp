using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using DataFileApp.Models;
using DataFileApp.Services;


namespace DataFileApp.Services
{
    public class DataLoader : IDataLoader
    {
        public List<DataObject> LoadDataObjects(string filePath)
        {
            var dataObjects = new List<DataObject>();
            var lines = File.ReadAllLines(filePath);
            DataObject currentObject = null;
            var parameterRegex = new Regex(@"^(?<key>\w+)=(?<value>.+)$");

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    if (currentObject != null)
                    {
                        dataObjects.Add(currentObject);
                        currentObject = null;
                    }
                    continue;
                }

                if (line.StartsWith("[") && line.EndsWith("]"))
                {
                    if (currentObject != null)
                    {
                        dataObjects.Add(currentObject);
                    }
                    currentObject = new DataObject { Title = line.Trim('[', ']') };
                }
                else
                {
                    if (currentObject == null)
                    {
                        Console.WriteLine("Ошибка: некорректный формат данных.");
                        return null;
                    }

                    var match = parameterRegex.Match(line);
                    if (!match.Success)
                    {
                        Console.WriteLine("Ошибка: некорректный формат параметра.");
                        return null;
                    }

                    var key = match.Groups["key"].Value;
                    var value = match.Groups["value"].Value;
                    currentObject.Parameters[key] = value;
                }
            }

            if (currentObject != null)
            {
                dataObjects.Add(currentObject);
            }

            return dataObjects;
        }
    }
}
