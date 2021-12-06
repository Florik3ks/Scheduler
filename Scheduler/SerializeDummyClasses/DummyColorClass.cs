using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
namespace Scheduler.SerializeDummyClasses
{
    public class DummyColorClass
    {
        public int R { get; set; }
        public int B { get; set; }
        public int G { get; set; }
        public int A { get; set; }
        public bool IsKnownColor { get; set; }
        public bool IsEmpty { get; set; }
        public bool IsNamedColor { get; set; }
        public bool IsSystemColor { get; set; }
        public string Name { get; set; }
        public DummyColorClass() { }
        public Color GetColor()
        {
            return Color.FromArgb((byte)A, (byte)R, (byte)G, (byte)B);
        }
    }
}
