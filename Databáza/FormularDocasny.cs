/*
 * Vytvořeno aplikací SharpDevelop.
 * Uživatel: fodor
 * Datum: 6.2.2013
 * Čas: 11:41
 */

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data;
using System.IO;
using System.Diagnostics;

public class FormDocasny : Form
{
	DataSet ds = new DataSet();
	Databaza mojaDB = new Databaza();
	Button btnOverRevizie;
	Label SLD,SLDr;	
	
	public FormDocasny()
	{
		Text = "Dočasný";
		Width = 1000;
		Height = 500;
		
		ds = mojaDB.NaplnDataset("Zariadenia");

		SLD = new Label();
		SLD.Location = new Point(10,50);
		SLD.AutoSize = true;
		SLD.Parent = this;
		SLD.Text = /*mojaDB.Napln("3BJA21","SLD",ds)*/ "PNM3417004601";
		
		SLDr = new Label();
		SLDr.Location = new Point(SLD.Right + 10,50);
		SLDr.AutoSize = true;
		SLDr.Parent = this;
		SLDr.ForeColor = Color.Red;
		SLDr.Font = new Font("Verdana",8,FontStyle.Bold);
		SLDr.Text = "tu bude je ci novsia rev";
		
        btnOverRevizie = new Button();
        btnOverRevizie.Text = "Over revízie";
        btnOverRevizie.Location = new Point(10,10);
        btnOverRevizie.AutoSize = true;
        btnOverRevizie.Parent = this;
        btnOverRevizie.Click += new EventHandler(btnOverRevizie_Click);
 	}
	
	public string cestaCI = @"\\server2008\MO34\E005\_TRANSMITTAL_ENEL-PPA-REFLECTIONS_CI\";
	public string zoznamSuborovCI; // Zoznam všetkých súborov CI medzi ktorými chceme hľadať (;)
	public string umiestnenieProgramu = Directory.GetCurrentDirectory();
	public string[] kontrStlpce = {/*"ITP_QCP","PLKVZ_CEQP",*/"SLD"/*,"GA","WD","IT_program"*/}; //stĺpce v ktorých sa budú kontrolovať revízie
	
	private void btnOverRevizie_Click(object sender, EventArgs e)
	{
		int pom = 0;
		DataSet ds = mojaDB.NaplnDataset("Zariadenia");
		DataTable dt = ds.Tables["Zariadenia"];
		
		#region  Overenie či existuje cestaCI, ak neexistuje tak užívateľ vyberie cestu sám
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
		#endregion
		
		zoznamSuborovCI = mojaDB.NacitanieSuborovZAdresarov(cestaCI);
		
		pom = int.Parse(mojaDB.NajdiReviziu(SLD.Text,zoznamSuborovCI).ToString());
		if (pom > int.Parse(SLD.Text.Substring(11)))
			SLDr.Text = "Existuje vyššia revízia: " + pom;
		else
		{
			SLDr.ForeColor = Color.Green;
			SLDr.Text = "Najnovšia revízia";
		}
	}
}