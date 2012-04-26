using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Data;
using System.Windows.Forms;

[assembly:CLSCompliant(true)]
namespace OperationNS
{
	/// <summary>
	/// This class is a singleton that illustrates our progress
	/// </summary>
	public partial class OperationControl : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Label StatusText;
		private System.Windows.Forms.ProgressBar StatusProgress;
		private System.ComponentModel.Container components = null;
		private DateTime operationStart;
		private string operationName;

		private static OperationControl instance = null;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public static OperationControl GetInstance()
		{
			if (OperationControl.instance == null)
			{
				OperationControl.instance = new OperationControl();
			}

			return OperationControl.instance;
		}

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1500:VariableNamesShouldNotMatchFieldNames", MessageId = "operationName")]
        public void Start(string operationName, int startingMaximum)
		{
			this.StatusText.Text = string.Format(CultureInfo.CurrentCulture, "Starting '{0}' ...", operationName);

			this.operationName = operationName;
			this.Maximum = startingMaximum;
			this.operationStart = DateTime.Now;
		}

		public void Stop()
		{
			TimeSpan duration = DateTime.Now - this.operationStart;

			this.StatusText.Text = string.Format(CultureInfo.CurrentCulture,"'{0}' took {1} seconds", this.operationName, duration.TotalSeconds);
			this.StatusProgress.Maximum = 0;
		}

		public int Maximum
		{
			get { return this.StatusProgress.Maximum; }
			set { this.StatusProgress.Maximum = value; }
		}
		
		public void Increment(int step)
		{
			this.StatusProgress.Increment(step);
		}

		private OperationControl()
		{
			InitializeComponent();
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.StatusProgress = new System.Windows.Forms.ProgressBar();
			this.StatusText = new System.Windows.Forms.Label();
			this.SuspendLayout();

// 
// StatusProgress
// 
			this.StatusProgress.ForeColor = System.Drawing.Color.FromArgb(((byte)(0)), ((byte)(192)), ((byte)(0)));
			this.StatusProgress.Location = new System.Drawing.Point(5, 5);
			this.StatusProgress.Name = "StatusProgress";
			this.StatusProgress.Size = new System.Drawing.Size(165, 23);
			this.StatusProgress.Step = 1;
			this.StatusProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.StatusProgress.TabIndex = 0;

// 
// StatusText
// 
			this.StatusText.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.StatusText.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.StatusText.ForeColor = System.Drawing.SystemColors.ActiveCaption;
			this.StatusText.Location = new System.Drawing.Point(173, 5);
			this.StatusText.Name = "StatusText";
			this.StatusText.Size = new System.Drawing.Size(353, 23);
			this.StatusText.TabIndex = 1;
			this.StatusText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

// 
// UserControl1
// 
			this.Controls.Add(this.StatusText);
			this.Controls.Add(this.StatusProgress);
			this.Name = "UserControl1";
			this.Size = new System.Drawing.Size(542, 34);
			this.ResumeLayout(false);
		}
		#endregion
	}
}
