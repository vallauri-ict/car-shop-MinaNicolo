using ReflectionCssPocProject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using venditaVeicoliDLLProject;
using static ReflectionCssPocProject.Utils;

namespace winFormProject
{
    public partial class frmMain : Form
    {
        SerializableBindingList<veicolo> listVeicolo;
        public frmMain()
        {
            listVeicolo = new SerializableBindingList<veicolo>();
            InitializeComponent();
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            CaricaDatiDiTesto();
        }

        private void CaricaDatiDiTesto()
        {
            moto m = new moto();
            m = new moto("Ducati", "Panigale V4R", 1000, 75, DateTime.Now, 0, "blu", false, false, "StandarCO");
            listVeicolo.Add(m);
            auto a = new auto("Alfa Romeo", "Stelvio", 2000, 150, DateTime.Now, 0, "rosso", false, false, 8);
            listVeicolo.Add(a);
            lbVeicoli.DataSource = listVeicolo;
        } 

        private void toolStripBtnAddVeicolo_Click_1(object sender, EventArgs e)
        {
            AggiungiVeicoloDialog frmDialog = new AggiungiVeicoloDialog(listVeicolo);
            frmDialog.ShowDialog();
        }

        private void apriToolStripButton_Click(object sender, EventArgs e)
        {
            //string[] data;
            //StreamReader sr = new StreamReader("veicoli.dat");
            //listVeicolo.Clear();
            //while (!sr.EndOfStream)
            //{
            //    data = sr.ReadLine().Split('|');
            //    if (data[0]=="AUTO")
            //    {
            //        listVeicolo.Add(new auto(data));
            //    }
            //    else
            //    {
            //        listVeicolo.Add(new moto(data));
            //    }
            //}

            //sr.Close();
        }

        private void salvaToolStripButton_Click(object sender, EventArgs e)
        {
            Utils.SerializeToCsv(listVeicolo, "./veicolo.csv");
            Utils.SerializeToXml(listVeicolo,"./veicolo.xml");
            Utils.SerializeToJson(listVeicolo,"./veicolo.json");
            //string toWrite = null;
            //StreamWriter sw = new StreamWriter("veicoli.dat");

            //foreach (var item in listVeicolo)
            //{
            //    toWrite = null;
            //    if (item is auto)
            //    {   
            //        toWrite = $"AUTO|{item.Marca}|{item.Modello}|{item.Cilindrata}|{item.PotenzaKw}|{item.Immatricolazione.ToShortDateString()}|{item.KmPercorsi}|{item.Colore}|{item.IsUsato}|{item.IsKmZero}|{(item as auto).NumairBag}";
            //    }
            //    else
            //    {
            //        toWrite = $"MOTO|{item.Marca}|{item.Modello}|{item.Cilindrata}|{item.PotenzaKw}|{item.Immatricolazione.ToShortDateString()}|{item.KmPercorsi}|{item.Colore}|{item.IsUsato}|{item.IsKmZero}|{(item as moto).MarcaSella}";
            //    }
            //    sw.WriteLine(toWrite);
            //}
            //sw.Close();
        }

        private void tlsBtnCaricaOnline_Click(object sender, EventArgs e)
        {
            string homePath = @".\www.\index.html";
            Utils.createHTML(listVeicolo,homePath);
            System.Diagnostics.Process.Start(homePath);
        }
    }
}
