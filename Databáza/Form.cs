/*
 * Vytvořeno aplikací SharpDevelop.
 * Uživatel: fodor
 * Datum: 15.1.2013
 * Čas: 12:43
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Diagnostics;

public class Formular : Form
{
	private Font font;
	private Brush brush;
	private Button btnKartaZariadenia, btnUpravaDat, btnDocasnyPrePRT, btnModifikacie, btnSubory, btnOverRevizie;
	private Label lblDBNull, lblZapisaneBunky, lblPocetZariadeni, lblPrazdneBunky, lblDBNullD, lblZapisaneBunkyD,
			lblPocetZariadeniD, lblPrazdneBunkyD;
	private Databaza mojaDB = new Databaza();
	
	public Formular()
	{
		Text = "Databáza";
		Width = 1000;
		Height = 500;
		font = new Font("Verdana", 9, FontStyle.Bold);
        brush = new SolidBrush(ForeColor);
        
        #region TLAČIDLA + ŠTÍTKY
        btnUpravaDat = new Button();
        btnUpravaDat.Text = "Úprava údajov";
        btnUpravaDat.Location = new Point(10,10);
        btnUpravaDat.AutoSize = true;
        btnUpravaDat.Parent = this;
        btnUpravaDat.Font = font;
        btnUpravaDat.Click += new EventHandler(btnUpravaDat_Click);
        
        btnModifikacie = new Button();
        btnModifikacie.Text = "Modifikácie";
        btnModifikacie.Location = new Point(btnUpravaDat.Right + 10,10);
        btnModifikacie.AutoSize = true;
        btnModifikacie.Parent = this;
        btnModifikacie.Font = font;
        btnModifikacie.Click += new EventHandler(btnModifikácie_Click);
        
        btnKartaZariadenia = new Button();
        btnKartaZariadenia.Text = "Karta zariadenia";
        btnKartaZariadenia.Location = new Point(btnModifikacie.Right + 10,10);
        btnKartaZariadenia.AutoSize = true;
        btnKartaZariadenia.Parent = this;
        btnKartaZariadenia.Font = font;
        btnKartaZariadenia.Click += new EventHandler(btnKartaZariadenia_Click);
        
        btnSubory = new Button();
        btnSubory.Text = "Hľadanie súborov";
        btnSubory.Location = new Point(btnKartaZariadenia.Right + 10,10);
        btnSubory.AutoSize = true;
        btnSubory.Parent = this;
        btnSubory.Font = font;
        btnSubory.Click += new EventHandler(btnSubory_Click);
        
        btnDocasnyPrePRT = new Button();
        btnDocasnyPrePRT.Text = "Dočasný";
        btnDocasnyPrePRT.Location = new Point(btnSubory.Right + 10,10);
        btnDocasnyPrePRT.AutoSize = true;
        btnDocasnyPrePRT.Parent = this;
        btnDocasnyPrePRT.Font = font;
        btnDocasnyPrePRT.Click += new EventHandler(btnDocasnyPrePRT_Click);
        
        btnOverRevizie = new Button();
        btnOverRevizie.Text = "Over revízie";
        btnOverRevizie.Location = new Point(btnDocasnyPrePRT.Right + 10,10);
        btnOverRevizie.AutoSize = true;
        btnOverRevizie.Parent = this;
        btnOverRevizie.Font = font;
        btnOverRevizie.Click += new EventHandler(btnOverRevizie_Click);
        
        lblDBNull = new Label();
		lblDBNull.Location = new Point(10,btnUpravaDat.Bottom + 10);
		lblDBNull.AutoSize = true;
		lblDBNull.Parent = this;
		lblDBNull.Text = "Počet buniek s hodnotou DBNull";
		
		lblZapisaneBunky = new Label();
		lblZapisaneBunky.Location = new Point(10,lblDBNull.Bottom + 10);
		lblZapisaneBunky.AutoSize = true;
		lblZapisaneBunky.Parent = this;
		lblZapisaneBunky.Text = "Počet zapisaných buniek (okrem stlpca SJZ)";
		
		lblPocetZariadeni = new Label();
		lblPocetZariadeni.Location = new Point(10,lblZapisaneBunky.Bottom + 10);
		lblPocetZariadeni.AutoSize = true;
		lblPocetZariadeni.Parent = this;
		lblPocetZariadeni.Text = "Počet zariadení: ";
		
		lblPrazdneBunky = new Label();
		lblPrazdneBunky.Location = new Point(10,lblPocetZariadeni.Bottom + 10);
		lblPrazdneBunky.AutoSize = true;
		lblPrazdneBunky.Parent = this;
		lblPrazdneBunky.Text = "Počet prázdnych buniek (mimo DBNull)";
		
		lblDBNullD = new Label();
		lblDBNullD.Location = new Point(lblZapisaneBunky.Right + 10, lblDBNull.Top);
		lblDBNullD.AutoSize = true;
		lblDBNullD.Parent = this;
		
		lblZapisaneBunkyD = new Label();
		lblZapisaneBunkyD.Location = new Point(lblZapisaneBunky.Right + 10, lblZapisaneBunky.Top);
		lblZapisaneBunkyD.AutoSize = true;
		lblZapisaneBunkyD.Parent = this;
		
		lblPocetZariadeniD = new Label();
		lblPocetZariadeniD.Location = new Point(lblZapisaneBunky.Right + 10, lblPocetZariadeni.Top);
		lblPocetZariadeniD.AutoSize = true;
		lblPocetZariadeniD.Parent = this;
		
		lblPrazdneBunkyD = new Label();
		lblPrazdneBunkyD.Location = new Point(lblZapisaneBunky.Right + 10, lblPrazdneBunky.Top);
		lblPrazdneBunkyD.AutoSize = true;
		lblPrazdneBunkyD.Parent = this;
        #endregion
        
        #region Výpis počtu zapísaných, prázdnych buniek, .....
		DataSet ds = mojaDB.NaplnDataset("Zariadenia");
		DataTable dt = ds.Tables["Zariadenia"];
		int dBNull, zapisane, zariadenia, prazdne;
		dBNull = zapisane = zariadenia = prazdne = 0;
		
		foreach (DataRow dr in dt.Rows)
		{
			foreach (DataColumn dc in dt.Columns)
			{
				if ((string) dr[dc.ColumnName].GetType().ToString() == "System.DBNull")
					dBNull++;
				else if ((string) dr[dc.ColumnName] != "")
					zapisane++;
				else
					prazdne++;
			}
			zariadenia++;
		}
		lblDBNullD.Text = dBNull.ToString();
		lblZapisaneBunkyD.Text = (zapisane - zariadenia).ToString();
		lblPocetZariadeniD.Text = zariadenia.ToString();
		lblPrazdneBunkyD.Text = prazdne.ToString();
        #endregion
	}
	
	private void btnDocasnyPrePRT_Click(object sender, EventArgs e)
	{
		FormDocasny frmDocasny = new FormDocasny();
		frmDocasny.Show();
	}
	
	private void btnUpravaDat_Click(object sender, EventArgs e)
	{
		FormUprava frmUprava = new FormUprava();
		frmUprava.Show();
	}
	
	private void btnKartaZariadenia_Click(object sender, EventArgs e)
	{
		FormZariadenie frmzar = new FormZariadenie();
		frmzar.Show();
	}
	
	private void btnModifikácie_Click(object sender, EventArgs e)
	{
		FormMod frmMod = new FormMod();
		frmMod.Show();
	}
	
	private void btnSubory_Click(object sender, EventArgs e)
	{
		FormHladanieSuborov frmHladSub = new FormHladanieSuborov();
		frmHladSub.Show();
	}
	
	#region Overenie najnovšej revízie dokumentov porovnaním zo súbormi na servri
	public string cestaCI = @"\\server2008\MO34\E005\_TRANSMITTAL_ENEL-PPA-REFLECTIONS_CI\";
	public string cestaNI = @"\\server2008\MO34\E005\_TRANSMITTAL_ENEL-PPA-REFLECTIONS_NI\";
	public string zoznamAdresarovCI; // Zoznam adresárov a podadresárov vrátane toho v ktorom chceme hľadať (cestaCI), oddelené bodkočiarkou (;)
	public string zoznamAdresarovNI; // Zoznam adresárov a podadresárov vrátane toho v ktorom chceme hľadať (cestaNI), oddelené bodkočiarkou (;)
	public string zoznamSuborovCI; // Zoznam všetkých súborov CI medzi ktorými chceme hľadať (;)
	public string zoznamSuborovNI; // Zoznam všetkých súborov CI medzi ktorými chceme hľadať (;)
	public string[] suboryCI; // Zoznam všetkých súborov CI medzi ktorými chceme hľadať
	public string[] suboryNI; // Zoznam všetkých súborov NI medzi ktorými chceme hľadať
	public string umiestnenieProgramu = Directory.GetCurrentDirectory();
	public string[] kontrStlpce = {"ITP_QCP_Manufacturing","ITP_QCP","PLKVZ_CEQP","SLD","GA","WD","FAT_Procedúra","IT_program"}; //stĺpce v ktorých sa budú kontrolovať revízie
	
	private void btnOverRevizie_Click(object sender, EventArgs e)
	{
		DataSet ds = mojaDB.NaplnDataset("Zariadenia");
		DataTable dt = ds.Tables["Zariadenia"];
		
		// Overenie či existuje cestaCI, ak neexistuje tak užívateľ vyberie cestu sám
		if (!Directory.Exists(cestaCI))
		{
			FolderBrowserDialog otvorAdresar = new FolderBrowserDialog();
			cestaCI = otvorAdresar.SelectedPath = @"C:\";
			otvorAdresar.Description = "Zvoľ adresár v ktorom chceš hľadať súbory pre CI";
			if (otvorAdresar.ShowDialog() == DialogResult.OK)
			{
				if (!otvorAdresar.SelectedPath.EndsWith(@"\"))
					cestaCI = otvorAdresar.SelectedPath + @"\";
				else
					cestaCI = otvorAdresar.SelectedPath;
			}
		}
		// Overenie či existuje cestaNI, ak neexistuje tak užívateľ vyberie cestu sám
		if (!Directory.Exists(cestaNI))
		{
			FolderBrowserDialog otvorAdresar = new FolderBrowserDialog();
			cestaNI = otvorAdresar.SelectedPath = @"C:\";
			otvorAdresar.Description = "Zvoľ adresár v ktorom chceš hľadať súbory pre NI";
			if (otvorAdresar.ShowDialog() == DialogResult.OK)
			{
				if (!otvorAdresar.SelectedPath.EndsWith(@"\"))
					cestaNI = otvorAdresar.SelectedPath + @"\";
				else
					cestaNI = otvorAdresar.SelectedPath;
			}
		}
		
		FileStream fs = new FileStream(Path.Combine(umiestnenieProgramu, "LogSubor.log"), FileMode.Create);
		StreamWriter sw = new StreamWriter(fs);
		sw.WriteLine(DateTime.Now.ToString());
		
		suboryCI = mojaDB.NacitanieSuborovZAdresarov(cestaCI).Split(';');
		sw.WriteLine(DateTime.Now.ToString());
		suboryNI = mojaDB.NacitanieSuborovZAdresarov(cestaNI).Split(';');
		sw.WriteLine(DateTime.Now.ToString());
		
		#region Prehľadanie celej DB
		foreach (DataRow dr in dt.Rows)
		{	// Ak sa bunke nenachádza "N/A" alebo ak nie je bunka prázdna, alebo ak má bunka 13 znakov vykonaj Over(...)
			for (int i = 0; i < kontrStlpce.Length; i++)
			{
				if (!(dr[kontrStlpce[i]].ToString() == "N/A" || dr[kontrStlpce[i]].ToString() == "" || dr[kontrStlpce[i]].ToString().Length != 13))
				{
					if (Over(dr[kontrStlpce[i]].ToString(), dr["LOT"].ToString()))
						sw.WriteLine(dr["SJZ"] + " " + kontrStlpce[i] + ": existuje vyššia revízia ako rev." + dr[kontrStlpce[i]].ToString().Substring(11));
				}
				else if (dr[kontrStlpce[i]].ToString() == "")
					sw.WriteLine(dr["SJZ"] + " " + kontrStlpce[i] + ": žiadný záznam");
				else if (dr[kontrStlpce[i]].ToString().Length != 13 && dr[kontrStlpce[i]].ToString() != "N/A")
					sw.WriteLine(dr["SJZ"] + " " + kontrStlpce[i] + ": v PNM chýba revízia resp. je zle napísane");
			}
		}
		#endregion
		
		sw.WriteLine(DateTime.Now.ToString());
		sw.Close();
		Process.Start(Path.Combine(umiestnenieProgramu, "LogSubor.log"));
	}
	
	public bool Over(string strPNM, string lot) 
	{
		string revPNM = strPNM.Substring(11);
		string PNM = strPNM.Remove(11,2);
		
		if (lot.Contains("C") || lot.Contains("c"))
		{
			for (int i = 0; i < suboryCI.Length; i++)
			{
				if (suboryCI[i].Contains(PNM))
				{	
					if (suboryCI[i].Contains("rev"))
				    {
						suboryCI[i] = suboryCI[i].Remove(suboryCI[i].IndexOf("rev"),3);
					}
					//Ak revízia súboru z DB je menšia ako súboru najdeného, tak ...
					if (int.Parse(revPNM) < JeToCislo((suboryCI[i].Substring(suboryCI[i].IndexOf("PNM"), 13)).Substring(11,2))) // spraviť procedúru ktorá overí či "(suboryCI[i].Substring(suboryCI[i].IndexOf("PNM"), 13)).Substring(11,2)" je číslo okontrolovať prvý znak, aj druhý
						return true;
				}
			}
			return false;
		}
		
		if (lot.Contains("N") || lot.Contains("n"))
		{
			for (int i = 0; i < suboryNI.Length; i++)
			{
				if (suboryNI[i].Contains(PNM))
				{	//Ak revízia súboru zDB je menšia ako súboru najdeného, tak ...
					if (int.Parse(revPNM) < JeToCislo((suboryNI[i].Substring(suboryNI[i].IndexOf("PNM"), 13)).Substring(11,2))) 
						return true;
				}
			}
			return false;
		}
		return false;
	}
	
	public int JeToCislo(string rev)
	{
		if(rev.Length==2)
		{
			if(rev[0] == '0' || rev[0] == '1' || rev[0] == '2' || rev[0] == '3' || rev[0] == '4' || rev[0] == '5' || rev[0] == '6' || 
			   rev[0] == '7' || rev[0] == '8' || rev[0] == '9')
			{
				if(rev[1] == '0' || rev[1] == '1' || rev[1] == '2' || rev[1] == '3' || rev[1] == '4' || rev[1] == '5' || rev[1] == '6' ||
				   rev[1] == '7' || rev[1] == '8' || rev[1] == '9')
					return int.Parse(rev);
				else
					return 0;
			}
			else
				return 0;
		}
		else
			return 0;
	}
	#endregion
}