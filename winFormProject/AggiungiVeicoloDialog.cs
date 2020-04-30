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
namespace winFormProject
{
    public partial class AggiungiVeicoloDialog : Form
    {
        BindingList<veicolo> listVeicolo;
        int x = 0;
        int y = 0;
        public AggiungiVeicoloDialog(BindingList<veicolo> listVeicolo)
        {
            InitializeComponent();
            this.listVeicolo = listVeicolo;
        }

        string[] marcaAuto = { "Alfa Romeo","Audi","BMW","Citroen","Dacia","Ferrari","Fiat","Ford","Honda","Hunady",
            "Infinity","Jaguar","Jeep","Kia","Lamborghini","Lancia","Land Rover","Lexus","Maserati","Mazda",
            "Mercedes","Mini-Cooper","Mitsubishi","Nissan","Opel","Peugeot","Porsche","Renault","Seat",
            "Skoda","Smart","Subaru","Suzuki","Tesla","Toyota","Volkswagen","Volvo"
        };

        string[] marcaMoto ={
            "Aprilia","BMW","Ducati","Gilera","Harley Davidson","Honda","Husqvarna","Kawasaki","KTM","Moto Guzzi",
            "MV Agusta","Ohvale","Suzuki","Triumph","Yamaha","Vertigo","KSR","Zaeta"
        };

        string[] color = {"Aquamarina","Avorio", "Azzurro", "Bianco","Blu","Giallo","Grigio Antracite","Rosa","Nero","Ocra","Rosso","Verde"};
        private void AggiungiVeicoloDialog_Load(object sender, EventArgs e)
        {
            cmbTipoVeicolo.SelectedIndex = 0;
            cmbMarca.DataSource = marcaAuto;
            cmbColor.DataSource = color;
            nudMP.Maximum = int.MaxValue;
            
        }

        private void btnAggiungi_Click(object sender, EventArgs e)
        {
            if (cmbTipoVeicolo.SelectedIndex == 0)
                listVeicolo.Add(new auto(cmbMarca.SelectedItem.ToString(),txtModello.Text,int.Parse(txtCilindrata.Text) ,int.Parse(txtPotenza.Text),dtpImm.Value,Convert.ToInt32(nudMP.Value), cmbColor.SelectedItem.ToString(), cbUsato.Checked,cbKmZero.Checked, Convert.ToInt32(nudNumeroAirBag.Value)));
            else
                listVeicolo.Add(new moto(cmbMarca.SelectedItem.ToString(), txtModello.Text, int.Parse(txtCilindrata.Text), int.Parse(txtPotenza.Text), dtpImm.Value, Convert.ToInt32(nudMP.Value), cmbColor.SelectedItem.ToString(), cbUsato.Checked, cbKmZero.Checked, tbMarcaSella.Text));

            MessageBox.Show($"Aggiungi\n {listVeicolo.Last()}");
            this.Close();
        }

        private void btnAnnulla_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbTipoVeicolo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTipoVeicolo.SelectedIndex==1)
            {
                lblSellaNAirBag.Text = "Marca Sella";
                nudNumeroAirBag.Hide();
                tbMarcaSella.Show();
                cmbMarca.DataSource = marcaMoto;
            }
            else
            {
                lblSellaNAirBag.Text = "N Airbag";
                x = tbMarcaSella.Location.X;
                y = tbMarcaSella.Location.Y;
                nudNumeroAirBag.Location= new Point((int)x, y);
                nudNumeroAirBag.Show();
                tbMarcaSella.Hide();
                cmbMarca.DataSource = marcaAuto;
            }
        }

        private void cbUsato_CheckedChanged(object sender, EventArgs e)
        {
            if (cbUsato.Checked)
            {
                cbKmZero.Enabled = false;
                nudMP.Enabled = true;
            }
            else
            {
                cbKmZero.Enabled = true;
                nudMP.Enabled = true;
            }
        }

        private void cbKmZero_CheckedChanged(object sender, EventArgs e)
        {
            if (cbKmZero.Checked)
            {
                cbUsato.Enabled = false;
                nudMP.Enabled = false;
            }
            else
            {
                cbUsato.Enabled = true;
                nudMP.Enabled = true;
            }
        }
    }
}
