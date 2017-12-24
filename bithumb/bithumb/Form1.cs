using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace bithumb
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            curveChart.Series.Clear();
            curveChart.Series.Add("price");
            curveChart.Series.Add("ma60");


            curveChart.Series["price"].XValueType = ChartValueType.DateTime;
            curveChart.Series["price"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            curveChart.Series["price"].BorderWidth = 3;

            curveChart.Series["ma60"].XValueType = ChartValueType.DateTime;
            curveChart.Series["ma60"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            curveChart.Series["ma60"].BorderWidth = 1;



            float min = 20000000;

            Dictionary<DateTime, int> dicTick = new Dictionary<DateTime, int>();

            string [] lines = File.ReadAllLines("trans.csv");
            foreach(string row in lines)
            {
                string [] cols  =  row.Split(new char[] { ',' });

                var parsedDate = DateTime.Parse(cols[0]);
                int price = 0;
                int.TryParse(cols[2], out price);

                dicTick[parsedDate] = price;

                int count = 0;
                int sum = 0;
                int ma60sec = 0;
                for (int i = 0; i < 60; i++)
                {

                    DateTime prev = parsedDate - new TimeSpan(0, 0, i);
                    if (dicTick.ContainsKey(prev))
                    {
                        count++;
                        sum += dicTick[prev];
                    }
                }

                if (count > 0)
                {
                    ma60sec = sum / count;
                    curveChart.Series["ma60"].Points.AddXY(parsedDate, ma60sec);
                }

                min = Math.Min(min, price);                

                //curveChart.ChartAreas["0"].AxisX.Interval = 1;
                curveChart.Series["price"].Points.AddXY(parsedDate, price);
                

                //curveChart.Series.Add("Series2");
                //curveChart.Series["Series2"].XValueType = ChartValueType.DateTime;
                //curveChart.Series["Series2"].Points.DataBind(list2, "MonthYear", "PriceValue", null);
                //curveChart.Series["Series2"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                //curveChart.Series["Series2"].BorderWidth = 3;
                //curveChart.ChartAreas["0"].AxisX.Interval = 1;
            }

            curveChart.ChartAreas[0].AxisY.Minimum = min;
            curveChart.Update();
        }
    }
}
