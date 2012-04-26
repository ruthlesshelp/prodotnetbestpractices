using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Globalization;

using PeopleNS;
using PersonInformationNS;
using OperationNS;

[assembly:CLSCompliant(true)]
namespace PeopleTrax
{
	public class Form1 : System.Windows.Forms.Form
	{
        private readonly string outputFileName = ConfigurationManager.AppSettings.Get("outputFileName");
        private readonly int numPeople = Int32.Parse(ConfigurationManager.AppSettings.Get("numPeople"), CultureInfo.CurrentCulture);
        private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button GetPeopleButton;
		private System.Windows.Forms.ListView peopleList;
		private System.Windows.Forms.ColumnHeader fullName;
		private System.Windows.Forms.ColumnHeader companyName;
		private System.Windows.Forms.Button ExportToExcel;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.GetPeopleButton = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.ExportToExcel = new System.Windows.Forms.Button();
			this.peopleList = new System.Windows.Forms.ListView();
			this.fullName = new System.Windows.Forms.ColumnHeader("");
			this.companyName = new System.Windows.Forms.ColumnHeader("");
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
// 
// GetPeopleButton
// 
			this.GetPeopleButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.GetPeopleButton.ForeColor = System.Drawing.SystemColors.ControlText;
			this.GetPeopleButton.Location = new System.Drawing.Point(5, 376);
			this.GetPeopleButton.Name = "GetPeopleButton";
			this.GetPeopleButton.Size = new System.Drawing.Size(103, 23);
			this.GetPeopleButton.TabIndex = 0;
			this.GetPeopleButton.Text = "Get People";
			this.GetPeopleButton.Click += new System.EventHandler(this.GetPeopleButton_Click);
// 
// groupBox1
// 
			this.groupBox1.Controls.Add(this.ExportToExcel);
			this.groupBox1.Controls.Add(this.peopleList);
			this.groupBox1.Controls.Add(this.GetPeopleButton);
			this.groupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
			this.groupBox1.Location = new System.Drawing.Point(10, 7);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(527, 407);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "People";
// 
// ExportToExcel
// 
			this.ExportToExcel.ForeColor = System.Drawing.SystemColors.ControlText;
			this.ExportToExcel.Location = new System.Drawing.Point(117, 376);
			this.ExportToExcel.Name = "ExportToExcel";
			this.ExportToExcel.Size = new System.Drawing.Size(103, 23);
			this.ExportToExcel.TabIndex = 5;
			this.ExportToExcel.Text = "Export Data";
			this.ExportToExcel.Click += new System.EventHandler(this.ExportToExcel_Click);
// 
// peopleList
// 
			this.peopleList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.fullName,
            this.companyName});
			this.peopleList.FullRowSelect = true;
			this.peopleList.GridLines = true;
			this.peopleList.Location = new System.Drawing.Point(5, 21);
			this.peopleList.Name = "peopleList";
			this.peopleList.Size = new System.Drawing.Size(512, 349);
			this.peopleList.TabIndex = 4;
			this.peopleList.View = System.Windows.Forms.View.Details;
// 
// fullName
// 
			this.fullName.Text = "Full Name";
			this.fullName.Width = 200;
// 
// companyName
// 
			this.companyName.Text = "Company Name";
			this.companyName.Width = 310;

// Operation
			OperationNS.OperationControl.GetInstance().Location = new System.Drawing.Point(10, 417);
// 
// Form1
// 


			
            this.AutoScaleDimensions= new System.Drawing.SizeF(5, 13);
			this.ClientSize = new System.Drawing.Size(546, 451);
			this.Controls.Add(OperationNS.OperationControl.GetInstance());
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Form1";
			this.Text = "PeopleTrax";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.Run(new Form1());
		}

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Left = (Screen.PrimaryScreen.WorkingArea.Width / 2) - (this.Width / 2);
        }

#if OPTIMIZED_GETPEOPLE
		private void GetPeopleButton_Click(object sender, System.EventArgs e)
		{
			OperationControl.GetInstance().Start("Get People", numPeople);
			People people = new People();
			this.peopleList.Items.Clear();

			// use BeginUpdate to minimize repainting
			this.peopleList.BeginUpdate();
			foreach (PersonInformation person in people.GetPeople(this.numPeople))
			{
				ListViewItem personItem = new ListViewItem(new string[] { person.FullName, person.Employer });
				
				// use enumerated value instead of literals
				personItem.ForeColor = Color.Blue;
				
				this.peopleList.Items.Add(personItem);
				OperationControl.GetInstance().Increment(1);
			}
			// call EndUpdate to do one repaint
			this.peopleList.EndUpdate();

			OperationControl.GetInstance().Stop();
		}
#else
		private void GetPeopleButton_Click(object sender, System.EventArgs e)
		{
			OperationControl.GetInstance().Start("Get People", numPeople);
			People people = new People();
			this.peopleList.Items.Clear();

			foreach (PersonInformation person in people.GetPeople(this.numPeople))
			{
				ListViewItem personItem = new ListViewItem(new string[] { person.FullName, person.Employer });
				personItem.ForeColor = Color.FromName("Blue");
				this.peopleList.Items.Add(personItem);
				OperationControl.GetInstance().Increment(1);
			}
			OperationControl.GetInstance().Stop();
			
		}
#endif

#if OPTIMIZED_EXPORTDATA
		private string ExportData()
		{
			// use stringbuilder instead of +=, which translates to string.concat in IL
			StringBuilder builder = new StringBuilder();
			foreach (ListViewItem item in this.peopleList.Items)
			{
				builder.Append(item.SubItems[0].Text);
				builder.Append(",");
				builder.Append(item.SubItems[1].Text);
				builder.Append("\r\n");
				OperationControl.GetInstance().Increment(1);
			}
			return builder.ToString();
		}
#else
		private string ExportData()
		{
			string data = "";
			foreach (ListViewItem item in this.peopleList.Items)
			{
				data += item.SubItems[0].Text;
				data += ",";
				data += item.SubItems[1].Text;
				data += "\r\n";
				OperationControl.GetInstance().Increment(1);
			}
			return data;
		}
#endif

		private void ExportToExcel_Click(object sender, System.EventArgs e)
		{
			OperationControl.GetInstance().Start("Export To Excel", this.numPeople);
			
			//
			// Get our page data
			//
			string data = ExportData();
			
			//
			// Write this out to a temporary file
			//
			string outputFile = string.Format(CultureInfo.CurrentCulture, "{0}\\{1}", Path.GetTempPath(), outputFileName);
			
			using( StreamWriter writer = new StreamWriter(outputFile))
            {
			    writer.WriteLine(data);
            }
			//writer.Close();

			OperationControl.GetInstance().Stop();

			//
			// 'Start' this file
			//
            using (Process exportProcess = new Process())
            {
                exportProcess.StartInfo.FileName = outputFile;
                exportProcess.StartInfo.UseShellExecute = true;
                exportProcess.Start();
            }
		}
	}
}
