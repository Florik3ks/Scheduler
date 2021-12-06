using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
namespace Scheduler
{
    class SubjectData
    {

        public static Color GetColorBySubjectAcronym(string acronym)
        {
            if (DataManager.subjectColors.ContainsKey(acronym.ToUpper()))
            {
                Color c = DataManager.subjectColors[acronym.ToUpper()];
                if (c.A > 100) c.A = 100;
                return c;
            }
            return Colors.Transparent;
        }
        public static Color GetBaseColorBySubjectAcronym(string acronym)
        {
            if (DataManager.BaseSubjectColors.ContainsKey(acronym.ToUpper()))
            {
                Color c = DataManager.BaseSubjectColors[acronym.ToUpper()];
                if (c.A > 100) c.A = 100;
                return c;
            }
            return Colors.Transparent;
        }
        public static string GetSubjectNameByAcronym(string acronym)
        {
            if (DataManager.subjectNames.ContainsKey(acronym.ToUpper()))
            {
                string subject = DataManager.subjectNames[acronym.ToUpper()];
                return subject;
            }
            return "-";
        }
    }
}
