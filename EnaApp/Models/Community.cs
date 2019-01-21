using System;
using System.Collections.Generic;

namespace EnaApp
{
    public class Community
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public List<Tabanca> Tabancas { get; set; } = new List<Tabanca>();

        public Community()
        {
            ID = Guid.NewGuid().ToString();
            GenerateTabancas();
        }

        private void GenerateTabancas()
        {
            Tabancas = new List<Tabanca>()
            {
                new Tabanca { Name = "Tabanca (Ivandro)"},
                new Tabanca { Name = "Tabanca (Ismael)"},
                new Tabanca { Name = "Tabanca (Gomes)"},
            };
        }

        public void Add(Tabanca tabanca) => Tabancas.Add(tabanca);

        public override string ToString() => Name;
    }
}