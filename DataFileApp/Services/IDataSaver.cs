using System.Collections.Generic;
using DataFileApp.Models;


namespace DataFileApp.Services
{
    public interface IDataSaver
    {
        void SaveDataObjects(List<DataObject> dataObjects, string fileName);
        void SaveValidDataObjects(List<DataObject> dataObjects, int partsCount);
    }
}
