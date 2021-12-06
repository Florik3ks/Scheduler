// LEGACY 


using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System;
namespace Scheduler
{
    public static class Subjects
    {
        private static Dictionary<string, Color> subjectColors;
        //     private static Dictionary<string, string> subjects = new Dictionary<string, string>{
        //     {"Informatik", "INF"},
        //     {"Englisch", "E"},
        //     {"Physik", "PH"},
        //     {"Deutsch", "D"},
        //     {"Mathe", "M"},
        //     {"Sozialkunde", "SK"},//
        //     {"Sozialkunde/Erdkunde", "SKEK"},
        //     {"Erdkunde", "EK"},//
        //     {"Geschichte", "G"},
        //     {"Musik", "MU"},
        //     {"Sport", "SP"},
        //     {"Biologie", "BIO"},
        //     {"Chemie", "CH"},
        //     {"Kunst", "BK"},//
        //     {"Darstellendes Spiel", "DS"},
        //     {"Französisch", "F"},//
        //     {"k. Religion", "RELRK"},
        //     {"ev. Religion", "RELEV"},
        //     {"Ethik", "ETH"},
        //     {"Philosophie", "PHIL"}
        // };
        public static string GetSubjectNameByTimetableString(string ttString)
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
        //public static (int, int) GetLessonStartTime(int lesson)
        //{
        //    if (lessonStartTimes.ContainsKey(lesson))
        //    {
        //        return lessonStartTimes[lesson].Item1;
        //    }
        //    return lessonStartTimes[-1].Item1;
        //}
        //public static (int, int) GetLessonEndTime(int lesson)
        //{
        //    if (lessonStartTimes.ContainsKey(lesson))
        //    {
        //        return lessonStartTimes[lesson].Item2;
        //    }
        //    return lessonStartTimes[-1].Item2;
        //}
        //public static String[] GetSubjectsThatTheUserHas(string[,] timetable)
        //{
        //    List<String> subjects = new List<String>();
        //    if (timetable.GetLength(0) == 0 && timetable.GetLength(1) == 0) return GetSubjects();
        //    string s;
        //    for (int x = 0; x < timetable.GetLength(0); x++)
        //    {
        //        for (int y = 0; y < timetable.GetLength(1); y++)
        //        {
        //            s = GetSubjectByAcronym(GetSubjectNameByTimetableString(timetable[x, y]));
        //            if (s == "-") continue;
        //            if (!subjects.Contains(s))
        //            {
        //                subjects.Add(s);
        //            }
        //        }
        //    }
        //    subjects.Sort();
        //    subjects.Add("Sonstiges");
        //    return subjects.ToArray();
        //}
        //public static void LoadSubjectColors()
        //{
        //    string jsonString = FileManager.LoadData(FileManager.fileNames["Colors"]);
        //    if (jsonString == null)
        //    {
        //        subjectColors = baseSubjectColors;
        //        return;
        //    }
        //    subjectColors = new Dictionary<string, Color>();
        //    Dictionary<string, DummyColorClass> dummyDict = JsonSerializer.Deserialize<Dictionary<string, DummyColorClass>>(jsonString);
        //    foreach (var key in dummyDict.Keys)
        //    {
        //        subjectColors[key.ToUpper()] = dummyDict[key].GetColor();
        //    }
        //    CheckForWrongOrMissingColorAcronyms();
        //}
        // needed for compatibility reasons
        //public static void CheckForWrongOrMissingColorAcronyms()
        //{
        //    foreach (var key in baseSubjectColors.Keys)
        //    {
        //        if (!subjectColors.ContainsKey(key))
        //        {
        //            subjectColors.Add(key, baseSubjectColors[key]);
        //        }
        //    }
        //    foreach (var key in baseSubjectColors.Keys)
        //    {
        //        if (!subjectColors.ContainsKey(key))
        //        {
        //            subjectColors.Remove(key);
        //        }
        //    }
        //}
        //public static void SaveSubjectColors()
        //{
        //    FileManager.SaveData(subjectColors, FileManager.fileNames["Colors"]);
        //    // string jsonString = JsonSerializer.Serialize(subjectColors);
        //    // string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Florian\\HomeworkPlanner\\";
        //    // if (!Directory.Exists(path))
        //    // {
        //    //     Directory.CreateDirectory(path);
        //    // }
        //    // File.WriteAllText(path + "colors.json", jsonString);
        //}
        public static Color GetColorBySubjectAcronym(string acronym)
        {
            if (subjectColors.ContainsKey(acronym.ToUpper()))
            {
                return subjectColors[acronym.ToUpper()];
            }
            return Color.Transparent;
        }
        //public static string GetSubjectByAcronym(string acronym)
        //{
        //    if (subjects.ContainsKey(acronym.ToUpper()))
        //    {
        //        return subjects[acronym.ToUpper()];
        //    }
        //    return acronym;
        //}
        //public static string GetAcronymBySubject(string subject)
        //{
        //    foreach (var key in subjects.Keys)
        //    {
        //        if (subject == subjects[key]) return key;
        //    }
        //    return subject;
        //}
        //public static string[] GetSubjects()
        //{
        //    // string[] result = new string[subjects.Keys.Count];
        //    // int i = 0;
        //    // foreach (string key in subjects.Keys)
        //    // {
        //    //     result[i] = key;
        //    //     i++;
        //    // }
        //    // return result;

        //    string[] result = new string[subjects.Keys.Count];
        //    int i = 0;
        //    foreach (string key in subjects.Keys)
        //    {
        //        result[i] = subjects[key];
        //        i++;
        //    }
        //    return result;
        //}
        //public static void ColorButtonClick(object sender, System.EventArgs e)
        //{
        //    Control b = (Control)sender;
        //    ColorDialog dialog = new ColorDialog();
        //    dialog.ShowHelp = false;
        //    dialog.Color = b.BackColor;
        //    // Update the text box color if the user clicks OK 
        //    if (dialog.ShowDialog() == DialogResult.OK)
        //    {
        //        if (dialog.Color.GetBrightness() < .33f)
        //        {
        //            b.ForeColor = Color.White;
        //        }
        //        else
        //        {
        //            b.ForeColor = Color.Black;
        //        }
        //        b.BackColor = dialog.Color;
        //        subjectColors[(string)b.Tag] = dialog.Color;
        //        SaveSubjectColors();
        //        Timetable.hasTimetableChangedSinceRedraw = true;
        //        Timetable.ShowPlan(Form1.timetablePanel);
        //    }
        //}
        //public static void ColorListSelectionChanged(object sender, EventArgs e)
        //{
        //    ComboBox box = sender as ComboBox;
        //    string acronym = GetAcronymBySubject(box.SelectedItem.ToString());
        //    Form1.colorPictureBox.Tag = acronym;
        //    Form1.colorPictureBox.BackColor = GetColorBySubjectAcronym(acronym);
        //}
        //public static void ColorListHoverChanged(object sender, ComboBoxListEx.ListItemSelectionChangedEventArgs e)
        //{
        //    if (e.ItemText.ToString() == "") return;
        //    string acronym = GetAcronymBySubject(e.ItemText.ToString());
        //    Form1.colorPictureBox.Tag = acronym;
        //    Form1.colorPictureBox.BackColor = GetColorBySubjectAcronym(acronym);
        //}

    }

}