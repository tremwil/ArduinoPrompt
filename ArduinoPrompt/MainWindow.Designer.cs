namespace ArduinoPrompt
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.baudRateInput = new System.Windows.Forms.TextBox();
            this.logGraphCall = new System.Windows.Forms.CheckBox();
            this.showConsole = new System.Windows.Forms.CheckBox();
            this.graphPrefix = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDetect = new System.Windows.Forms.Button();
            this.serialChoice = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnMonitor = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.variableInput = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnChangeColor = new System.Windows.Forms.Button();
            this.variablePicker = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.colorPicker = new System.Windows.Forms.ColorDialog();
            this.saveDialog = new System.Windows.Forms.SaveFileDialog();
            this.canvas = new ArduinoPrompt.DrawingPanel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.baudRateInput);
            this.groupBox1.Controls.Add(this.logGraphCall);
            this.groupBox1.Controls.Add(this.showConsole);
            this.groupBox1.Controls.Add(this.graphPrefix);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnDetect);
            this.groupBox1.Controls.Add(this.serialChoice);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 329);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(142, 257);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 79);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "Baud Rate";
            // 
            // baudRateInput
            // 
            this.baudRateInput.Location = new System.Drawing.Point(10, 99);
            this.baudRateInput.Name = "baudRateInput";
            this.baudRateInput.Size = new System.Drawing.Size(121, 22);
            this.baudRateInput.TabIndex = 7;
            this.baudRateInput.Text = "9600";
            this.baudRateInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.baudRateInput_KeyDown);
            this.baudRateInput.Leave += new System.EventHandler(this.baudRateInput_Leave);
            // 
            // logGraphCall
            // 
            this.logGraphCall.AutoSize = true;
            this.logGraphCall.Location = new System.Drawing.Point(6, 229);
            this.logGraphCall.Name = "logGraphCall";
            this.logGraphCall.Size = new System.Drawing.Size(132, 21);
            this.logGraphCall.TabIndex = 6;
            this.logGraphCall.Text = "Log Graph Calls";
            this.logGraphCall.UseVisualStyleBackColor = true;
            // 
            // showConsole
            // 
            this.showConsole.AutoSize = true;
            this.showConsole.Location = new System.Drawing.Point(6, 208);
            this.showConsole.Name = "showConsole";
            this.showConsole.Size = new System.Drawing.Size(119, 21);
            this.showConsole.TabIndex = 5;
            this.showConsole.Text = "Show Console";
            this.showConsole.UseVisualStyleBackColor = true;
            this.showConsole.CheckedChanged += new System.EventHandler(this.showConsole_CheckedChanged);
            // 
            // graphPrefix
            // 
            this.graphPrefix.Location = new System.Drawing.Point(10, 180);
            this.graphPrefix.Name = "graphPrefix";
            this.graphPrefix.Size = new System.Drawing.Size(118, 22);
            this.graphPrefix.TabIndex = 4;
            this.graphPrefix.Text = "graph";
            this.graphPrefix.KeyDown += new System.Windows.Forms.KeyEventHandler(this.graphPrefix_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 160);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Graphing Prefix";
            // 
            // btnDetect
            // 
            this.btnDetect.Location = new System.Drawing.Point(9, 127);
            this.btnDetect.Name = "btnDetect";
            this.btnDetect.Size = new System.Drawing.Size(121, 23);
            this.btnDetect.TabIndex = 2;
            this.btnDetect.Text = "Re-Detect Ports";
            this.btnDetect.UseVisualStyleBackColor = true;
            this.btnDetect.Click += new System.EventHandler(this.btnDetect_Click);
            // 
            // serialChoice
            // 
            this.serialChoice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.serialChoice.FormattingEnabled = true;
            this.serialChoice.Items.AddRange(new object[] {
            "None"});
            this.serialChoice.Location = new System.Drawing.Point(9, 52);
            this.serialChoice.Name = "serialChoice";
            this.serialChoice.Size = new System.Drawing.Size(121, 24);
            this.serialChoice.TabIndex = 1;
            this.serialChoice.SelectedIndexChanged += new System.EventHandler(this.serialChoice_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Target Serial Port";
            // 
            // btnMonitor
            // 
            this.btnMonitor.Enabled = false;
            this.btnMonitor.Location = new System.Drawing.Point(6, 21);
            this.btnMonitor.Name = "btnMonitor";
            this.btnMonitor.Size = new System.Drawing.Size(129, 25);
            this.btnMonitor.TabIndex = 2;
            this.btnMonitor.Text = "Begin Monitoring";
            this.btnMonitor.UseVisualStyleBackColor = true;
            this.btnMonitor.Click += new System.EventHandler(this.btnMonitor_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.Location = new System.Drawing.Point(7, 81);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(129, 43);
            this.btnExcel.TabIndex = 3;
            this.btnExcel.Text = "Export graph data to Excel";
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnClear);
            this.groupBox2.Controls.Add(this.btnMonitor);
            this.groupBox2.Controls.Add(this.btnExcel);
            this.groupBox2.Location = new System.Drawing.Point(12, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(142, 134);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Actions";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(7, 52);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(129, 23);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "Clear Data";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.variableInput);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.btnChangeColor);
            this.groupBox3.Controls.Add(this.variablePicker);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox3.Location = new System.Drawing.Point(12, 153);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(142, 170);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Variables/Colors";
            // 
            // variableInput
            // 
            this.variableInput.AcceptsReturn = true;
            this.variableInput.Enabled = false;
            this.variableInput.Location = new System.Drawing.Point(6, 94);
            this.variableInput.MaxLength = 30;
            this.variableInput.Name = "variableInput";
            this.variableInput.Size = new System.Drawing.Size(122, 22);
            this.variableInput.TabIndex = 4;
            this.variableInput.TextChanged += new System.EventHandler(this.variableInput_TextChanged);
            this.variableInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.variableInput_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "Name (no spaces)";
            // 
            // btnChangeColor
            // 
            this.btnChangeColor.BackColor = System.Drawing.Color.Black;
            this.btnChangeColor.ForeColor = System.Drawing.Color.White;
            this.btnChangeColor.Location = new System.Drawing.Point(5, 122);
            this.btnChangeColor.Name = "btnChangeColor";
            this.btnChangeColor.Size = new System.Drawing.Size(122, 31);
            this.btnChangeColor.TabIndex = 2;
            this.btnChangeColor.Text = "Color";
            this.btnChangeColor.UseVisualStyleBackColor = false;
            this.btnChangeColor.Click += new System.EventHandler(this.btnChangeColor_Click);
            // 
            // variablePicker
            // 
            this.variablePicker.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.variablePicker.FormattingEnabled = true;
            this.variablePicker.Location = new System.Drawing.Point(7, 43);
            this.variablePicker.Name = "variablePicker";
            this.variablePicker.Size = new System.Drawing.Size(121, 24);
            this.variablePicker.TabIndex = 1;
            this.variablePicker.SelectedIndexChanged += new System.EventHandler(this.variablePicker_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Variable";
            // 
            // canvas
            // 
            this.canvas.BackColor = System.Drawing.Color.White;
            this.canvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.canvas.Location = new System.Drawing.Point(161, 20);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(827, 566);
            this.canvas.TabIndex = 1;
            this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.canvas_Paint);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.canvas);
            this.Controls.Add(this.groupBox1);
            this.MaximumSize = new System.Drawing.Size(1018, 647);
            this.MinimumSize = new System.Drawing.Size(1018, 647);
            this.Name = "MainWindow";
            this.Text = "Arduino Plotter";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private DrawingPanel canvas;
        private System.Windows.Forms.TextBox graphPrefix;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDetect;
        private System.Windows.Forms.ComboBox serialChoice;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox showConsole;
        private System.Windows.Forms.Button btnMonitor;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox logGraphCall;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TextBox variableInput;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnChangeColor;
        private System.Windows.Forms.ComboBox variablePicker;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ColorDialog colorPicker;
        private System.Windows.Forms.SaveFileDialog saveDialog;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox baudRateInput;
    }
}

