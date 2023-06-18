/*
 * Vytvořeno aplikací SharpDevelop.
 * Uživatel: fodor
 * Datum: 15.1.2013
 * Čas: 12:38s
 */

using System;
using System.Data;
using System.Data.OleDb;
using System.Xml;
using System.IO;

public class Databaza
{
	//Pripojenie sa k databáze - miesto kde je cesta k databáze, ak treba zmeniť tak to treba spraviť tu - pozrieť zbalenú verziu D:\Download\C#\Databáza - BETA v.0.0.0.0.rar a spraviť cestu k DB podľa tohoto (pozri Form.cs)
	OleDbConnection pripojenie = new OleDbConnection(@"provider=Microsoft.Jet.OLEDB.4.0; Data Source=Databáza.mdb");
	
	public string Napln(string riadok, string stlpec, DataSet ds)
	{
		DataTable dt = ds.Tables["Zariadenia"];
		
		foreach (DataRow dr in dt.Rows)
		{
			if ((string)dr["SJZ"] == riadok)
			{
				if (dr[stlpec].GetType().ToString() == "System.DBNull")
					return "";
				else
					return (string) dr[stlpec];
			}
		}
		return "";
	}
	
	public DataSet NaplnDataset(string tabulka)
	{
		// Text pre príkaz - načítaj všetko z tabuľky "Zariadenia" a zoraď to podľa stĺpca "SJZ"
		string strNacitanie = "SELECT * FROM " + tabulka + " ORDER BY SJZ";
		
		try
         {
            // Skusíme pripojeníe otvoriť:
            pripojenie.Open();
            
            OleDbDataAdapter mojAdapter = new OleDbDataAdapter(strNacitanie, pripojenie);
            DataSet mojDataSet = new DataSet();
            
            mojAdapter.Fill(mojDataSet, tabulka);
            
            return mojDataSet;
         }
         catch (OleDbException ex)
         {
            // Vypíšeme správu výnimky:
            Console.WriteLine("Naplnenie DB", ex.Message);
            return null;
         }
         finally
         {
            // Zavreme spojenie:
            pripojenie.Close();
         }
	}
	
	public void VykonajPrikaz(string update)
	{
		pripojenie.Open();
		OleDbCommand prikaz = new OleDbCommand(update, pripojenie);
		prikaz.ExecuteNonQuery();
		pripojenie.Close();
	}
	
	public string NacitanieSuborovZAdresarov(string cesta)
	{
		string zoznamAdresarov = cesta;
		zoznamAdresarov += NacitanieAdresarov(cesta);
		
		string zoznamSuborov = NacitanieSuborov(zoznamAdresarov);
		return zoznamSuborov;
	}
	
	private string NacitanieAdresarov(string adresare)
    {
		string[] pomAdr = Directory.GetDirectories(adresare);
		string pomZozn = "";
    	if (pomAdr.Length > 0)
    	{
    		for(int i=0; i < pomAdr.Length; i++)
    		{
    			pomZozn += ";" + pomAdr[i];
    			if(Directory.GetDirectories(pomAdr[i]).Length > 0)
    			{
    				pomZozn += NacitanieAdresarov(pomAdr[i]);
    			}
    		}
    	}
		else
			return null;
		return pomZozn;
    }
	
	private string NacitanieSuborov(string zoznam)
    {
		string zoznamSuborov = null;
		string[] dirs = zoznam.Split(';');
		string[] files = null;
		
		for (int i = 0; i < dirs.Length; i++)
		{
			files = Directory.GetFiles(dirs[i]);
			if (files.Length >0)
			{
				for (int j = 0; j < files.Length; j++)
				{
					if (zoznamSuborov == null)
						zoznamSuborov += files[j];
					else
						zoznamSuborov += ";" + files[j];
				}
			}
		}
		return zoznamSuborov;
    }
	
	public int NajdiReviziu(string PNM, string subory)
	{
		string[] rSubory = subory.Split(';');
		int rev = 0;
		for (int i = 0; i < rSubory.Length; i++)
		{
			if (rSubory[i].Contains(PNM.Substring(0,11)))
				if (int.Parse((rSubory[i].Substring(rSubory[i].IndexOf("PNM"),13)).Substring(11,2))>int.Parse((PNM.Substring(11))))
					rev = int.Parse((rSubory[i].Substring(rSubory[i].IndexOf("PNM"),13)).Substring(11,2));
		}
		return rev;
	}
}