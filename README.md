# car-shop-MinaNicolo -- Vendita veicoli #

* Il progetto contiene un progetto Console nel quale è possibile creare due tabelle relative ad auto e moto in Access, aggiungere eventuali record, visualizzare le due tabelle ed infine eliminarle se necessario.
### Elenco comandi ###
```
H  [help] : mostra comandi
CS [clear screen] : pulisce lo schermo
C  [create] : crea tabella specificata
A  [add] : aggiunge un veicolo (auto o moto)
LS [list] : mostra lista veicoli
D  [delete] : elimina tabella specificata
```
* Un progetto Windows Form nel quale è possibile gestire il salone veicoli tramite un'interfaccia grafica.

* Una DLL nella quale sono presenti le principali classi per la gestione dell'intero progetto.
## Classi presenti all'interno della DLL ##
> 
Classe veicolo: è una classe astratta, contiene i campi per descrivere un veicolo generico

Classe auto: contiene i campi relativi ad un'auto

Classe moto: contiene i campi relativi ad una moto

Classe Utils: contiene delle funzioni generiche utili
>
## Alcune funzioni : ##
```javascript
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
```
