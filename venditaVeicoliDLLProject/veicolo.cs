using System;
using System.Xml.Serialization;

namespace venditaVeicoliDLLProject
{
    [Serializable()]
    [XmlInclude(typeof(auto))]
    [XmlInclude(typeof(moto))]
    public abstract class veicolo
    {
        #region fields
        private string marca;
        private string modello;
        private int cilindrata;
        private double potenzaKw;
        private DateTime immatricolazione;
        private int kmPercorsi;
        private string colore;
        private bool isUsato;
        private bool isKmZero;
        private int prezzo;
        #endregion

        public veicolo() { }
        public string ImagePath { get; set; }
        public veicolo(string marca, string modello, int cilindrata, double potenzaKw, DateTime immatricolazione, int kmPercorsi, string colore, bool isUsato, bool isKmZero, int prezzo)
        {
            this.Marca = marca;
            this.Modello = modello;
            this.Cilindrata = cilindrata;
            this.PotenzaKw = potenzaKw;
            this.Immatricolazione = immatricolazione;
            this.KmPercorsi = kmPercorsi;
            this.Colore = colore;
            this.IsUsato = isUsato;
            this.IsKmZero = isKmZero;
            this.Prezzo = prezzo;
        }

        public veicolo(string[] data)
        {

            this.Marca = data[1];
            this.Modello = data[2];
            this.Cilindrata = int.Parse(data[3]);
            this.PotenzaKw = int.Parse(data[4]);
            this.Immatricolazione = Convert.ToDateTime(data[5]);
            this.KmPercorsi = int.Parse(data[6]);
            this.Colore = data[7];
            this.IsUsato = Convert.ToBoolean(data[8]);
            this.IsKmZero = Convert.ToBoolean(data[9]);
            this.Prezzo = Convert.ToInt32(data[11]);
        }

        public string Marca { get => marca; set => marca = value; }
        public string Modello { get => modello; set => modello = value; }
        public int Cilindrata { get => cilindrata; set => cilindrata = value; }
        public double PotenzaKw { get => potenzaKw; set => potenzaKw = value; }
        public DateTime Immatricolazione { get => immatricolazione; set => immatricolazione = value; }
        public int KmPercorsi { get => kmPercorsi; set => kmPercorsi = value; }
        public string Colore { get => colore; set => colore = value; }
        public bool IsUsato { get => isUsato; set => isUsato = value; }
        public bool IsKmZero { get => isKmZero; set => isKmZero = value; }
        public int Prezzo { get => prezzo; set => prezzo = value; }

        public override string ToString()
        {
            return $": marca: {Marca} - Modello {Modello} Colore({Colore})";
        }

    }
}
