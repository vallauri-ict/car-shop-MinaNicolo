using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using venditaVeicoliDLLProject;
using System.Data.OleDb;
using System.IO;
using static ReflectionCssPocProject.Utils;

namespace ConsoleAppProject
{
    class Program
    {
        public static string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=CarShop.accdb";
        static void Main(string[] args)
        {
            Console.WriteLine("\t\t\t----- SALONE VEICOLI USATI E NUOVI -----\n");

            /*moto m = new moto();
            m = new moto("Ducati", "Panigale V4R", 1000, 75, DateTime.Now, 0, "blu", false, false, "StandarCO");
            auto a = new auto("Alfa Romeo", "Stelvio", 2000, 150, DateTime.Now, 0, "rosso", false, false, 8);
            Console.WriteLine(m);
            Console.WriteLine(a);*/

            SerializableBindingList<veicolo> listaVeicoli = new SerializableBindingList<veicolo>();

            string scelta;

            do
            {

                Console.Write("\nFAI UNA SCELTA: (per vedere le scelte possibili premere H) ");
                scelta = Console.ReadLine().Trim().ToUpper();

                switch (scelta)
                {
                    case "H":
                        Console.WriteLine("\n" + File.ReadAllText(@".\Comandi.txt") + "\n");
                        break;
                    case "C": CreateTable(); break;
                    case "A":
                        Console.WriteLine("\n\t\t\tAGGIUNGI VEICOLO\n");
                        Console.WriteLine("Auto o Moto? [A/M]");
                        veicolo v;
                        string VeicoloScelto = Console.ReadLine().Trim().ToUpper();
                        if (VeicoloScelto == "A" || VeicoloScelto == "M")
                        {
                            if (VeicoloScelto == "A") v = new auto();
                            else v = new moto();
                        }
                        else break;
                        AddNewVehicle(v, listaVeicoli);
                       
                        break;
                    case "LS": CarList();break;

                    default:
                        Console.WriteLine("Comando non disponibile!");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }

            } while (scelta != "X");
            
        }

        private static void AddNewVehicle(veicolo v, SerializableBindingList<veicolo> lista)
        {
            if (connStr != null)
            {
                OleDbConnection con = new OleDbConnection(connStr);
                using (con)
                {
                    con.Open();

                    OleDbCommand cmd = new OleDbCommand();
                    cmd.Connection = con;

                    try
                    {
                        v.Marca = SetFields("Inserisci marca: ");
                        v.Modello = SetFields("Inserisci modello: ");
                        v.Cilindrata = Convert.ToInt32(SetFields("Inserisci cilindrata: "));
                        v.PotenzaKw = Convert.ToDouble(SetFields("Inserisci potenza [KW]: "));
                        v.Immatricolazione = Convert.ToDateTime(SetFields("Inserisci data di immatricolazione: "));
                        //v.IsUsato = Convert.ToBoolean(SetFields("Il veicolo è usato? [Si/No] "));
                        //v.IsKmZero = Convert.ToBoolean((SetFields("Il veicolo è Km zero? [Si/No] "));
                        v.KmPercorsi = v.IsUsato ? Convert.ToInt32(SetFields("Inserisci km percorsi: ")) : 0;
                        v.Colore = SetFields("Inserisci colore: ");                       

                        string query;
                        if (v is auto)
                        {
                            (v as auto).NumairBag = Convert.ToInt32(SetFields("Inserisci numero di airbag: "));
                            query = "INSERT INTO cars(marca, modello, cilindrata, potenzaKw," +
                                "immatricolazione, kmPercorsi, colore, isUsato, isKmZero, numAirBag) VALUES(@marca, @modello," +
                                "@cilindrata, @potenzaKw, @immatr, @kmPercorsi, @colore, @isUsato, @isKmZero, @nAirBag)";
                        }
                        else
                        {
                            (v as moto).MarcaSella = SetFields("Inserisci numero di airbag: ");
                            query = "INSERT INTO cars(marca, modello, cilindrata, potenzaKw," +
                                "immatricolazione, kmPercorsi, colore, isUsato, isKmZero, marcaSella) VALUES(@marca, @modello," +
                                "@cilindrata, @potenzaKw, @immatr, @kmPercorsi, @colore, @isUsato, @isKmZero, @mSella)";
                        }
                        cmd.CommandText = query;

                        addParameters(cmd, v);

                        if (v is auto)
                            cmd.Parameters.Add("@nAirBag", OleDbType.Integer).Value=(v as auto).NumairBag;
                        else
                            cmd.Parameters.Add("@mSella", OleDbType.VarChar,255).Value = (v as moto).MarcaSella;

                        cmd.Prepare();
                        cmd.ExecuteNonQuery();
                        System.Threading.Thread.Sleep(3000);

                        Console.WriteLine("Veicolo aggiunto correttamente!");
                        lista.Add(v);
                        Console.ReadKey();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message + " -- Errore durante il caricamento del veicolo!");
                    }                   
                }
            }
        }

        private static void addParameters(OleDbCommand cmd, veicolo v)
        {
            cmd.Parameters.Add(new OleDbParameter("@marca", OleDbType.VarChar, 255)).Value = v.Marca;
            cmd.Parameters.Add("@modello", OleDbType.VarChar, 255).Value = v.Modello;
            cmd.Parameters.Add("@cilindrata", OleDbType.Integer).Value = v.Cilindrata;
            cmd.Parameters.Add("@potenzaKw", OleDbType.Double).Value = v.PotenzaKw;
            cmd.Parameters.Add("@immatr", OleDbType.Date).Value = v.Immatricolazione;
            cmd.Parameters.Add("@kmPercorsi", OleDbType.Integer).Value = v.KmPercorsi;
            cmd.Parameters.Add("@colore", OleDbType.VarChar, 255).Value = v.Colore;
            cmd.Parameters.Add("@isUsato", OleDbType.Boolean).Value = v.IsUsato;
            cmd.Parameters.Add("@isKmZero", OleDbType.Boolean).Value = v.IsKmZero;
        }

        private static string SetFields(string question)
        {
            Console.Write(question);
            string r = Console.ReadLine().Trim().ToUpper();
            if (r == "SI" || r == "NO")
            {
                if (r == "SI")
                    return "true";
                else
                    return "false";
            }
            else
                return r;
        }

        private static void CarList()
        {
            if (connStr != null)
            {
                OleDbConnection connection = new OleDbConnection(connStr);
                using (connection)
                {
                    connection.Open();

                    OleDbCommand command = new OleDbCommand("SELECT * FROM cars", connection);

                    OleDbDataReader rdr = command.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        Console.WriteLine("\n");
                        while (rdr.Read())
                        {
                            Console.WriteLine("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}",
                                rdr.GetInt32(0), rdr.GetString(1),rdr.GetInt32(2),
                                rdr.GetInt32(3), rdr.GetString(4), rdr.GetInt32(5),
                                rdr.GetInt32(6), rdr.GetString(7), rdr.GetInt32(8),
                                rdr.GetInt32(9));
                        }
                    }
                    else
                    {
                        Console.WriteLine("\n\nNo rows found.");
                    }
                    rdr.Close();
                }
                Console.WriteLine("\nCars listed!");
                System.Threading.Thread.Sleep(5000);
            }
        }

        private static void CreateTable()
        {
            if (connStr != null)
            {
                OleDbConnection connection = new OleDbConnection(connStr);
                using (connection)
                {
                    connection.Open();

                    OleDbCommand cmd = new OleDbCommand();
                    cmd.Connection = connection;

                    // cmd.CommandText = "DROP TABLE IF EXISTS cars";
                    // cmd.ExecuteNonQuery();

                    try
                    {
                        cmd.CommandText = @"CREATE TABLE cars(
                                            id int identity(1,1) NOT NULL PRIMARY KEY,
                                            name VARCHAR(255) NOT NULL,
                                            price INT
                                          )";
                        cmd.ExecuteNonQuery();
                    }
                    catch (OleDbException exc)
                    {
                        Console.WriteLine("\n\n" + exc.Message);
                        System.Threading.Thread.Sleep(3000);
                        return;
                    }

                    cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Audi',52642)";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Mercedes',57127)";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Skoda',9000)";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Volvo',29000)";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Bentley',350000)";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Citroen',21000)";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Hummer',41400)";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Volkswagen',21600)";
                    cmd.ExecuteNonQuery();

                    Console.WriteLine("\n\nCars created with test data!");
                    System.Threading.Thread.Sleep(3000);
                }
            }
        }

    }
}
