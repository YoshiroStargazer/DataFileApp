using System;
using System.Collections.Generic;
using System.IO;
using DataFileApp.Services;
using DataFileApp.Models;
using DataProcessingApp.Services;

namespace DataFileApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Ошибка: укажите путь к файлу в качестве аргумента командной строки.");
                Environment.Exit(-1);
            }

            string filePath = args[0];

            if (string.IsNullOrEmpty(filePath))
            {
                Console.WriteLine("Ошибка: путь к файлу не может быть пустым.");
                Environment.Exit(-1);
            }

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Ошибка: файл '{filePath}' не найден.");
                Environment.Exit(-1);
            }

            IDataLoader dataLoader = new DataLoader();
            var dataObjects = dataLoader.LoadDataObjects(filePath);
            if (dataObjects == null)
            {
                Environment.Exit(-1);
            }

            IDataValidator dataValidator = new DataValidator();
            var (validData, invalidData) = dataValidator.ValidateDataObjects(dataObjects);

            IDataSaver dataSaver = new DataSaver();
            dataSaver.SaveDataObjects(invalidData, "bad_data.txt");
            dataSaver.SaveValidDataObjects(validData, 5);

            Console.WriteLine("Обработка завершена.");
        }
    }
}
