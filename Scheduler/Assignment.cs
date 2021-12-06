using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
namespace Scheduler
{
    public class Assignment
    {
        public string Title { get; set; }
        public string ID { get; }
        public string SubjectAcronym { get; }
        public string Header { get; }
        public string Description { get; }
        public bool Done { get; set; }
        public SolidColorBrush Color { get; }
        public DateTime Deadline { get; }
        public Assignment(string title, string subjectAcronym, DateTime deadline, string id=null, string description = "")
        {
            Title = title;
            SubjectAcronym = subjectAcronym;
            Deadline = deadline;
            Description = description;
            ID = id;
            if (ID == null) ID = Guid.NewGuid().ToString();
            Color = new SolidColorBrush(SubjectData.GetColorBySubjectAcronym(subjectAcronym));
        }
        //public Assignment(string title, string subjectAcronym, DateTime deadline, string id, string description = "")
        //{
        //    Title = title;
        //    SubjectAcronym = subjectAcronym;
        //    Deadline = deadline;
        //    ID = id;
        //    Color = new SolidColorBrush(SubjectData.GetColorBySubjectAcronym(subjectAcronym));
        //}
        public string GetDay()
        {
            return Deadline.DayOfWeek.ToString()
            .Replace("Monday", "Montag")
            .Replace("Tuesday", "Dienstag")
            .Replace("Wednesday", "Mittwoch")
            .Replace("Thursday", "Donnerstag")
            .Replace("Friday", "Freitag")
            .Replace("Saturday", "Samstag")
            .Replace("Sunday", "Sonntag");
             //+ ", " + d.Day.ToString() + "." + d.Month.ToString();
        }
        public string GetDate()
        {
            return Deadline.Day.ToString() + "." + Deadline.Month.ToString() + Deadline.Year.ToString();
        }
    }
}
