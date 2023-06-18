/*
 * Vytvořeno aplikací SharpDevelop.
 * Uživatel: fodor
 * Datum: 13.2.2013
 * Čas: 13:19
 */

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

public class FormEditPRT : Form
{
	private Button btnUloz;
	private ComboBox cmbSJZ;
	private Label lblVK, lblSP, lblMK, lblUM, lblIT, lblREV, lblSTD;
	private TextBox txbVKDatum, txbVKCislo, txbSPDatum, txbSPCislo, txbMKDatum, txbMKCislo, txbUMDatum, txbUMCislo, txbITDatum, txbITCislo,
					txbREVDatum, txbREVCislo,txbSTDDatum, txbSTDCislo;
	ListBox lsbPom;
	private Databaza mojaDB = new Databaza();
	DataSet mojDS = new DataSet();
	DataTable mojaDT = new DataTable();
	
	public FormEditPRT()
	{
		Text = "Úprava protokolov";
		
		btnUloz = new Button();
		btnUloz.AutoSize = true;
		btnUloz.Text = "Ulož Zmeny";
		btnUloz.Parent = this;
		btnUloz.Click += new EventHandler(btnUloz_Click);
		
		//Naplnenie DataSetu a DataTable z databázy
		mojDS = mojaDB.NaplnDataset("Zariadenia");
		mojaDT = mojDS.Tables["Zariadenia"];
		
		cmbSJZ = new ComboBox();
		cmbSJZ.Location = new Point(10,10);
		//Naplnenie ComboBoxu SJZ zariadení
		foreach (DataRow dr in mojaDT.Rows)
		{
			cmbSJZ.Items.Add(dr["SJZ"]);
		}
		cmbSJZ.SelectedIndexChanged += new EventHandler(cmbSJZ_SelectedIndexChanged);
		cmbSJZ.TextChanged += new EventHandler(cmbSJZ_TextChanged);
		cmbSJZ.Parent = this;
		
		#region Vytvorenie textboxes a labels pre čísla dátumi protokolov + overenie prázdnosti textboxov (ak nie je prázdny tak je disable)
		#region Vstupná kontrola
		lblVK = new Label();
		lblVK.AutoSize = true;
		lblVK.Text = "Vstupna kontrola:";
		lblVK.Parent = this;
		lblVK.Location = new Point((15 + Font.Height * 9) - (lblVK.Width/2), cmbSJZ.Bottom + 10);
		
		txbVKCislo = new TextBox();
		txbVKCislo.Location = new Point(10, lblVK.Bottom + 3);
		txbVKCislo.Size = new Size(Font.Height * 9, Font.Height + 5);
		txbVKCislo.Name = "PRT_VK";
		txbVKCislo.Parent = this;
		txbVKCislo.Click += new EventHandler(txb_Click);
		
		txbVKDatum = new TextBox();
		txbVKDatum.Location = new Point(txbVKCislo.Right + 10, txbVKCislo.Top);
		txbVKDatum.Size = new Size(Font.Height * 9, Font.Height + 5);
		txbVKDatum.Name = "Dátum_VK";
		txbVKDatum.Parent = this;
		txbVKDatum.Click += new EventHandler(txb_Click);
		
		OverCiJeBunkaPrazdna(txbVKCislo);
		OverCiJeBunkaPrazdna(txbVKDatum);
		#endregion
		
		#region Stavebná pripravenosť
		lblSP = new Label();
		lblSP.AutoSize = true;
		lblSP.Text = "Stavebná pripravenosť:";
		lblSP.Parent = this;
		lblSP.Location = new Point((15 + Font.Height * 9) - (lblSP.Width/2), txbVKCislo.Bottom + 10);
		
		txbSPCislo = new TextBox();
		txbSPCislo.Location = new Point(10, lblSP.Bottom + 3);
		txbSPCislo.Size = new Size(Font.Height * 9, Font.Height + 5);
		txbSPCislo.Name = "PRT_SP";
		txbSPCislo.Parent = this;
		txbSPCislo.Click += new EventHandler(txb_Click);
		
		txbSPDatum = new TextBox();
		txbSPDatum.Location = new Point(txbSPCislo.Right + 10, txbSPCislo.Top);
		txbSPDatum.Size = new Size(Font.Height * 9, Font.Height + 5);
		txbSPDatum.Name = "Dátum_SP";
		txbSPDatum.Parent = this;
		txbSPDatum.Click += new EventHandler(txb_Click);
		
		OverCiJeBunkaPrazdna(txbSPCislo);
		OverCiJeBunkaPrazdna(txbSPDatum);
		#endregion
		
		#region Medzioperačná kontrola
		lblMK = new Label();
		lblMK.AutoSize = true;
		lblMK.Text = "Medzioperačná kontrola:";
		lblMK.Parent = this;
		lblMK.Location = new Point((15 + Font.Height * 9) - (lblMK.Width/2), txbSPDatum.Bottom + 10);
		
		txbMKCislo = new TextBox();
		txbMKCislo.Location = new Point(10, lblMK.Bottom + 3);
		txbMKCislo.Size = new Size(Font.Height * 9, Font.Height + 5);
		txbMKCislo.Name = "PRT_MK";
		txbMKCislo.Parent = this;
		txbMKCislo.Click += new EventHandler(txb_Click);
		
		txbMKDatum = new TextBox();
		txbMKDatum.Location = new Point(txbMKCislo.Right + 10, txbMKCislo.Top);
		txbMKDatum.Size = new Size(Font.Height * 9, Font.Height + 5);
		txbMKDatum.Name = "Dátum_MK";
		txbMKDatum.Parent = this;
		txbMKDatum.Click += new EventHandler(txb_Click);
		
		OverCiJeBunkaPrazdna(txbMKCislo);
		OverCiJeBunkaPrazdna(txbMKDatum);
		#endregion
		
		#region Ukončenie montáže
		lblUM = new Label();
		lblUM.AutoSize = true;
		lblUM.Text = "Ukončenie montáže:";
		lblUM.Parent = this;
		lblUM.Location = new Point((15 + Font.Height * 9) - (lblUM.Width/2), txbMKDatum.Bottom + 10);
		
		txbUMCislo = new TextBox();
		txbUMCislo.Location = new Point(10, lblUM.Bottom + 3);
		txbUMCislo.Size = new Size(Font.Height * 9, Font.Height + 5);
		txbUMCislo.Name = "PRT_UM";
		txbUMCislo.Parent = this;
		txbUMCislo.Click += new EventHandler(txb_Click);
		
		txbUMDatum = new TextBox();
		txbUMDatum.Location = new Point(txbUMCislo.Right + 10, txbUMCislo.Top);
		txbUMDatum.Size = new Size(Font.Height * 9, Font.Height + 5);
		txbUMDatum.Name = "Dátum_UM";
		txbUMDatum.Parent = this;
		txbUMDatum.Click += new EventHandler(txb_Click);
		
		OverCiJeBunkaPrazdna(txbUMCislo);
		OverCiJeBunkaPrazdna(txbUMDatum);
		#endregion
		
		#region Individuálne testy
		lblIT = new Label();
		lblIT.AutoSize = true;
		lblIT.Text = "Individuálne testy alebo SAT:";
		lblIT.Parent = this;
		lblIT.Location = new Point((15 + Font.Height * 9) - (lblIT.Width/2), txbUMDatum.Bottom + 10);
		
		txbITCislo = new TextBox();
		txbITCislo.Location = new Point(10, lblIT.Bottom + 3);
		txbITCislo.Size = new Size(Font.Height * 9, Font.Height + 5);
		txbITCislo.Name = "PRT_IT";
		txbITCislo.Parent = this;
		txbITCislo.Click += new EventHandler(txb_Click);
		
		txbITDatum = new TextBox();
		txbITDatum.Location = new Point(txbITCislo.Right + 10, txbITCislo.Top);
		txbITDatum.Size = new Size(Font.Height * 9, Font.Height + 5);
		txbITDatum.Name = "Dátum_IT";
		txbITDatum.Parent = this;
		txbITDatum.Click += new EventHandler(txb_Click);
		
		OverCiJeBunkaPrazdna(txbITCislo);
		OverCiJeBunkaPrazdna(txbITDatum);
		#endregion
		
		#region Revízna správa
		lblREV = new Label();
		lblREV.AutoSize = true;
		lblREV.Text = " Revízna správa:";
		lblREV.Parent = this;
		lblREV.Location = new Point((15 + Font.Height * 9) - (lblREV.Width/2), txbITDatum.Bottom + 10);
		
		txbREVCislo = new TextBox();
		txbREVCislo.Location = new Point(10, lblREV.Bottom + 3);
		txbREVCislo.Size = new Size(Font.Height * 9, Font.Height + 5);
		txbREVCislo.Name = "PRT_REV";
		txbREVCislo.Parent = this;
		txbREVCislo.Click += new EventHandler(txb_Click);
		
		txbREVDatum = new TextBox();
		txbREVDatum.Location = new Point(txbREVCislo.Right + 10, txbREVCislo.Top);
		txbREVDatum.Size = new Size(Font.Height * 9, Font.Height + 5);
		txbREVDatum.Name = "Dátum_REV";
		txbREVDatum.Parent = this;
		txbREVDatum.Click += new EventHandler(txb_Click);
		
		OverCiJeBunkaPrazdna(txbREVCislo);
		OverCiJeBunkaPrazdna(txbREVDatum);
		#endregion
		
		#region Sprievodná technická dokumentácia
		lblSTD = new Label();
		lblSTD.AutoSize = true;
		lblSTD.Text = "Sprievodná technická dokumentácia:";
		lblSTD.Parent = this;
		lblSTD.Location = new Point((15 + Font.Height * 9) - (lblSTD.Width/2), txbREVDatum.Bottom + 10);
		
		txbSTDCislo = new TextBox();
		txbSTDCislo.Location = new Point(10, lblSTD.Bottom + 3);
		txbSTDCislo.Size = new Size(Font.Height * 9, Font.Height + 5);
		txbSTDCislo.Name = "PRT_STD";
		txbSTDCislo.Parent = this;
		txbSTDCislo.Click += new EventHandler(txb_Click);
		
		txbSTDDatum = new TextBox();
		txbSTDDatum.Location = new Point(txbSTDCislo.Right + 10, txbSTDCislo.Top);
		txbSTDDatum.Size = new Size(Font.Height * 9, Font.Height + 5);
		txbSTDDatum.Name = "Dátum_STD";
		txbSTDDatum.Parent = this;
		txbSTDDatum.Click += new EventHandler(txb_Click);
		
		OverCiJeBunkaPrazdna(txbSTDCislo);
		OverCiJeBunkaPrazdna(txbSTDDatum);
		#endregion
		
		#endregion
		
		cmbSJZ.SelectedIndex = 0;
		btnUloz.Location = new Point((15 + Font.Height * 9) - (btnUloz.Width/2), txbSTDCislo.Bottom + 10);
		Width = txbVKDatum.Right + 10;
		Height = btnUloz.Bottom + 40;
		
		lsbPom = new ListBox();
		lsbPom.Parent = this;
		lsbPom.Location = new Point(300, 20);
		lsbPom.Size = new Size(200,200);
		lsbPom.Items.Add(lsbPom.Height.ToString());
	}
	
	private void btnUloz_Click(object sender, EventArgs e)
	{
		string[] pom = {OverCiJeBunkaPrazdnaAEnabled(txbVKCislo, txbVKDatum, lblVK), OverCiJeBunkaPrazdnaAEnabled(txbSPCislo, txbSPDatum, lblSP),
			OverCiJeBunkaPrazdnaAEnabled(txbMKCislo, txbMKDatum, lblMK), OverCiJeBunkaPrazdnaAEnabled(txbUMCislo, txbUMDatum, lblUM),
			OverCiJeBunkaPrazdnaAEnabled(txbITCislo, txbITDatum, lblIT), OverCiJeBunkaPrazdnaAEnabled(txbREVCislo, txbREVDatum, lblREV),
			OverCiJeBunkaPrazdnaAEnabled(txbSTDCislo, txbSTDDatum, lblSTD)};
		string prikaz = "";
		
		for (int i = 0; i < pom.Length; i++)
		{
			if (prikaz == "")
				prikaz = pom[i];
			else if (pom[i] != "")
				prikaz += ", " + pom[i];
		}
		
		if ((prikaz != "")&& !prikaz.Contains("NEUKLADAŤ"))
		{
			prikaz = "UPDATE Zariadenia SET " + prikaz + " WHERE SJZ = '" + (string) cmbSJZ.Text + "'";
			Console.WriteLine(prikaz);
			mojaDB.VykonajPrikaz(prikaz);
		}
		
		OverCiJeBunkaPrazdna(txbVKCislo);
		OverCiJeBunkaPrazdna(txbVKDatum);
		OverCiJeBunkaPrazdna(txbSPCislo);
		OverCiJeBunkaPrazdna(txbSPDatum);
		OverCiJeBunkaPrazdna(txbMKCislo);
		OverCiJeBunkaPrazdna(txbMKDatum);
		OverCiJeBunkaPrazdna(txbUMCislo);
		OverCiJeBunkaPrazdna(txbUMDatum);
		OverCiJeBunkaPrazdna(txbITCislo);
		OverCiJeBunkaPrazdna(txbITDatum);
		OverCiJeBunkaPrazdna(txbREVCislo);
		OverCiJeBunkaPrazdna(txbREVDatum);
		OverCiJeBunkaPrazdna(txbSTDCislo);
		OverCiJeBunkaPrazdna(txbSTDDatum);
		
		mojDS.Clear();
		mojDS = mojaDB.NaplnDataset("Zariadenia");
	}
	
	public void cmbSJZ_SelectedIndexChanged(object sender, EventArgs e)
	{
		txbVKCislo.Text = mojaDB.Napln((string) cmbSJZ.SelectedItem,"PRT_VK", mojDS);
		txbVKDatum.Text = mojaDB.Napln((string) cmbSJZ.SelectedItem,"Dátum_VK", mojDS);
		txbSPCislo.Text = mojaDB.Napln((string) cmbSJZ.SelectedItem,"PRT_SP", mojDS);
		txbSPDatum.Text = mojaDB.Napln((string) cmbSJZ.SelectedItem,"Dátum_SP", mojDS);
		txbMKCislo.Text = mojaDB.Napln((string) cmbSJZ.SelectedItem,"PRT_MK", mojDS);
		txbMKDatum.Text = mojaDB.Napln((string) cmbSJZ.SelectedItem,"Dátum_MK", mojDS);
		txbUMCislo.Text = mojaDB.Napln((string) cmbSJZ.SelectedItem,"PRT_UM", mojDS);
		txbUMDatum.Text = mojaDB.Napln((string) cmbSJZ.SelectedItem,"Dátum_UM", mojDS);
		txbITCislo.Text = mojaDB.Napln((string) cmbSJZ.SelectedItem,"PRT_IT", mojDS);
		txbITDatum.Text = mojaDB.Napln((string) cmbSJZ.SelectedItem,"Dátum_IT", mojDS);
		txbREVCislo.Text = mojaDB.Napln((string) cmbSJZ.SelectedItem,"PRT_REV", mojDS);	
		txbREVDatum.Text = mojaDB.Napln((string) cmbSJZ.SelectedItem,"Dátum_REV", mojDS);
		txbSTDCislo.Text = mojaDB.Napln((string) cmbSJZ.SelectedItem,"PRT_STD", mojDS);
		txbSTDDatum.Text = mojaDB.Napln((string) cmbSJZ.SelectedItem,"Dátum_STD", mojDS);

		OverCiJeBunkaPrazdna(txbVKCislo);
		OverCiJeBunkaPrazdna(txbVKDatum);
		OverCiJeBunkaPrazdna(txbSPCislo);
		OverCiJeBunkaPrazdna(txbSPDatum);
		OverCiJeBunkaPrazdna(txbMKCislo);
		OverCiJeBunkaPrazdna(txbMKDatum);
		OverCiJeBunkaPrazdna(txbUMCislo);
		OverCiJeBunkaPrazdna(txbUMDatum);
		OverCiJeBunkaPrazdna(txbITCislo);
		OverCiJeBunkaPrazdna(txbITDatum);
		OverCiJeBunkaPrazdna(txbREVCislo);
		OverCiJeBunkaPrazdna(txbREVDatum);
		OverCiJeBunkaPrazdna(txbSTDCislo);
		OverCiJeBunkaPrazdna(txbSTDDatum);
	}
	
	public void cmbSJZ_TextChanged(object sender, EventArgs e)
	{
		txbVKCislo.Text = mojaDB.Napln((string) cmbSJZ.Text.ToUpper(),"PRT_VK", mojDS);
		txbVKDatum.Text = mojaDB.Napln((string) cmbSJZ.Text.ToUpper(),"Dátum_VK", mojDS);
		txbSPCislo.Text = mojaDB.Napln((string) cmbSJZ.Text.ToUpper(),"PRT_SP", mojDS);
		txbSPDatum.Text = mojaDB.Napln((string) cmbSJZ.Text.ToUpper(),"Dátum_SP", mojDS);
		txbMKCislo.Text = mojaDB.Napln((string) cmbSJZ.Text.ToUpper(),"PRT_MK", mojDS);
		txbMKDatum.Text = mojaDB.Napln((string) cmbSJZ.Text.ToUpper(),"Dátum_MK", mojDS);
		txbUMCislo.Text = mojaDB.Napln((string) cmbSJZ.Text.ToUpper(),"PRT_UM", mojDS);
		txbUMDatum.Text = mojaDB.Napln((string) cmbSJZ.Text.ToUpper(),"Dátum_UM", mojDS);
		txbITCislo.Text = mojaDB.Napln((string) cmbSJZ.Text.ToUpper(),"PRT_IT", mojDS);
		txbITDatum.Text = mojaDB.Napln((string) cmbSJZ.Text.ToUpper(),"Dátum_IT", mojDS);
		txbREVCislo.Text = mojaDB.Napln((string) cmbSJZ.Text.ToUpper(),"PRT_REV", mojDS);
		txbREVDatum.Text = mojaDB.Napln((string) cmbSJZ.Text.ToUpper(),"Dátum_REV", mojDS);
		txbSTDCislo.Text = mojaDB.Napln((string) cmbSJZ.Text.ToUpper(),"PRT_STD", mojDS);
		txbSTDDatum.Text = mojaDB.Napln((string) cmbSJZ.Text.ToUpper(),"Dátum_STD", mojDS);
		
		OverCiJeBunkaPrazdna(txbVKCislo);
		OverCiJeBunkaPrazdna(txbVKDatum);
		OverCiJeBunkaPrazdna(txbSPCislo);
		OverCiJeBunkaPrazdna(txbSPDatum);
		OverCiJeBunkaPrazdna(txbMKCislo);
		OverCiJeBunkaPrazdna(txbMKDatum);
		OverCiJeBunkaPrazdna(txbUMCislo);
		OverCiJeBunkaPrazdna(txbUMDatum);
		OverCiJeBunkaPrazdna(txbITCislo);
		OverCiJeBunkaPrazdna(txbITDatum);
		OverCiJeBunkaPrazdna(txbREVCislo);
		OverCiJeBunkaPrazdna(txbREVDatum);
		OverCiJeBunkaPrazdna(txbSTDCislo);
		OverCiJeBunkaPrazdna(txbSTDDatum);
	}
	
	// Overí či je bunka prázdna, ak nie je tak zablokuje TextBox proti zmene
	public void OverCiJeBunkaPrazdna(TextBox txb)
	{
		if (txb.Text != "")
			txb.ReadOnly = true;
		else
			txb.ReadOnly = false;
	}
	
	public string OverCiJeBunkaPrazdnaAEnabled(TextBox txbCislo, TextBox txbDatum, Label lbl)
	{
		bool[] vystup = {false, false, false, false};
		string pom = "";
		
		if(txbCislo.Text != "")
		   vystup[0] = true;
		if(txbCislo.ReadOnly == false)
		   vystup[1] = true;
		if(txbDatum.Text != "")
		   vystup[2] = true;
		if(txbDatum.ReadOnly == false)
		   vystup[3] = true;
		
		if((vystup[0] == true) && (vystup[2] == true) && (vystup[1] == true) && (vystup[3] == true))
			return txbCislo.Name + " = '" + txbCislo.Text + "', " + txbDatum.Name + " = '" + txbDatum.Text + "' ";
		
		
		#region Ev. číslo prázdne, dátum vyplnený prístupny resp. dátum vyplnený neprístupný
		else if((vystup[0] == false)&&(vystup[1] == true))
		{
			if ((vystup[2] == true) && (vystup[3] == true))
			{
				string text = "Pravdepodobne ste zabudli dopísať ev.číslo protokolu " + lbl.Text.Replace(":","")
					+ ", chcete aj tak uložiť?";
				Console.WriteLine(text);
				DialogResult dr = MessageBox.Show(text, "POZOR",MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
				if (dr == DialogResult.Yes)
				{
					pom = txbDatum.Name + " = '" + txbDatum.Text + "'";
					Console.WriteLine(pom);
					return pom;
				}
				else
				{
					Console.WriteLine("NEUKLADAŤ");
					return "NEUKLADAŤ";
				}
			}
			if ((vystup[2] == true) && (vystup[3] == false))
			{
				string text = "V protokole " + lbl.Text + " je dátum vystavenia protokolu zadaný a evidenčné čislo nie je," +
					"chcete pokračovať v ukladaní?";
				Console.WriteLine(text);
				DialogResult dr = MessageBox.Show(text, "POZOR",MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
				if (dr == DialogResult.No)
				{
					Console.WriteLine("NEUKLADAŤ");
					return "NEUKLADAŤ";
				}
			}
		}
		#endregion
		
		#region Ev. číslo vyplnené prístupne, dátum nevyplnený resp. dátum vyplnený neprístupný
		else if((vystup[0] == true)&&(vystup[1] == true))
		{
			if ((vystup[2] == false) && (vystup[3] == true))
			{
				string text = "Pravdepodobne ste zabudli dopísať dátum protokolu " + lbl.Text.Replace(":","")
					+ ", chcete aj tak uložiť?";
				Console.WriteLine(text);
				DialogResult dr = MessageBox.Show(text, "POZOR",MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
				if (dr == DialogResult.Yes)
				{
					pom = txbCislo.Name + " = '" + txbCislo.Text + "'";
					Console.WriteLine(pom);
					return pom;
				}
				else
				{
					Console.WriteLine("NEUKLADAŤ");
					return "NEUKLADAŤ";
				}
			}
			if ((vystup[2] == true) && (vystup[3] == false))
			{
				string text = "V protokole " + lbl.Text + " ste upravili iba evidenčné čislo, dátum je bez úpravy, chcete pokračovať v ukladaní?";
				Console.WriteLine(text);
				DialogResult dr = MessageBox.Show(text, "POZOR",MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
				if (dr == DialogResult.Yes)
				{
					pom = txbCislo.Name + " = '" + txbCislo.Text + "'";
					Console.WriteLine(pom);
					return pom;
				}
				else
				{
					Console.WriteLine("NEUKLADAŤ");
					return "NEUKLADAŤ";
				}
			}
		}
		#endregion
		
		#region Ev. číslo vyplnené neprístupne, dátum nevyplnený resp. dátum vyplnený prístupný
		else if((vystup[0] == true)&&(vystup[1] == false))
		{
			if ((vystup[2] == false) && (vystup[3] == true))
			{
				string text = "Pravdepodobne ste zabudli dopísať dátum protokolu " + lbl.Text.Replace(":","")
					+ ", chcete pokračovať v ukladaní?";
				Console.WriteLine(text);
				DialogResult dr = MessageBox.Show(text, "POZOR",MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
				if (dr == DialogResult.No)
				{
					Console.WriteLine("NEUKLADAŤ");
					return "NEUKLADAŤ";// OŠETRIŤ - aby sa neuložilo - aby sa prerušilo ukladanie
				}
			}
			if ((vystup[2] == true) && (vystup[3] == true))
			{
				string text = "V protokole " + lbl.Text + " ste upravili iba dátum, evidenčné čislo je bez úpravy, chcete pokračovať v ukladaní?";
				Console.WriteLine(text);
				DialogResult dr = MessageBox.Show(text, "POZOR",MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
				if (dr == DialogResult.Yes)
				{
					pom = txbDatum.Name + " = '" + txbDatum.Text + "'";
					Console.WriteLine(pom);
					return pom;
				}
				else
				{
					Console.WriteLine("NEUKLADAŤ");
					return "NEUKLADAŤ";
				}
			}
		}
		#endregion

		return "";
	}
	
	public void txb_Click(object sender, EventArgs e)
	{
		if (((TextBox) sender).ReadOnly == true)
		{
			DialogResult dr = MessageBox.Show("Skutočne chceš prepísať túto bunku?", "POZOR",
			                                  MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
			if (dr == DialogResult.Yes)
			{
				((TextBox) sender).ReadOnly = false;
			}
		}
	}
}