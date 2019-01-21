using System;
using System.Collections.Generic;

namespace EnaApp
{
    public class Tabanca
    {
        public Guid Id { get; set; }
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

        public override string ToString() => Name;
    }
}