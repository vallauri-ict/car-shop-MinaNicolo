using System;

namespace venditaVeicoliDLLProject
{
    [Serializable()]
    public class moto:veicolo
    {
        private string marcaSella;
        public moto() : base("BMW","S1000RR",1000,0,new DateTime(),0,"blu",false,false){ MarcaSella = "Quintino"; }
        public moto(string marca, string modello, int cilindrata, int potenza, DateTime dataImm,
    int chilometriPercorsi, string colore, bool usato, bool kmZero,string MarcaSella) : base(marca, modello, cilindrata,
        potenza, dataImm, chilometriPercorsi, colore, usato, kmZero)
        {
            this.MarcaSella = MarcaSella;
        }
        public moto(string[] data) : base(data)
        {
            marcaSella = data[10];
        }
        public string MarcaSella { get => marcaSella; set => marcaSella = value; }

        public override string ToString()
        {
            return $"MOTO {base.ToString()} - marca sella:{MarcaSella}";
        }
    }
}
