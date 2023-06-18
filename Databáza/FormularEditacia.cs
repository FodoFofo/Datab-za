/*
 * Vytvořeno aplikací SharpDevelop.
 * Uživatel: fodor
 * Datum: 6.2.2013
 * Čas: 11:30
 */

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

public class FormUprava : Form
{
	private Button btnProtokoly;
	
	public FormUprava()
	{
		Text = "Úprava údajov";
		Width = 1000;
		Height = 500;
		
		btnProtokoly = new Button();
		btnProtokoly.Location = new Point(10,10);
		btnProtokoly.AutoSize = true;
		btnProtokoly.Text = "Editácia\nprotokolov";
		btnProtokoly.Parent = this;
		btnProtokoly.Click += new EventHandler(btnProtokoly_Click);
	}
	
	private void btnProtokoly_Click(object sender, EventArgs e)
	{
		FormEditPRT frmEditPRT = new FormEditPRT();
		frmEditPRT.Show();
	}
}