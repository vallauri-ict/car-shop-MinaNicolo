using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using venditaVeicoliDLLProject;

namespace ReflectionCssPocProject
{
    public class Utils
    {
        public class SerializableBindingList<T> : BindingList<T> { }
        public static IEnumerable<string> ToCsv<T>(IEnumerable<T> objectlist, string separator = "|")
        {
            foreach (var o in objectlist)
            {
                FieldInfo[] fields = o.GetType().GetFields();
                PropertyInfo[] properties = o.GetType().GetProperties();

                yield return string.Join(separator, fields.Select(f => (f.GetValue(o) ?? "").ToString())
                    .Concat(properties.Select(p => (p.GetValue(o, null) ?? "").ToString())).ToArray());
            }
        }

        public static string ToCsvString<T>(IEnumerable<T> objectlist, string separator = "|")
        {
            StringBuilder csvdata = new StringBuilder();
            foreach (var o in objectlist)
            {
                FieldInfo[] fields = o.GetType().GetFields();
                PropertyInfo[] properties = o.GetType().GetProperties();

                csvdata.AppendLine(string.Join(separator, fields.Select(f => (f.GetValue(o) ?? "").ToString())
                    .Concat(properties.Select(p => (p.GetValue(o, null) ?? "").ToString())).ToArray()));
            }
            return csvdata.ToString();
        }
        public static void SerializeToCsv<T>(IEnumerable<T> objectlist,string pathName,string separator="|")
        {
            string datatosave = Utils.ToCsvString(objectlist, separator);
            File.WriteAllText(pathName, datatosave);
        }

        public static void SerializeToXml<T>(SerializableBindingList<T> objectlist, string pathName)
        {
            XmlSerializer x = new XmlSerializer(typeof(SerializableBindingList<T>));
            TextWriter w = new StreamWriter(pathName);
            x.Serialize(w, objectlist);
        }

        public static void SerializeToJson<T>(IEnumerable<T> objectlist, string pathName)
        {
            string json = JsonConvert.SerializeObject(objectlist,Formatting.Indented);
            File.WriteAllText(pathName,json);
        }

        public static void createHTML(SerializableBindingList<veicolo> lista, string homePath,string skeletonPath= @".\www\indexSkeleton.html")
        {
            string _div = "";
            string html = File.ReadAllText(skeletonPath);
            html = html.Replace("{{head-title}}", "AUTOVALLAURI");
            html = html.Replace("{{body-title}}", "SALONE AUTOVALLAURI");

            foreach (var item in lista)
            {
                _div += "<div class = \"divVeicolo\">";
                _div += $"<div class = \"titoloMarca\">{(item as veicolo).Marca} {(item as veicolo).Modello}";
                _div += $"<div class = \"didascalia\"> Colore: {(item as veicolo).Colore}<br> Cilindrata: {(item as veicolo).Cilindrata}<br> Immatricolazione: {(item as veicolo).Immatricolazione.ToString("dd/MM/yyyy")}<br> Prezzo: {(item as veicolo).Prezzo} € </div>";
                _div += "</div></div>";
            }
            html = html.Replace("{{body-subtitle}}", "Veicoli");
            html = html.Replace("{{main-content}}", _div);


            File.WriteAllText(homePath, html);
        }
    }
}
