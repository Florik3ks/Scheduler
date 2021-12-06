using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.Storage;
using Windows.Storage.Pickers;
using System.IO;
using System.Text.Json;
using Scheduler.SerializeDummyClasses;
namespace Scheduler
{
    public class DataManager
    {
        public static bool loadedData = false;

        public static Dictionary<string, Color> subjectColors;
        public static Dictionary<string, string> subjectNames;
        public static Dictionary<string, Color> BaseSubjectColors { get => baseSubjectColors; }
        public static Timetable timetable;

        public static async Task InitializeData()
        {
            await LoadTimetable();
            await LoadColors();
            await LoadSubjects();
            loadedData = true;
        }
        private static async Task LoadColors()
        {
            string filename = "subjectColors.json";
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile file = (StorageFile)await localFolder.TryGetItemAsync(filename);
            if (file != null)
            {
                string jsonString = await LoadData(file);
                if (jsonString == null)
                {
                    subjectColors = baseSubjectColors;
                    return;
                }
                subjectColors = new Dictionary<string, Color>();
                Dictionary<string, DummyColorClass> dummyDict = JsonSerializer.Deserialize<Dictionary<string, DummyColorClass>>(jsonString);
                foreach (var key in dummyDict.Keys)
                {
                    subjectColors[key.ToUpper()] = dummyDict[key].GetColor();
                }

                bool changed = false;
                foreach(var key in baseSubjectColors.Keys)
                {
                    if (!subjectColors.Keys.Contains(key))
                    {
                        changed = true;
                        subjectColors[key] = baseSubjectColors[key];
                    }
                }
                if (changed)
                {
                    await SaveData(subjectColors, await localFolder.GetFileAsync(filename));
                }
            }
            else
            {
                await localFolder.CreateFileAsync(filename);
                file = await localFolder.GetFileAsync(filename);
                subjectColors = baseSubjectColors;
                await SaveData(subjectColors, await localFolder.GetFileAsync(filename));
            }
        }
        
        private static async Task LoadTimetable()
        {
            string filename = "timetable.json";
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile file = (StorageFile)await localFolder.TryGetItemAsync(filename);
            if (file != null)
            {
                string jsonString = await LoadData(file);
                if (jsonString == null)
                {
                    timetable = new Timetable();
                    return;
                }
                subjectColors = new Dictionary<string, Color>();
                TimetableDummyClass tbc = JsonSerializer.Deserialize<TimetableDummyClass>(jsonString);
                timetable = new Timetable(tbc.GetNormalTimetable());
            }
            else
            {
                timetable = new Timetable();
            }
        }

        private static async Task LoadSubjects()
        {
            string filename = "subjects.json";
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile file = (StorageFile)await localFolder.TryGetItemAsync(filename);
            if (file != null)
            {
                string jsonString = await LoadData(file);
                if (jsonString == null)
                {
                    subjectNames = baseSubjectNames;
                    return;
                }
                subjectNames = JsonSerializer.Deserialize <Dictionary<string, string>> (jsonString);
            }
            else
            {
                subjectNames = baseSubjectNames;
                await localFolder.CreateFileAsync(filename);
                file = await localFolder.GetFileAsync(filename);
                await SaveData(subjectNames, await localFolder.GetFileAsync(filename));
            }
        }

        private static async Task<String> LoadData(StorageFile file)
        {
            string jsonString = await FileIO.ReadTextAsync(file);
            return jsonString;
        }
        private static async Task SaveData(object data, StorageFile file)
        {
            string jsonString = JsonSerializer.Serialize(data);
            if (File.Exists(file.Path))
            {
                await FileIO.WriteTextAsync(file, jsonString);
            }
        }


        private static Dictionary<string, Color> baseSubjectColors = new Dictionary<string, Color>{
            {"INF", Colors.GreenYellow},
            {"E", Colors.Yellow},
            {"PH", Colors.Gray},
            {"D", Colors.Red},
            {"M", Colors.RoyalBlue},
            {"SK", Colors.RosyBrown},//
            {"SKEK", Colors.RosyBrown},
            {"EK", Colors.RosyBrown},//
            {"G", Colors.White},
            {"MU", Colors.Orange},
            {"SP", Colors.BurlyWood},
            {"BIO", Colors.Green},
            {"CH", Colors.LightGreen},
            {"BK", Colors.Purple},//
            {"DS", Colors.White},
            {"F", Colors.Orange},//
            {"L", Colors.Cyan},//
            {"RELRK", Colors.Pink},
            {"RELEV", Colors.Pink},
            {"ETH", Colors.Pink},
            {"PHIL", Colors.SlateGray},
            {"KL", Colors.SlateGray},
            {"ITG", Colors.SlateGray},
            {"NW", Colors.SeaGreen},
            {"FREISTUNDE", Colors.Transparent}
        };
        private static Dictionary<string, string> baseSubjectNames = new Dictionary<string, string>{
            {"INF", "Informatik"},
            {"E", "Englisch"},
            {"PH", "Physik"},
            {"D", "Deutsch"},
            {"M", "Mathe"},
            {"SK", "Sozialkunde"},
            {"SKEK", "Sozialkunde/Erdkunde"},
            {"EK", "Erdkunde"},
            {"G", "Geschichte"},
            {"MU", "Musik"},
            {"SP", "Sport"},
            {"BIO", "Biologie"},
            {"CH", "Chemie"},
            {"S", "Spanisch"},
            {"L", "Latein"},
            {"BK", "Kunst"},//
            {"DS", "Darstellendes Spiel"},
            {"F", "Französisch"},//
            {"RELRK", "k. Religion"},
            {"RELEV", "ev. Religion"},
            {"ETH", "Ethik"},
            {"PHIL", "Philosophie"},
            {"KL", "Klassenleiterstunde"},
            {"ITG", "Informatisch Technologische Grundlagen"},
            {"NW", "Naturwissenschaften"},
            {"SONSTIGES", "Sonstiges"},
            {"FREISTUNDE","Freistunde"}
        };
    }
}
