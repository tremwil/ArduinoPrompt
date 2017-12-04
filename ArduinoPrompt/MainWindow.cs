using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO.Ports;

namespace ArduinoPrompt
{
    public partial class MainWindow : Form
    {
        private bool nameExists = false;
        private bool isMonitoring = false;

        private SerialPort arduinoPort;
        private string currentLine;

        private IntPtr consoleHandle;

        public MainWindow()
        {
            InitializeComponent();
            canvas.MouseDown += canvas_MouseDown;
            canvas.MouseUp += canvas_MouseUp;
            canvas.MouseMove += canvas_MouseMove;
            canvas.MouseWheel += canvas_MouseWheel;
            canvas.MouseDoubleClick += canvas_MouseDoubleClick;

            WriteLineColor("Arduino Plotter 1.0 - Console\n", ConsoleColor.DarkCyan);
            arduinoPort = null;

            variablePicker.Items.AddRange(new object[] {
                new Variable("[default]", Color.Black),
                new Variable("Add new...", Color.Black)
            });

            btnDetect_Click(this, new EventArgs());
            serialChoice.SelectedIndex = 0;
            variablePicker.SelectedIndex = 0;
            
            // Disable close button
            consoleHandle = GetConsoleWindow();
            IntPtr hMenu = GetSystemMenu(consoleHandle, false);
            EnableMenuItem(hMenu, 0xF060, 0x01);

            // Make window layered & transparent
            int newLong = GetWindowLong(consoleHandle, -20) | 0x80000;
            SetWindowLong(consoleHandle, -20, newLong);
            SetLayeredWindowAttributes(consoleHandle, 0x00, 0xD0, 0x02);

            // Hide window
            ShowWindow(consoleHandle, 0x00);

            // Disable CTRL+C and CTRL+BREAK
            SetConsoleCtrlHandler(dwCtrlType => dwCtrlType == 0 || dwCtrlType == 1, true);
        }

        private void WriteColor(object obj, ConsoleColor fg = ConsoleColor.Green, ConsoleColor bg = ConsoleColor.Black)
        {
            ConsoleColor tempBg = Console.BackgroundColor, tempFg = Console.ForegroundColor;

            Console.BackgroundColor = bg;
            Console.ForegroundColor = fg;

            Console.Write(obj);

            Console.BackgroundColor = tempBg;
            Console.ForegroundColor = tempFg;
        }

        private void WriteLineColor(object obj, ConsoleColor fg = ConsoleColor.Green, ConsoleColor bg = ConsoleColor.Black)
        {
            ConsoleColor tempBg = Console.BackgroundColor, tempFg = Console.ForegroundColor;

            Console.BackgroundColor = bg;
            Console.ForegroundColor = fg;

            Console.WriteLine(obj);

            Console.BackgroundColor = tempBg;
            Console.ForegroundColor = tempFg;
        }

        private void btnDetect_Click(object sender, EventArgs e)
        {
            List<string> ports = ArduinoDetector.GetArduinoPorts();
            WriteLineColor($"[Serial] Found {ports.Count} Arduinos connected", ConsoleColor.White);

            if (ports.Count == 0)
            {
                ports.Add("None");
            }

            serialChoice.Items.Clear();
            serialChoice.Items.AddRange(ports.ToArray());
            serialChoice.SelectedIndex = 0;

            btnMonitor.Enabled = ports[0] != "None";
        }

        private void showConsole_CheckedChanged(object sender, EventArgs e)
        {
            ShowWindow(consoleHandle, showConsole.Checked ? 0x05 : 0x00);
        }

        private void variablePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Variable item = (Variable)variablePicker.SelectedItem;
            if (item.name == "Add new...")
            {
                uint uniqueNum = 1;
                for (int i = 0; i < variablePicker.Items.Count; i++)
                {
                    Variable var = (Variable)variablePicker.Items[i];
                    if (var.name.StartsWith("var") && uint.TryParse(var.name.Substring(3), out uint num) && num >= uniqueNum)
                    {
                        uniqueNum = num + 1;
                    }
                }

                variablePicker.Items.Insert(variablePicker.Items.Count - 1,
                    new Variable("var" + uniqueNum.ToString(), Color.Black));

                variablePicker.SelectedIndex = variablePicker.Items.Count - 2;
                return;
            }

            variableInput.Enabled = variablePicker.SelectedIndex != 0;
            variableInput.Text = item.name;

            btnChangeColor.BackColor = item.plotColor;
            btnChangeColor.ForeColor = InvertColor(item.plotColor);
        }

        private void btnChangeColor_Click(object sender, EventArgs e)
        {
            if (colorPicker.ShowDialog() == DialogResult.OK)
            {
                Variable item = (Variable)variablePicker.SelectedItem;
                item.plotColor = colorPicker.Color;
                btnChangeColor.BackColor = colorPicker.Color;
                btnChangeColor.ForeColor = InvertColor(colorPicker.Color);
            }
        }

        private Color InvertColor(Color c)
        {
            return (Math.Abs(c.R - 128) < 30 && Math.Abs(c.G - 128) < 30 && Math.Abs(c.B - 128) < 30) ?
                Color.White : Color.FromArgb(255 - c.R, 255 - c.G, 255 - c.B);
        }

        private void variableInput_TextChanged(object sender, EventArgs e)
        {
            if (variablePicker.SelectedIndex != 0)
            {
                nameExists = false;
                for (int i = 0; i < variablePicker.Items.Count; i++)
                {
                    Variable var = (Variable)variablePicker.Items[i];
                    if (i != variablePicker.SelectedIndex && var.name == variableInput.Text)
                    {
                        nameExists = true;
                        break;
                    }
                }
                variableInput.ForeColor = nameExists ? Color.DarkRed : Color.Black;
            }
        }

        private void HandleVarInputKeydown(TextBox textBox, KeyEventArgs e)
        {
            char chr = Convert.ToChar(MapVirtualKey((uint)e.KeyValue, 0x02));

            if (!char.IsControl(chr))
            {
                if (!char.IsLetterOrDigit(chr) && !e.Modifiers.HasFlag(Keys.Control) && !e.Modifiers.HasFlag(Keys.Alt))
                {
                    e.SuppressKeyPress = true;
                    return;
                }
                else if (textBox.SelectionStart == 0 && !char.IsLetter(chr))
                {
                    e.SuppressKeyPress = true;
                    return;
                }
            }

            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete && textBox.SelectionStart == 0)
            {
                if (textBox.Text.Length == textBox.SelectionLength) { return; }
                e.SuppressKeyPress = !char.IsLetter(textBox.Text[textBox.SelectionLength]);
            }
        }

        private void variableInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (variableInput.Text == "")
                {
                    int index = variablePicker.SelectedIndex;
                    variablePicker.Items.RemoveAt(index);
                    variablePicker.SelectedIndex = (index == variablePicker.Items.Count - 1) ? index - 1 : index;
                    variablePicker.Refresh();
                    return;
                }
                if (!nameExists)
                {
                    Variable var = (Variable)variablePicker.SelectedItem;
                    var.name = variableInput.Text;
                    variablePicker.Items[variablePicker.SelectedIndex] = var;
                }

                e.SuppressKeyPress = true;
            }

            HandleVarInputKeydown(variableInput, e);
        }

        private void OnDataRecieved(object sender, SerialDataReceivedEventArgs e)
        {
            string[] incoming = arduinoPort.ReadExisting().Split('\n');
            for (int i = 0; i < incoming.Length; i++)
            {
                if (i == 0) { incoming[0] = currentLine + incoming[0]; }
                if (i + 1 == incoming.Length) { currentLine = incoming[i]; }
                else
                {
                    if (graphPrefix.Text != "" && incoming[i].StartsWith(graphPrefix.Text))
                    {
                        string svar;
                        PointF vec = new PointF();

                        try
                        {
                            string[] split = incoming[i].Split(' ');
                            string[] cmd = split[0].Split(':');

                            svar = (cmd.Length == 1) ? "[default]" : cmd[1];
                            vec.X = float.Parse(split[1]);
                            vec.Y = float.Parse(split[2]);

                            GetVariableByName(svar).plotValues.Add(vec);
                            canvas.Invalidate();
                        }
                        catch (Exception err)
                        {
                            WriteLineColor($"[Arduiono] Misformed plot command, got {err.GetType()}", ConsoleColor.Red);
                        }
                        if (logGraphCall.Checked)
                        {
                            WriteLineColor($"[Arduino] {incoming[i]}", ConsoleColor.DarkYellow);
                        }
                        continue;
                    }
                    WriteLineColor($"[Arduino] {incoming[i]}", ConsoleColor.Yellow);
                }
            }
        }

        private void graphPrefix_KeyDown(object sender, KeyEventArgs e)
        {
            HandleVarInputKeydown(graphPrefix, e);
        }

        private void serialChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (serialChoice.SelectedItem.ToString() != "None")
            {
                string port = serialChoice.SelectedItem.ToString();
                arduinoPort = new SerialPort(port, int.Parse(baudRateInput.Text));
                arduinoPort.DataReceived += OnDataRecieved;
                WriteLineColor($"[Serial] Connected to Arduino on port {port}", ConsoleColor.Green);
            }
            else
            {
                if (arduinoPort != null) { arduinoPort.Dispose(); }
                WriteLineColor($"[Serial] No Arduino connected", ConsoleColor.Red);
            }
        }

        private void btnMonitor_Click(object sender, EventArgs e)
        {
            try
            {
                if (isMonitoring) { arduinoPort.Close(); }
                else
                {
                    arduinoPort.Open();
                    arduinoPort.DiscardInBuffer();
                    arduinoPort.BaseStream.Flush();
                }

                string s = isMonitoring ? "stopped" : "started";
                WriteLineColor($"[Serial] Monitoring {s}", ConsoleColor.Cyan);
            }
            catch (Exception err)
            {
                if (!showConsole.Checked)
                {
                    DialogResult res = MessageBox.Show($"Got error {err.GetType()}. See console for more information.",
                         "FATAL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                WriteLineColor($"[Serial] FATAL ERROR: {err.GetType()}", ConsoleColor.Red, ConsoleColor.White);
                WriteLineColor($"{err.Message}\n{err.StackTrace}", ConsoleColor.Red);
                return;
            }

            btnDetect.Enabled = isMonitoring;
            btnChangeColor.Enabled = isMonitoring;
            btnExcel.Enabled = isMonitoring;

            serialChoice.Enabled = isMonitoring;
            variablePicker.Enabled = isMonitoring;
            variableInput.Enabled = isMonitoring;
            graphPrefix.Enabled = isMonitoring;
            baudRateInput.Enabled = isMonitoring;

            isMonitoring = !isMonitoring;
            btnMonitor.Text = isMonitoring ? "Stop Monitoring" : "Begin Monitoring";
        }

        private Variable GetVariableByName(string name)
        {
            if (name == "Add new...") { return null; }

            foreach (object obj in variablePicker.Items)
            {
                Variable var = (Variable)obj;
                if (var.name == name) { return var; }
            }

            return null;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Console.Clear();
            WriteLineColor("Arduino Plotter 1.0 - Console\n", ConsoleColor.DarkCyan);

            foreach (object obj in variablePicker.Items)
            {
                Variable var = (Variable)obj;
                if (var != null) var.plotValues.Clear();
            }

            canvas.Invalidate();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            saveDialog.AddExtension = true;
            saveDialog.DefaultExt = ".xlsx";

            DialogResult result = saveDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    Variable[] vars = new Variable[variablePicker.Items.Count];
                    variablePicker.Items.CopyTo(vars, 0);
                    ExcelSaver.SaveDataToExcel(saveDialog.FileName, vars);
                }
                catch (Exception err)
                {
                    if (!showConsole.Checked)
                    {
                        DialogResult res = MessageBox.Show($"Got error {err.GetType()}. See console for more information.",
                             "FATAL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    WriteLineColor($"[Excel] FATAL ERROR: {err.GetType()}", ConsoleColor.Red, ConsoleColor.White);
                    WriteLineColor($"{err.Message}\n{err.StackTrace}", ConsoleColor.Red);
                    return;
                }

                MessageBox.Show("File was saved!", "Arduino Prompt");
            }
        }

        private void baudRateInput_KeyDown(object sender, KeyEventArgs e)
        {
            char chr = Convert.ToChar(MapVirtualKey((uint)e.KeyValue, 0x02));

            if (!char.IsControl(chr) && !char.IsDigit(chr))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void baudRateInput_Leave(object sender, EventArgs e)
        {
            if (!int.TryParse(baudRateInput.Text, out int baud) || baud == 0)
            {
                baudRateInput.Text = "9600";
                if (arduinoPort != null)
                {
                    arduinoPort.BaudRate = 9600;
                }
            }
            else if (arduinoPort != null)
            {
                arduinoPort.BaudRate = baud;
            }
        }

        private delegate bool PHandlerRountine(uint dwCtrlType);

        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleCtrlHandler(PHandlerRountine handlerRoutine, bool add);
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hwnd, bool bRevert);
        [DllImport("user32.dll")]
        private static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);
        [DllImport("user32.dll")]
        private static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);
        [DllImport("user32.dll")]
        private static extern int MapVirtualKey(uint uCode, uint uMapType);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hwnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hwnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    }
}
