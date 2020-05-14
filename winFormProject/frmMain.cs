using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
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
using OpenXmlUtilities;
using DocumentFormat.OpenXml.Wordprocessing;

namespace winFormProject
{
    public partial class frmMain : Form
    {
        SerializableBindingList<veicolo> listVeicolo;
        clsOpenXmlWordUtilities word = new clsOpenXmlWordUtilities();
        clsOpenXmlExcelUtilities excel = new clsOpenXmlExcelUtilities();
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
            Utils.createHTML(listVeicolo, homePath);
            System.Diagnostics.Process.Start(homePath);
        }

        private void ToolStripButtonExcel_Click(object sender, EventArgs e)
        {

            try
            {
                string filePath = excel.OutputFileName(excel.SelectPath(folderBrowserDialog), "xlsx");
                List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
                for (int i = 0; i < listVeicolo.Count; i++)
                {
                    string usato = listVeicolo[i].IsUsato ? "Si" : "No";
                    string kmZero = listVeicolo[i].IsKmZero ? "Si" : "No";
                    Dictionary<string, string> excelContent = new Dictionary<string, string>();

                    excelContent.Add("Marca", listVeicolo[i].Marca);
                    excelContent.Add("Modello", listVeicolo[i].Modello);
                    excelContent.Add("Colore", listVeicolo[i].Colore);
                    excelContent.Add("Cilindrata", listVeicolo[i].Cilindrata.ToString());
                    excelContent.Add("Potenza", listVeicolo[i].PotenzaKw.ToString() + " kw");
                    excelContent.Add("Immatricolazione", listVeicolo[i].Immatricolazione.ToShortDateString());
                    excelContent.Add("Usato", usato);
                    excelContent.Add("Km Zero", kmZero);
                    excelContent.Add("Km Percorsi", listVeicolo[i].KmPercorsi.ToString());
                    excelContent.Add("Prezzo", listVeicolo[i].Prezzo.ToString() + " €");
                    if ((listVeicolo[i] is auto)) excelContent.Add("Numero Airbag/Marca sella", (listVeicolo[i] as auto).NumairBag.ToString());
                    else excelContent.Add("Numero Airbag/Marca sella", (listVeicolo[i] as moto).MarcaSella);
                    list.Add(excelContent);
                }
                using (SpreadsheetDocument package = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook))
                {

                    clsOpenXmlExcelUtilities.CreatePartsForExcel(package, list);

                    MessageBox.Show("Il documento excel è pronto!");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Problemi con il documento!!!");
            }
        }

        private void toolStripButtonWord_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = word.OutputFileName(word.SelectPath(folderBrowserDialog), "docx");

                using (WordprocessingDocument doc = WordprocessingDocument.Create(filePath, WordprocessingDocumentType.Document))
                {
                    MainDocumentPart mainPart = doc.AddMainDocumentPart();

                    mainPart.Document = new Document();
                    Body body = mainPart.Document.AppendChild(new Body());

                    clsOpenXmlWordUtilities.AddStyle(mainPart, true, true, true, false, "MyHeading1", "Title", "Verdana", 16, "000000");
                    clsOpenXmlWordUtilities.AddStyle(mainPart, true, false, false, false, "MyHeading2", "Subtitle", "Verdana", 14, "000000");

                    AddParagraph(body, "MyHeading1", "SALONE AUTOVALLAURI", JustificationValues.Center);
                    AddParagraph(body, "MyHeading2", "Veicoli", JustificationValues.Center);

                    clsOpenXmlWordUtilities.CreateBulletNumberingPart(mainPart, "•");
                    for (int i = 0; i < listVeicolo.Count; i++)
                    {
                        AddParagraph(body, "MyParagraph2", $"{listVeicolo[i].Marca} {listVeicolo[i].Modello}");
                        string usato = listVeicolo[i].IsUsato ? "Si" : "No";
                        string kmZero = listVeicolo[i].IsKmZero ? "Si" : "No";
                        string[] elements = {
                            $"Immatricolazione: {listVeicolo[i].Immatricolazione.ToShortDateString()}",
                            $"Colore: {listVeicolo[i].Colore}",
                            $"Cilindrata: {listVeicolo[i].Cilindrata}", 
                            $"Potenza: {listVeicolo[i].PotenzaKw} Kw",
                            $"Usato: {usato}", $"Km zero: {kmZero}", 
                            $"Km Percorsi: {listVeicolo[i].KmPercorsi}",
                            $"Prezzo: {listVeicolo[i].Prezzo} €"
                        };
                        List<Paragraph> bulletList = new List<Paragraph>();
                        
                        word.CreateBulletOrNumberedList(100, 200, bulletList, elements);
                        foreach (Paragraph paragraph in bulletList)
                            body.Append(paragraph);
                    }
                    MessageBox.Show("Il documento word è pronto!");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Problemi con il documento!!!");
            }
        }
        private void AddParagraph(Body body, string idStyle, string text, JustificationValues justification = JustificationValues.Left)
        {
            Paragraph headingPar = clsOpenXmlWordUtilities.CreateParagraphWithStyle(idStyle, justification);
            clsOpenXmlWordUtilities.AddTextToParagraph(headingPar, text);
            body.AppendChild(headingPar);
        }
    }
}
