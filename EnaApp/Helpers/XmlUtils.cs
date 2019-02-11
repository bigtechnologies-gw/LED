using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace EnaApp.Helpers
{
    public static class XmlUtils
    {
        private static XDocument _xDoc;

        static XmlUtils() // ensure file is created if not create a new one 
        {
            var appContext = MainActivity.AppContext;
            //File.Delete(Configs.DataFile);

            // delete file if it's been existing for more then 10 minutes
            //if(File.GetCreationTimeUtc(Configs.DataFile) < DateTime.UtcNow.AddMinutes(-5))
            //{
            //    File.Delete(Configs.DataFile);
            //}

            if (File.Exists(Configs.DataFile) == false)
            {
                _xDoc = new XDocument(
                  new XElement("Regions",
                      new XElement("Region",
                          new XElement("Name", StringUtils.CapitalizeFirstLetter(appContext.Region)),
                          new XElement("Sectors",
                              new XElement("Sector",
                                  new XElement("Name", appContext.Sector),
                                  new XElement("Tabancas"),
                                  new XElement("Communities")
                                  )))));

                _xDoc.Save(Configs.DataFile);
                //Log.WriteLine(...."file_saved");
            }
            else
            {
                _xDoc = XDocument.Load(Configs.DataFile);

                XElement region = _xDoc.Root.Elements("Region")
                .FirstOrDefault(el => el.Element("Name").Value.Equals(appContext.Region, System.StringComparison.OrdinalIgnoreCase));

                // region not created yet!`
                if (region == null)
                {
                    region = new XElement("Region",
                        new XElement("Name", StringUtils.CapitalizeFirstLetter(appContext.Region)),
                        new XElement("Sectors",
                            new XElement("Sector",
                                new XElement("Name", appContext.Sector),
                                new XElement("Tabancas"),
                                new XElement("Communities"))));
                    _xDoc.Element("Regions").Add(region);
                    Save();
                }

                EnsureSector(MainActivity.AppContext.Sector, region);
            }
        }

        private static XElement EnsureSector(string sectorName, XElement region)
        {
            var sector = region.Element("Sectors")
                .Elements("Sector")
                .FirstOrDefault(el => el.Element("Name")?.Value.Equals(sectorName,
                System.StringComparison.OrdinalIgnoreCase) == true);

            if (sector == null)
            {
                sector = new XElement("Sector",
                   new XElement("Name", sectorName),
                   new XElement("Tabancas"),
                   new XElement("Communities")
                   );
                region.Element("Sectors").Add(sector);
                Save();

            }
            return sector;
        }

        public static void Ensure()
        {
            // ignore. ps: constructor will handle everything
        }

        public static XElement GetRootSector()
        {
            _xDoc = _xDoc ?? XDocument.Load(Configs.DataFile);
            var appContext = MainActivity.AppContext;

            const System.StringComparison sc = StringComparison.OrdinalIgnoreCase;
            XElement region = _xDoc.Root.Elements("Region")
                .FirstOrDefault(el => el.Element("Name").Value.Equals(appContext.Region, sc));
            return EnsureSector(MainActivity.AppContext.Sector, region);
        }

        public static void Save()
        {
            _xDoc?.Save(Configs.DataFile);
        }
    }
}