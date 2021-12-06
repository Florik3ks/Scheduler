using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.SerializeDummyClasses
{
    public class TimetableDummyClass
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public string[] Values { get; set; }
        public TimetableDummyClass() { }
        public TimetableDummyClass(string[,] normalTimetable)
        {
            Height = normalTimetable.GetLength(1);
            Width = normalTimetable.GetLength(0);
            Values = new string[Height * Width];
            int i = 0;
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Values[i] = normalTimetable[x, y];
                    i++;
                }
            }
        }
        public string[,] GetNormalTimetable()
        {
            string[,] normal = new string[Width, Height];
            int i = 0;
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    normal[x, y] = Values[i];
                    i++;
                }
            }
            return normal;
        }
    }
}
