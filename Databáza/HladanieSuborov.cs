/*
 * Vytvořeno aplikací SharpDevelop.
 * Uživatel: fodor
 * Datum: 19.4.2013
 * Čas: 16:22
 */

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Diagnostics;

public class FormHladanieSuborov : Form
{
	private Button btnOtvorZoznam; //Otvorí súbor v ktorom je zoznam súborov ktoré chceme nájsť a skopírovať
	private Button btnOtvorAdresar; // Otvorí adresár v ktorom chceme hľadať
	private Button btnSpustHladanie; // Spustí hľadanie a kopírovanie
	private Label lblSubor, lblAdresar;
	public string zoznam; // Obsahuje cestu a súbor v ktorom je zoznam súborov ktoré chceme nájsť a skopírovať
	public string adresar; // Obsahuje cestu k adresáru v ktorom chceme hľadať
	public string cestaUlozit; // Obsahuje cestu k adresáru v ktorom uložené nájdené súbory, prednastavená C:\
	public string zoznamAdresarov; // Zoznam adresárov a podadresárov vrátane toho v ktorom chceme hľadať (adresar), oddelené bodkočiarkou (;)	
	public string zoznamVsetkychSuborov; // Zoznam všetkých súborov medzi ktorými chceme hľadať (;)
	public string zoznamHladanychSuborov; // Zoznam súborov ktoré chceme hľadať (;)
	public string umiestnenieProgramu = Directory.GetCurrentDirectory();
	
	public FormHladanieSuborov ()
	{
		Text = "Hľadanie a skopírovanie súborov";
		Width = 1000;
		Height = 500;
		
		btnOtvorZoznam = new Button();
		btnOtvorZoznam.Location = new Point(10,10);
		btnOtvorZoznam.AutoSize = true;
		btnOtvorZoznam.Text = "Načítaj zoznam\nsúborov";
		btnOtvorZoznam.Parent = this;
		btnOtvorZoznam.Click += new EventHandler(btnOtvorZoznam_Click);
		
		btnOtvorAdresar = new Button();
		btnOtvorAdresar.Location = new Point(btnOtvorZoznam.Right + 10,10);
		btnOtvorAdresar.AutoSize = true;
		btnOtvorAdresar.Text = "Načítaj adresár,\nkde chceš hľadať";
		btnOtvorAdresar.Parent = this;
		btnOtvorAdresar.Click += new EventHandler(btnOtvorAdresar_Click);
		
		lblSubor = new Label();
		lblSubor.Location = new Point(10,btnOtvorZoznam.Bottom + 10);
		lblSubor.AutoSize = true;
		lblSubor.Parent = this;
		lblSubor.Text = "Súbor zo zoznamom: " + zoznam;
		
		lblAdresar = new Label();
		lblAdresar.Location = new Point(10,lblSubor.Bottom + 10);
		lblAdresar.AutoSize = true;
		lblAdresar.Parent = this;
		lblAdresar.Text = "Adresár kde sa bude hľadať zoznamom: " + adresar;
		
		btnSpustHladanie = new Button();
		btnSpustHladanie.Location = new Point(10,lblAdresar.Bottom + 10);
		btnSpustHladanie.AutoSize = true;
		btnSpustHladanie.Text = "Spusť hľadanie\n a kopírovanie";
		btnSpustHladanie.Parent = this;
		btnSpustHladanie.Enabled = false;
		btnSpustHladanie.Click += new EventHandler(btnSpustHladanie_Click);
	}
	
	private void btnOtvorZoznam_Click(object sender, EventArgs e)
	{
		OpenFileDialog otvorZoznam = new OpenFileDialog();
		if (otvorZoznam.ShowDialog() == DialogResult.OK && otvorZoznam.FileName.Length > 0)
		{
			zoznam = otvorZoznam.FileName;
			this.lblSubor.Text = "Súbor zo zoznamom: " + zoznam;
		}
		if (zoznam != null && adresar != null)
			btnSpustHladanie.Enabled = true;
	}
	
	private void btnOtvorAdresar_Click(object sender, EventArgs e)
	{
		FolderBrowserDialog otvorAdresar = new FolderBrowserDialog();
		otvorAdresar.SelectedPath = @"C:\ako";
		otvorAdresar.Description = "Zvoľ adresár v ktorom chceš hľadať";
		if (otvorAdresar.ShowDialog() == DialogResult.OK)
		{
			adresar = otvorAdresar.SelectedPath;
			lblAdresar.Text = "Adresár kde sa bude hľadať: " + adresar;
		}
		if (zoznam != null && adresar != null)
			btnSpustHladanie.Enabled = true;
	}
	
	private void btnSpustHladanie_Click(object sender, EventArgs e) 
	{
		FolderBrowserDialog otvorAdresar = new FolderBrowserDialog();
		cestaUlozit = otvorAdresar.SelectedPath = @"C:\adresar";
		otvorAdresar.Description = "Zvoľ adresár do ktorého chceš kopírovať nájdené súbory";
		if (otvorAdresar.ShowDialog() == DialogResult.OK)
		{
			if (!otvorAdresar.SelectedPath.EndsWith(@"\"))
				cestaUlozit = otvorAdresar.SelectedPath + @"\";
			else
				cestaUlozit = otvorAdresar.SelectedPath;
		}
		NacitanieSuborovZAdresara();
		HladanieAKopirovanie();
		Process.Start(Path.Combine(umiestnenieProgramu, "Kopirovanie.log"));
	}
	
	public void NacitanieSuborovZAdresara()
	{
		zoznamAdresarov = adresar;
		string[] dirs = Directory.GetDirectories(adresar);
		
		NacitanieAdresarov(dirs);
		NacitanieSuborov();
	}
	
	public void NacitanieAdresarov(string[] adresare)
    {
    	foreach(string dir in adresare)
    	{
    		if(Directory.GetDirectories(dir).Length > 0)
    		{
    			NacitanieAdresarov(Directory.GetDirectories(dir));
    			zoznamAdresarov += ";" + dir;
    		}
    		else
    		{
    			zoznamAdresarov += ";" + dir;
    		}
    	}
    }
	
	public void NacitanieSuborov()
    {
		zoznamVsetkychSuborov = null;
		string[] dirs = zoznamAdresarov.Split(';');
		string[] files;
		
		for (int i = 0; i < dirs.Length; i++)
		{
			files = Directory.GetFiles(dirs[i]);
			if (files.Length >0)
			{
				for (int j = 0; j < files.Length; j++)
				{
					if (zoznamVsetkychSuborov == null)
						zoznamVsetkychSuborov += files[j];
					else
						zoznamVsetkychSuborov += ";" + files[j];
				}
			}
		}
    }
	
	public void HladanieAKopirovanie()
	{
		NacitanieZoSuboru();
		Porovnanie();
	}
	
	public void NacitanieZoSuboru()
	{
		zoznamHladanychSuborov = null;
		FileStream fs = new FileStream(zoznam, FileMode.Open);
		StreamReader sr = new StreamReader(fs);
		
		while(sr.Peek() > -1)
		{
			if (zoznamHladanychSuborov == null)
				zoznamHladanychSuborov = sr.ReadLine();
			else
				zoznamHladanychSuborov += ";" + sr.ReadLine();
		}
	}
	
	public void Porovnanie() 
	{
		string[] vsetkySubory = zoznamVsetkychSuborov.Split(';');
		string[] hladaneSubory = zoznamHladanychSuborov.Split(';');
		int pomLog = -1; // pomocná premenná pre log súbor

		FileStream fs = new FileStream(Path.Combine(umiestnenieProgramu, "Kopirovanie.log"), FileMode.Create);
		StreamWriter sw = new StreamWriter(fs);
		sw.WriteLine(DateTime.Now);
		sw.WriteLine("-------------------------------------------------------------------------------------------------------");
		sw.WriteLine("\"Nazov suboru\"  - subor ktorý hľadáme");
		sw.WriteLine("\t \"Nazov suboru\" - súbor ktorý bol nájdený + poznámka (ak je bez poznámky tak bol skopírovaný)");
		sw.WriteLine("-------------------------------------------------------------------------------------------------------");
		sw.WriteLine();
		
		for (int i = 0; i < hladaneSubory.Length; i++)
		{
			if(pomLog < i)
				sw.WriteLine(hladaneSubory[i]);
			pomLog = i;
			for (int j = 0; j < vsetkySubory.Length; j++)
			{
				if(vsetkySubory[j].Contains(hladaneSubory[i]))
				{
					string[] strPomLog = Kopiruj(vsetkySubory[j]).Split(';');
					if(strPomLog[1] == "nekopirovane")
						sw.WriteLine("\t" + strPomLog[0] + " - súbor nebol skopírovaný");
					else if(strPomLog[1] == "kopirovane")
					{
						if(strPomLog[2] == null || strPomLog[2] == "")
							sw.WriteLine("\t" + strPomLog[0]);
						else
							sw.WriteLine("\t" + strPomLog[0] + " - súbor bol premenovaný na: " + strPomLog[2]);
					}
				}
			}
		}
		sw.Close();
	}
	
	public string Kopiruj(string subor)
	{
		string menoSuboru = null;
		string kopirovanie = "nekopirovane";
		FileInfo fi = new FileInfo(subor);
		FormularZadajNazov zadaj = new FormularZadajNazov();
		
		if (!Directory.Exists(cestaUlozit)) // Ak adresár neexistuje
			Directory.CreateDirectory(cestaUlozit); // tak ho vytvor
		
		if (!File.Exists(cestaUlozit + fi.Name)) // Ak súbor na cieľovej adrese neexistuje
		{
			fi.CopyTo(cestaUlozit + fi.Name); // tak ho skopíruj
			kopirovanie = "kopirovane";
		}
		else                                  // ale ak existuje, tak ...
		{
			string pom = "Súbor " + fi.Name + " už existuje, chcete ho uložiť pod iným menom?";
			DialogResult dr = MessageBox.Show(pom, "Uložiť súbor?", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
			                                  MessageBoxDefaultButton.Button2);
			while (dr != DialogResult.No)
			{
				if(dr == DialogResult.Yes)
				{
					zadaj.txbZadajNazov.Text = subor.Substring(subor.LastIndexOf(@"\")+1, subor.Length - subor.LastIndexOf(@"\")-1);
					zadaj.ShowDialog(this);
					menoSuboru = zadaj.novyNazov;
					
					if (File.Exists(cestaUlozit + menoSuboru))
					{
						pom = "Aj tento súbor (" + menoSuboru + ") už existuje, chcete ho uložiť pod iným menom?";
						dr = MessageBox.Show(pom, "Uložiť súbor?", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
			                                  MessageBoxDefaultButton.Button2);
					}
					else
					{
						fi.CopyTo(cestaUlozit + menoSuboru);
						kopirovanie = "kopirovane";
						dr = DialogResult.No;
					}
				}
				else
				{
					menoSuboru = null;
					dr = DialogResult.No;
				}
			}
		}
		return subor + ";" + kopirovanie + ";" + menoSuboru;
	}
}

//Pomocný formulár pre zadanie názvu
public class FormularZadajNazov : Form
{
	public string novyNazov = null;
	public TextBox txbZadajNazov;
		
	public FormularZadajNazov()
	{
		Text = "Zadajte nový názov súboru (aj príponou)";
		Width = 200;
		Height = 170;
		
		txbZadajNazov = new TextBox();
		txbZadajNazov.Parent = this;
		txbZadajNazov.Location = new Point(10,50);
		txbZadajNazov.Size = new Size(150, this.FontHeight + 5);
		
		Button btnNazov = new Button();
		btnNazov.Location = new Point(50,txbZadajNazov.Bottom + 10);
		btnNazov.AutoSize = true;
		btnNazov.Text = "OK";
		btnNazov.Parent = this;
		btnNazov.Click += new EventHandler(btnNazov_Click);
	}
	
	public void btnNazov_Click(object sender, EventArgs e) 
	{
		novyNazov = txbZadajNazov.Text;
		txbZadajNazov.Text= "";
		this.Close();
	}
}