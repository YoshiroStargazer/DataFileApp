using System.Collections.Generic;
using DataFileApp.Models;


namespace DataFileApp.Services
{
    public interface IDataLoader
    {
        List<DataObject> LoadDataObjects(string filePath);
    }
}
