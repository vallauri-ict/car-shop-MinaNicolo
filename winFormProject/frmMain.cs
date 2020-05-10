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
            m = new moto("Ducati", "Panigale V4R", 1000, 75, DateTime.Now, 0, "blu", false, false, 11300, "StandarCO");
            listVeicolo.Add(m);
            auto a = new auto("Alfa Romeo", "Stelvio", 2000, 150, DateTime.Now, 0, "rosso", false, false, 10000, 8);
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
            try
            {
                Utils.SerializeToCsv(listVeicolo, "./veicolo.csv");
                Utils.SerializeToXml(listVeicolo, "./veicolo.xml");
                Utils.SerializeToJson(listVeicolo, "./veicolo.json");
                MessageBox.Show("Salvato correttamente");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
           
        }

        private void tlsBtnCaricaOnline_Click(object sender, EventArgs e)
        {
            string homePath = @".\www.\index.html";
            Utils.createHTML(listVeicolo,homePath);
            System.Diagnostics.Process.Start(homePath);
        }
    }
}
