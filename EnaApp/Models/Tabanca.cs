using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace EnaApp
{
    public class Tabanca
    {
        private readonly XElement el;

        public Tabanca()
        {
            ID = Guid.NewGuid().ToString();
        }

        public Tabanca(XElement el)
        {
            this.el = el;
        }

        /// <summary>
        /// Tabanca ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Id of community this tabancans belongs to.
        /// </summary>
        public string CommunityID { get; set; }

        public string Name { get; set; }

        // perto da floreast...
        public IList<string> Location { get; set; }


        // Ponto da uniao
        public decimal DistanceToSection { get; set; }
        public decimal DistanceToSector { get; set; }
        public decimal DistanceToRegion { get; set; }
        public decimal DistanceToBissau { get; set; }

        public string FounderName { get; set; }

        // Period de fundacao da tabanca em ano
        public string TabancaCreation { get; set; } // antes da colonizacao, durante, depois

        /// <summary>
        /// Tabanca de origem
        /// </summary>
        public string NativeTabanca { get; set; }

        // etnica de fundador
        public string FounderEthnic { get; set; }

        // Agricultura, caca, pesca...
        public List<string> FoudingReasons { get; set; }

        public List<string> PastIssues { get; set; }
        public List<string> CurrentIssues { get; set; }

        public IList<string> MainEthnicGroupsInTabanca { get; set; } // todo: fix in view from community to tabanca


        // TODO: handle cause when there is comflict
        public IList<string> RalationBetweenEthicGroups { get; set; }

        /// <summary>
        /// Name of current representatives in tabanca...
        /// </summary>
        public List<string> CurrentTabancaRepresentatives { get; set; }

        // LOCAL GORVERNATION
        public IList<string> PublicServices { get; set; } // TODO: IMPROVE THE MODEL

        /// <summary>
        /// Como voce avalia a cooperacao e coordenacao entre od departamentos de estado e os atores locais, como o governo local
        /// e o poder tradicional, a sociedade civil, o sector privado e outros
        /// </summary>
        public string Relations { get; set; }

        public string StateHelpInDecisions { get; set; }

        /// <summary>
        /// Serialize and return value of tabanca.
        /// </summary>
        /// <returns></returns>
        public XElement ToXElement()
        {
            // get only properties that have a value.
            var propsWithValue = this.GetType().GetProperties().Where(p => p.GetValue(this) != null);

            XElement tabanca = new XElement("Tabanca");

            // filter non-generic type first
            foreach (PropertyInfo propInfo in propsWithValue.Where( p => p.PropertyType != typeof(IList<string>) && p.PropertyType != typeof(List<string>)))
            {
                tabanca.Add(new XElement(propInfo.Name, propInfo.GetValue(this)));
            }

            // generic value
            foreach (PropertyInfo propInfo in propsWithValue.Where( p => p.PropertyType.IsGenericType))
            {
                IList<string> listItem = (IList<string>)propInfo.GetValue(this);
                XElement xEl = new XElement(propInfo.Name);
                foreach (string item in listItem)
                {
                    // TODO: Xml escape
                    xEl.Add(new XElement("Item", item));
                }
                tabanca.Add(xEl);
            }

            return tabanca;
        }

        public override string ToString() => Name;
    }
}