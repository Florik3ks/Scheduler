using System;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;

namespace Scheduler
{
    public static class _Timetable
    {
        public const int timetableBaseHeight = 11;
        public const int timetableBaseWidth = 5;
        public static string[,] timetable;
        public static bool hasTimetableChangedSinceRedraw = false;
        public enum loadingType
        {
            reloadFromPortal, loadFromFile
        };

        public static DayOfWeek GetCurrentWeekDayByDayIndex(int day){

            DayOfWeek dayOfWeek = DayOfWeek.Monday;
            switch (day)
            {
                case 0:
                    dayOfWeek = DayOfWeek.Monday;
                    break;
                case 1:
                    dayOfWeek = DayOfWeek.Tuesday;
                    break;
                case 2:
                    dayOfWeek = DayOfWeek.Wednesday;
                    break;
                case 3:
                    dayOfWeek = DayOfWeek.Thursday;
                    break;
                case 4:
                    dayOfWeek = DayOfWeek.Friday;
                    break;
                case 5:
                    dayOfWeek = DayOfWeek.Saturday;
                    break;
                case 6:
                    dayOfWeek = DayOfWeek.Sunday;
                    break;
            }
            return dayOfWeek;
        }
        
        public static DateTime GetNextWeekday(DateTime start, DayOfWeek day)
        {
            int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
            return start.AddDays(daysToAdd);
        }


        //public static void OnLoginCredentialsSubmitButtonClick(object sender, EventArgs e)
        //{

        //    Portal.HasPortalLoginDetails = true;
        //    bool result = LoadPlan(loadingType.reloadFromPortal, true, Form1.username.Text, Form1.password.Text);
        //    ShowPlan(Form1.timetablePanel);
        //    // ResizeLabels(Form1.timetablePanel);
        //    if (result && timetable.Length > 0)
        //    {
        //        MessageBox.Show("Stundenplan erfolgreich geladen!");
        //        Config.portalUsername = Form1.username.Text;
        //        Config.portalPassword = Form1.password.Text;
        //        Config.SaveConfig();
        //    }
        //}
        public static bool AreTTEqual(string[,] timetable, string[,] otherTimetable)
        {
            // TimetableDummyClass tt = new TimetableDummyClass(timetable);
            // TimetableDummyClass otherTt = new TimetableDummyClass(otherTimetable);
            // return JsonSerializer.Serialize(tt) == JsonSerializer.Serialize(otherTt);
            if (timetable == null || otherTimetable == null) return false;
            if (timetable.GetLength(0) != otherTimetable.GetLength(0) || timetable.GetLength(1) != otherTimetable.GetLength(1)) return false;
            for (int x = 0; x < timetable.GetLength(0); x++)
            {
                for (int y = 0; y < timetable.GetLength(1); y++)
                {
                    if (timetable[x, y] != otherTimetable[x, y]) return false;
                }
            }
            return true;
        }
        //public static bool LoadPlan(loadingType lt = loadingType.loadFromFile, bool showMessages = false, string username = "", string password = "")
        //{
        //    string[,] tt;
        //    // string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + FileManager.path;
        //    if (lt == loadingType.reloadFromPortal /*|| !File.Exists(path + FileManager.fileNames["Timetable"] + FileManager.fileNames["ext"])*/)
        //    {
        //        if (Portal.HasPortalLoginDetails)
        //        {
        //            tt = Portal.GetPlan(username, password, showMessages);
        //            if (!AreTTEqual(tt, timetable)) hasTimetableChangedSinceRedraw = true;
        //            if (tt.Length > 0)
        //            {
        //                timetable = tt;
        //                SavePlan();
        //                String[] newSubjects = Subjects.GetSubjectsThatTheUserHas();
        //                // if (((String[])Form1.subjectsBox.DataSource).ToList().Contains(Form1.subjectsBox.SelectedText)){
        //                // ;
        //                // }
        //                Form1.subjectsBox.Invoke(new Action(() =>
        //                {
        //                    Form1.subjectsBox.DataSource = newSubjects;
        //                    Form1.subjectsBox.Refresh();
        //                }));
        //                return true;
        //            }
        //            return false;
        //        }
        //        // dont overwrite existing timetable if no portal credentials are saved but a timetable.json is available
        //        if (timetable == null)
        //        {
        //            timetable = new string[0, 0];
        //        }
        //        return false;
        //    }
        //    string jsonString = FileManager.LoadData(FileManager.fileNames["Timetable"]);
        //    if (jsonString == null)
        //    {
        //        timetable = new string[0, 0];
        //        return false;
        //    }
        //    TimetableDummyClass tbc = JsonSerializer.Deserialize<TimetableDummyClass>(jsonString);
        //    tt = tbc.GetNormalTimetable();
        //    if (!AreTTEqual(tt, timetable)) hasTimetableChangedSinceRedraw = true;
        //    timetable = tt;
        //    return true;
        //}
        //public static (int, int) GetNextSubjectLesson(string subjectName){
        //    int currentDay, currentLesson;
        //    (currentDay, currentLesson) = GetCurrentLessons();
        //    if((currentDay, currentLesson) == (-1, -1)) return (-1, -1);
        //    // rest days of the week
        //    for (int d = currentDay + 1; d < timetableBaseWidth; d++){
        //        for (int l = 0; l < timetableBaseHeight; l++){
        //            if(Subjects.GetSubjectByAcronym(Subjects.GetSubjectNameByTimetableString(timetable[d, l])) == subjectName){
        //                return (d, l + 1);
        //            }
        //        }
        //    }
        //    // first days of the next week
        //    for (int d = 0; d <= currentDay; d++){
        //        for (int l = 0; l < timetableBaseHeight; l++){
        //            if(Subjects.GetSubjectByAcronym(Subjects.GetSubjectNameByTimetableString(timetable[d, l])) == subjectName){
        //                return (d, l + 1);
        //            }
        //        }
        //    }
        //    return (-1, -1);
        //}
        //public static (int, int) GetCurrentLessons()
        //{
        //    DateTime currentDate = DateTime.Now;
        //    int hour = 0;
        //    int minutes = 0;
        //    int lesson = -1;
        //    for (int i = 0; i < timetableBaseHeight; i++)
        //    {
        //        (hour, minutes) = Subjects.GetLessonStartTime(i + 1);
        //        if ((hour == currentDate.Hour && minutes >= currentDate.Minute) || hour > currentDate.Hour)
        //        {
        //            lesson = i - 1;
        //            break;
        //        }
        //    }
        //    if (lesson == -1) return (-1, -1);
        //    int dayIndex = (int)currentDate.DayOfWeek - 1;
        //    return (dayIndex, lesson);
        //}
        //public static string GetCurrentSubject()
        //{
        //    int day, hour;
        //    (day, hour) = GetCurrentLessons();
        //    if(day == -1 || hour == -1 || hour >= timetableBaseHeight || day >= timetableBaseWidth) return "-";
        //    return Subjects.GetSubjectByAcronym(Subjects.GetSubjectNameByTimetableString(timetable[day, hour]));
        //}
        //public static void SavePlan()
        //{
        //    TimetableDummyClass tdc = new TimetableDummyClass(timetable);
        //    FileManager.SaveData(tdc, FileManager.fileNames["Timetable"]);
        //    // string jsonString = JsonSerializer.Serialize(tdc);
        //    // string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Florian\\HomeworkPlanner\\";
        //    // if (!Directory.Exists(path))
        //    // {
        //    //     Directory.CreateDirectory(path);
        //    // }
        //    // File.WriteAllText(path + "timetable.json", jsonString);
        //}
    }
    
}