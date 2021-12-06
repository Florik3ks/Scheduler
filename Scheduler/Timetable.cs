using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
    public class Timetable
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Dictionary<int, ((int, int), (int, int))> lessonTimes { get; private set; }
        public TimetableCell[,] table;

        public Timetable()
        {
            //Width = 5;
            //Height = 11;
            //lessonTimes = baseLessonTimes;
            Width = 0;
            Height = 0;
            lessonTimes = baseLessonTimes;
            table = new TimetableCell[0, 0];
        }
        public Timetable(string[,] table)
        {
            Width = table.GetLength(0);
            Height = table.GetLength(1);
            lessonTimes = new Dictionary<int, ((int, int), (int, int))>();
            lessonTimes[-1] = baseLessonTimes[-1];

            for (int i = 0; i < Height; i++)
            {
                if (baseLessonTimes.ContainsKey(i + 1))
                {
                    lessonTimes[i + 1] = baseLessonTimes[i + 1];
                }
                else
                {
                    lessonTimes[i + 1] = baseLessonTimes[-1];
                }
            }

            this.table = new TimetableCell[Width, Height];
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    string subject = GetSubjectNameByTimetableString(table[x, y]);
                    string[] subjectData = table[x, y].Replace("\n", "").Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    if (subjectData.Length > 2)
                    {
                        this.table[x, y] = new TimetableCell(x, y, subject, subjectData[2], subjectData[1]);
                    }
                    else
                    {
                        this.table[x, y] = new TimetableCell(x, y, subject, "-", "-");

                    }
                }
            }
        }
        private string GetSubjectNameByTimetableString(string ttString)
        {
            string subject = "";
            char[] ar = ttString.Replace("\n", "").Split("_")[0].ToCharArray();
            bool subjectStarted = false;
            for (int i = 0; i < ar.Length; i++)
            {
                if (ar[i] != ' ')
                {
                    subjectStarted = true;
                    subject += ar[i];
                }
                else if (subjectStarted)
                    return subject;
            }
            return subject == "" ? ttString : subject;
        }
        public (int, int) GetLessonStartTime(int lesson)
        {
            if (lessonTimes.ContainsKey(lesson)) return lessonTimes[lesson].Item1;
            return lessonTimes[-1].Item1;
        }

        public static Dictionary<int, ((int, int), (int, int))> baseLessonTimes = new Dictionary<int, ((int, int), (int, int))>(){
            {01 , ((08, 00), (08, 45))},
            {02 , ((08, 45), (09, 30))},
            {03 , ((09, 50), (10, 35))},
            {04 , ((10, 35), (11, 20))},
            {05 , ((11, 40), (12, 25))},
            {06 , ((12, 25), (13, 10))},
            {07 , ((13, 15), (14, 00))},
            {08 , ((14, 00), (14, 45))},
            {09 , ((14, 45), (15, 30))},
            {10 , ((15, 40), (16, 25))},
            {11 , ((16, 25), (17, 10))},

            {-1 , ((17, 10), (08, 00))},
        };
    }
    public class TimetableCell
    {
        public string Subject { get; private set; }
        public string Room { get; private set; }
        public string Information { get; private set; }
        public int x { get; private set; }
        public int y { get; private set; }
        public TimetableCell(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public TimetableCell(int x, int y, string subject, string room, string information)
        {
            this.x = x;
            this.y = y;
            this.Subject = subject;
            this.Room = room;
            this.Information = information;
        }
    }
}
