/*
 * Vytvořeno aplikací SharpDevelop.
 * Uživatel: fodor
 * Datum: 16.1.2013
 * Čas: 11:28
 */

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

public class FormZariadenie : Form
{
	Font font = new Font("Verdana", 10, FontStyle.Bold);
	Font font1 = new Font("Verdana", 8, FontStyle.Bold);
	Color farbaPopis = Color.Red;
	Color farbaData = Color.DarkBlue;
	DataSet mojDataSet = new DataSet();
	DataTable mojaDataTable = new DataTable();
	#region Deklarácia prvkov na formuláry
	Label lblSJZ = new Label();
	Label lblTyp = new Label();
	Label lblBlok = new Label();
	Label lblLot = new Label();
	Label lblDPSPS = new Label();
	Label lblObjekt = new Label();
	Label lblMiestnost = new Label();
	Label lblSC = new Label();
	Label lblSeizmicita = new Label();
	Label lblITPQCP = new Label();
	Label lblCEQP = new Label();
	Label lblVyhradeneTechnickeZariadenie = new Label();
	Label lblVyrobneCislo = new Label();
	Label lblRokVyroby = new Label();
	Label lblPocePoli = new Label();
	Label lblZakazkoveCislo = new Label();
	Label lblPTS = new Label();
	Label lblIP = new Label();
	Label lblSLD = new Label();
	Label lblGA = new Label();
	Label lblWD = new Label();
	Label lblITProgram = new Label();
	Label lblSTD = new Label();
	Label lblProtokoly = new Label();
	Label lblVK = new Label();
	Label lblSP = new Label();
	Label lblMK = new Label();
	Label lblUM = new Label();
	Label lblIT = new Label();
	Label lblREV = new Label();
	Label lblprtSTD = new Label();
	
	Label lblTypData = new Label();
	Label lblBlokData = new Label();
	Label lblLotData = new Label();
	Label lblDPSPSData = new Label();
	Label lblObjektData = new Label();
	Label lblMiestnostData = new Label();
	Label lblSCData = new Label();
	Label lblSeizmicitaData = new Label();
	Label lblITPQCPData = new Label();
	Label lblCEQPData = new Label();
	Label lblVyhradeneTechnickeZariadenieData = new Label();
	Label lblVyrobneCisloData = new Label();
	Label lblRokVyrobyData = new Label();
	Label lblPocePoliData = new Label();
	Label lblZakazkoveCisloData = new Label();
	Label lblPTSData = new Label();
	Label lblIPData = new Label();
	Label lblSLDData = new Label();
	Label lblGAData = new Label();
	Label lblWDData = new Label();
	Label lblITProgramData = new Label();
	Label lblSTDData = new Label();
	Label lblVKData = new Label();
	Label lblSPData = new Label();
	Label lblMKData = new Label();
	Label lblUMData = new Label();
	Label lblITData = new Label();
	Label lblREVData = new Label();
	Label lblprtSTDData = new Label();
	
	ComboBox cmbSJZ = new ComboBox();
	#endregion
	Databaza databaza = new Databaza();
	int pozicia1 = 10;  // Pozícia prvého stĺpca popisov (SJZ, Typ, BLok, ...)
	int pozicia2 = 400;  // Pozícia druhého stĺpca popisov (Výrobne číslo, Rok výroby, ...)
	// string[ popis, názov stĺpca]
	string[,] strLabels = {{"SJZ:", "Typ:", "Blok:", "Lot:", "DPS/PS:", "Objekt:", "Miestnosť:", "SC", "VTZ:", "Výr. číslo:", "Rok výroby:", "Počet polí:", "Zák. číslo:", "IP:", "Seizmicita:", "ITP/QCP výr.:", "ITP/QCP:", "CEQP:", "PTS:", "SLD:", "GA:", "WD:", "IT program:", "STD:"},
		{"SJZ", "Type_of_device", "Blok", "LOT", "PS_DPS", "Objekt", "Miestnosť","Safety_class", "Vyhradené_zariadenie_druh", "Výrobné_číslo", "Rok_výroby", "Počet_polí", "Zákazkové_číslo", "IP", "Seizmicita", "ITP_QCP_Manufacturing", "ITP_QCP", "PLKVZ_CEQP", "PTS", "SLD", "GA", "WD", "IT_program", "ATD_No"}};
	string[,] strPRT = {{"PROTOKOLY:", "VK:", "SP:", "MK:", "UM:", "IT:", "REV:", "STD:"},
		{"", "Dátum_VK", "Dátum_SP", "Dátum_MK", "Dátum_UM", "Dátum_IT", "Dátum_REV", "Dátum_STD"},
		{"", "PRT_VK", "PRT_SP", "PRT_MK", "PRT_UM", "PRT_IT", "PRT_REV", "PRT_STD"}};
	Label[] lbl = new Label[30];
	Label[] lblData = new Label[30];
	Label[] lblPRT = new Label[8];
	Label[] lblPRTData = new Label[7];
	
	public FormZariadenie()
	{
		Text = "Detaily";
		Width = 1000;
		Height = 500;
		MaximizeBox = false;
		MinimizeBox = false;
		ResizeRedraw = false;
		
		#region Výpis Labels na formulár
		int dlzkaLbl1 = 0;
		int dlzkaLbl2 = 0;
		int pocetPrvkovVStlpci = ((ClientSize.Height-40)-((ClientSize.Height-40)%(font1.Height + 14)))/(font1.Height + 14);

		for (int i = 0; i < strLabels.GetLength(1); i++)
		{
			lbl[i] = new Label();
			// Farba písma
			lbl[i].ForeColor = farbaPopis;
			// Automatická zmena veľkosti - podľa obsahu
			lbl[i].AutoSize = true;
			// Font písma
			lbl[i].Font = font1;
			// Rodič prvku
			lbl[i].Parent = this;
			lbl[i].Text = strLabels[0,i];
	
			if (i < pocetPrvkovVStlpci)
			{
				if (i == 0)
				{
					lbl[i].Location = new Point(pozicia1, 40);
				}
				else
					lbl[i].Location = new Point(pozicia1, lbl[i-1].Bottom + 10);
				if (dlzkaLbl1 < lbl[i].Width)
					dlzkaLbl1 = lbl[i].Width;
			}
			else
			{
				if (i == pocetPrvkovVStlpci)
					lbl[i].Location = new Point(pozicia2, 10);
				else
					lbl[i].Location = new Point(pozicia2, lbl[i-1].Bottom + 10);
				if (dlzkaLbl2 < lbl[i].Width)
					dlzkaLbl2 = lbl[i].Width;
			}
		}
  		for (int i = 0; i < strLabels.GetLength(1); i++)
		{
			lblData[i] = new Label();
			// Farba písma
			lblData[i].ForeColor = farbaData;
			// Automatická zmena veľkosti - podľa obsahu
			lblData[i].AutoSize = true;
			// Font písma
			lblData[i].Font = font1;
			// Rodič prvku
			lblData[i].Parent = this;
			lblData[i].Click += new EventHandler(lbl_Click);
			
			if (i < pocetPrvkovVStlpci)
				lblData[i].Location = new Point(pozicia1 + dlzkaLbl1 + 10, lbl[i].Top);
			else
				lblData[i].Location = new Point(pozicia2 + dlzkaLbl2 + 10, lbl[i].Top);
		}
  		//PROTOKOLY
  		for (int i = 0; i < strPRT.GetLength(1); i++)
		{
  			lblPRT[i] = new Label();
			// Farba písma
			lblPRT[i].ForeColor = farbaPopis;
			// Automatická zmena veľkosti - podľa obsahu
			lblPRT[i].AutoSize = true;
			// Font písma
			lblPRT[i].Font = font1;
			// Rodič prvku
			lblPRT[i].Parent = this;
			lblPRT[i].Text = strPRT[0,i];
	
			if (i == 0)
				lblPRT[i].Location = new Point(700, 10);
			else
				lblPRT[i].Location = new Point(700, lblPRT[i-1].Bottom + 10);
		}
  		for (int i = 0; i < lblPRTData.Length; i++)
		{
			lblPRTData[i] = new Label();
			// Farba písma
			lblPRTData[i].ForeColor = farbaData;
			// Automatická zmena veľkosti - podľa obsahu
			lblPRTData[i].AutoSize = true;
			// Font písma
			lblPRTData[i].Font = font1;
			// Rodič prvku
			lblPRTData[i].Parent = this;
			lblPRTData[i].Click += new EventHandler(lbl_Click);
			
			lblPRTData[i].Location = new Point(lblPRT[7].Right + 10, lblPRT[i+1].Top);
		}
		#endregion
		
		mojDataSet = databaza.NaplnDataset("Zariadenia");
		mojaDataTable = mojDataSet.Tables["Zariadenia"];
		cmbSJZ.Parent = this;
		// Naplnenie ComboBoxu dátami zo stĺpca SJZ
		foreach (DataRow dr in mojaDataTable.Rows)
		{
			cmbSJZ.Items.Add(dr["SJZ"]);
		}
		cmbSJZ.SelectedIndexChanged += new EventHandler(cmbSJZ_SelectedIndexChanged);
		cmbSJZ.TextChanged += new EventHandler(cmbSJZ_TextChanged);
		cmbSJZ.Location = new Point(10,10);
		cmbSJZ.SelectedIndex = 0;
	}
	
	// Pri vybraní ďalšej položky zo zoznamu ComboBoxu
	private void cmbSJZ_SelectedIndexChanged(object sender, EventArgs e)
	{
		string pomSJZ = (string) cmbSJZ.SelectedItem;
		NaplnenieLabels(pomSJZ);
	}
	
	// Pri zmene tetu v ComboBoxe
	private void cmbSJZ_TextChanged(object sender, EventArgs e)
	{
		string pomSJZ = (string) cmbSJZ.Text.ToUpper();
		NaplnenieLabels(pomSJZ);
	}
	
	// Naplnenie Labels na karte dátami - vstupny parameter pomSJZ je SJZ zariadenia
	private void NaplnenieLabels(string pomSJZ)
	{
		// Naplnenie dát z DB
		for (int i = 0; i < strLabels.GetLength(1); i++)
		{
			lblData[i].Text = databaza.Napln(pomSJZ, strLabels[1,i], mojDataSet);
		}
		// Naplnenie dát z DB - Protokoly
		for (int i = 0; i < lblPRTData.Length; i++)
		{
			lblPRTData[i].Text = databaza.Napln(pomSJZ, strPRT[1,i+1], mojDataSet) + " - " + databaza.Napln(pomSJZ, strPRT[2,i+1], mojDataSet);
		}
	}
	
	// Vykoná skopírovanie textu po kliknutí na Label do schránky 
	private void lbl_Click(object sender, EventArgs e)
	{
		Clipboard.SetDataObject(((Label) sender).Text);
	}
}