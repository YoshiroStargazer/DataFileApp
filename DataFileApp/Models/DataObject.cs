using System.Collections.Generic;

namespace DataFileApp.Models
{
    public class DataObject
    {
        public string Title { get; set; }
        public Dictionary<string, string> Parameters { get; } = new Dictionary<string, string>();
    }
}
