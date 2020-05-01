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
                        Console.Write("Auto o Moto? [A/M]: ");
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
                        v.Marca = SetFields("\nInserisci marca: ");
                        v.Modello = SetFields("Inserisci modello: ");
                        v.Cilindrata = Convert.ToInt32(SetFields("Inserisci cilindrata: "));
                        v.PotenzaKw = Convert.ToDouble(SetFields("Inserisci potenza [KW]: "));
                        v.Immatricolazione = Convert.ToDateTime(SetFields("Inserisci data di immatricolazione [gg/mm/aaaa]: "));
                        v.IsUsato = Convert.ToBoolean(SetFields("Il veicolo è usato? [Si/No] "));
                        v.IsKmZero = Convert.ToBoolean(SetFields("Il veicolo è Km zero? [Si/No] "));
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
                            (v as moto).MarcaSella = SetFields("Inserisci la marca della sella: ");
                            query = "INSERT INTO cars(marca, modello, cilindrata, potenzaKw," +
                                "immatricolazione, kmPercorsi, colore, isUsato, isKmZero, marcaSella) VALUES(@marca, @modello," +
                                "@cilindrata, @potenzaKw, @immatr, @kmPercorsi, @colore, @isUsato, @isKmZero, @mSella)";
                        }

                        cmd.CommandText = query;
                        addParameters(cmd, v);

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

            if (v is auto)
                cmd.Parameters.Add("@nAirBag", OleDbType.Integer).Value = (v as auto).NumairBag;
            else
                cmd.Parameters.Add("@mSella", OleDbType.VarChar, 255).Value = (v as moto).MarcaSella;
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

                    //type BIT in the table = boolean
                    try
                    {
                        cmd.CommandText = @"CREATE TABLE cars(
                                            id int identity(1,1) NOT NULL PRIMARY KEY,
                                            marca VARCHAR(255) NOT NULL,
                                            modello VARCHAR(255) NOT NULL,
                                            cilindrata INT,
                                            potenzaKw DOUBLE,
                                            immatr DATE,
                                            kmPercorsi INT,
                                            colore VARCHAR(255) NOT NULL,
                                            isUsato BIT,
                                            isKmZero BIT,
                                            nAirBag INT,
                                            mSella VARCHAR(255)
                                          )";
                        cmd.ExecuteNonQuery();
                    }
                    catch (OleDbException exc)
                    {
                        Console.WriteLine("\n\n" + exc.Message);
                        System.Threading.Thread.Sleep(2000);
                        return;
                    }                    
                    Console.WriteLine("\n\nCars created!");
                    System.Threading.Thread.Sleep(3000);
                }
            }
        }

    }
}
