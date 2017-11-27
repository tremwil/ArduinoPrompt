using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ArduinoPrompt
{
    class Variable
    {
        public string name;
        public Color plotColor;

        public List<PointF> plotValues;

        public Variable(string name, Color plotColor)
        {
            this.name = name;
            this.plotColor = plotColor;
            plotValues = new List<PointF>();
        }

        public override string ToString()
        {
            return name;
        }
    }
}
