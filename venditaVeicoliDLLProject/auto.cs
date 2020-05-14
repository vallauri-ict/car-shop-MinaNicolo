using System;
using System.Collections.Generic;
using System.Text;

namespace venditaVeicoliDLLProject
{
    [Serializable()]
    public class auto:veicolo
    {
        private int numairBag;
        public auto() : base("BMW", "Z4", 2000, 200, new DateTime(), 0, "nero", false, false,12360) { NumairBag = 6; }
        public auto(string marca, string modello, int cilindrata, int potenza, DateTime dataImm,
            int chilometriPercorsi, string colore, bool usato, bool kmZero, int prezzo, int numairBag) : base(marca, modello, cilindrata,
                potenza, dataImm, chilometriPercorsi, colore, usato, kmZero, prezzo)
        {
            this.numairBag=numairBag;
        }
        public int NumairBag { get => numairBag; set => numairBag = value; }

        public override string ToString()
        {
            return $"AUTO {base.ToString()} - {this.NumairBag} Aribag";
        }
    }
}
