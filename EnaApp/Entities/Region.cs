using System.Collections.Generic;

namespace EnaApp.Entities
{

    public class Region
    {
        public Region(string name, List<string> sectors)
        {
            Name = name;
            Sectors = sectors;
        }

        /// <summary>
        /// Name of the region.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Region's sectors.
        /// </summary>
        public List<string> Sectors { set; get; }
    }
}