using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ArduinoPrompt
{
    static class ExcelSaver
    {
        public static void SaveDataToExcel(string path, Variable[] variables)
        {
            Application xlApp;
            Workbook wbook;
            Worksheet wsheet;
            
            xlApp = new Application();
            wbook = xlApp.Workbooks.Add(Missing.Value);
            wsheet = wbook.ActiveSheet;

            ChartObjects xlCharts = wsheet.ChartObjects(Type.Missing);
            ChartObject chart = xlCharts.Add(10, 80, 300, 250);
            Chart chartPage = chart.Chart;

            chartPage.ChartType = XlChartType.xlLine;
            SeriesCollection collection = chartPage.SeriesCollection();

            int col = 1;
            for (int i = 0; i < variables.Length; i++)
            {
                if (variables[i].plotValues.Count == 0) { continue; }

                wsheet.Cells[1, col] = "Time";
                wsheet.Cells[1, col + 1] = variables[i].name;

                for (int j = 0; j < variables[i].plotValues.Count; j++)
                {
                    wsheet.Cells[j + 2, col] = variables[i].plotValues[j].X;
                    wsheet.Cells[j + 2, col + 1] = variables[i].plotValues[j].Y;
                }

                Series s = collection.NewSeries();
                s.Smooth = true;
                s.Name = variables[i].name;
                s.XValues = wsheet.Range[VecToCell(col, 2), VecToCell(col, variables[i].plotValues.Count + 1)];
                s.Values = wsheet.Range[VecToCell(col + 1, 2), VecToCell(col + 1, variables[i].plotValues.Count + 1)];

                int color = colorToRGB(variables[i].plotColor);
                s.Format.Line.ForeColor.RGB = color;

                col += 3;
            }

            wsheet.SaveAs(path);
            wbook.Close(true, Missing.Value, Missing.Value);
            xlApp.Quit();

            Marshal.ReleaseComObject(wsheet);
            Marshal.ReleaseComObject(wbook);
            Marshal.ReleaseComObject(xlApp);
        }

        private static string VecToCell(int col, int row)
        {
            const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            string s = ""; col--;
            do
            {
                s = alphabet[col % 26] + s;
                col /= 26;
            } while (col != 0);
            return s + row.ToString();
        }

        private static int colorToRGB(Color col)
        {
            return (col.R << 0x10) | (col.G << 0x08) | col.B;
        }
    }
}
