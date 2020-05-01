# car-shop-MinaNicolo -- Vendita veicoli #
## Functions in Program.cs ##
This function adds parametes to OleDbCommand
``` 
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

```
