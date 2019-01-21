using System.Collections.Generic;

namespace EnaApp
{
    public class AppContext
    {
        public string Region { get; set; }
        public string Sector { get; set; }

        public List<Community> Communities { get; set; }

        public AppContext()
        {
            Communities = new List<Community>();

            Communities.Add(new Community());
        }
    }
}