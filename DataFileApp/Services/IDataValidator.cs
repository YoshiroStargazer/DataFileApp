using System.Collections.Generic;
using DataFileApp.Models;


namespace DataFileApp.Services
{
    public interface IDataValidator
    {
        (List<DataObject> validData, List<DataObject> invalidData) ValidateDataObjects(List<DataObject> dataObjects);
    }
}
