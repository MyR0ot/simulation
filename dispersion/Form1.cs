using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dispersion
{
	public partial class Form1 : Form
	{
		private int amountSeries;
		private int amountEx;
		public Form1(int n)
		{
			InitializeComponent();
			amountSeries = 0;
			amountEx = n;
		}

		private void chart1_Click(object sender, EventArgs e)
		{
			if (amountSeries == 5)
			{
				amountSeries = 0;
				chart1.Invalidate();
				for(int i=0; i < 5; i++)
					chart1.Series[i].Points.Clear();
			}
			var data = new Simulation(amountEx, 100, 0).GetData();
			for (int i = 0; i < data.Length; i++)
				chart1.Series[amountSeries].Points.AddY(data[i]);
			amountSeries++;
		}

		private void chart1_Load(object sender, EventArgs e)
		{
			//chart1.ChartAreas[0].AxisX.ScaleView.Zoom(0, amountEx);
			//chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
			//chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
			//chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
			//chart1.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
				//var data = new Simulation(amountEx, 50, 0).GetData();
				//for (int i = 0; i < data.Length; i++)
				//	chart1.Series[amountSeries].Points.AddY(data[i]);
				//amountSeries++;
		}
	}
}