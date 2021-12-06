using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Text.Json;
using Windows.Storage;
using Windows.Storage.Pickers;
using System.Threading.Tasks;
using Windows.UI;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace Scheduler
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class StundenplanPage : Page
    {
        private bool subjectInformationCurrentlyOpen;
        private TextBox roomText;
        private TextBox infoText;
        private ColorPicker subjectColorPicker;
        private Button restoreDefaultButton;
        public StundenplanPage()
        {
            this.InitializeComponent();
            InitializeTimetable();
            subjectInformationCurrentlyOpen = false;
        }
        private void InitializeTimetable()
        {
            bool showTimes = true;

            int height = DataManager.timetable.Height;
            int width = DataManager.timetable.Width;
            if (showTimes) width++;

            timetableGrid.ColumnDefinitions.Clear();
            timetableGrid.RowDefinitions.Clear();
            for (int x = 0; x < width; x++)
            {
                timetableGrid.ColumnDefinitions.Add(new ColumnDefinition { MaxWidth = 1000 });
            }
            for (int y = 0; y < height; y++)
            {
                timetableGrid.RowDefinitions.Add(new RowDefinition { MaxHeight = 300 });
            }

            for (int x = 0; x < width; x++)
            {
                int dontShowNextLessons = 0;
                int lessonRepeated = 1;
                for (int y = 0; y < height; y++)
                {
                    //Border b = new Border { Margin = new Thickness(2), Background = new SolidColorBrush(c) };
                    Brush acrylicBrush = (Brush)Application.Current.Resources.ThemeDictionaries["SystemControlAcrylicWindowBrush"];
                    //Brush revealBrush = (Brush)Application.Current.Resources.ThemeDictionaries["SystemControlBackgroundListMediumRevealBorderBrush"];
                    if (dontShowNextLessons > 0) { dontShowNextLessons--; continue; }
                    else lessonRepeated = 1;

                    int day = showTimes ? x - 1 : x;
                    Button b = new Button
                    {
                        FontSize = 20,
                        Style = (Style)Application.Current.Resources["ButtonRevealStyle"],
                    };
                    if (day >= 0)
                    {
                        b.Content = DataManager.timetable.table[day, y].Subject;
                    }
                    if (showTimes)
                    {
                        if (x == 0)
                        {
                            (int, int) time = DataManager.timetable.GetLessonStartTime(y + 1);
                            b.Content = $"{time.Item1.ToString().PadLeft(2, '0')}:{time.Item2.ToString().PadLeft(2, '0')}";
                        }
                        else if (b.Content.ToString() != "-")
                        {
                            ToolTipService.SetToolTip(b,
                                SubjectData.GetSubjectNameByAcronym(b.Content.ToString()) + "\n" +
                                DataManager.timetable.table[day, y].Room + " " +
                                DataManager.timetable.table[day, y].Information
                            );
                        }
                    }
                    if (b.Content.ToString() == "-" || (showTimes && x == 0))
                    {
                        b.Background = acrylicBrush;
                        b.Tag = null;
                    }
                    else
                    {
                        b.Background = new SolidColorBrush(SubjectData.GetColorBySubjectAcronym(b.Content.ToString()));
                        b.Click += TimetableLessonClicked;
                        b.Tag = DataManager.timetable.table[day, y];
                    }

                    string subject = b.Content.ToString();
                    if ((!showTimes || showTimes && x > 0) && subject != "-")
                    {
                        bool continuing = true;
                        for (int i = y; i < height - 1; i++)
                        {
                            if (DataManager.timetable.table[day, i + 1].Subject == subject && continuing)
                            {
                                lessonRepeated++;
                            }
                            else continuing = false;
                        }
                        dontShowNextLessons = lessonRepeated - 1;
                    }

                    b.HorizontalAlignment = HorizontalAlignment.Stretch;
                    b.VerticalAlignment = VerticalAlignment.Stretch;
                    Grid.SetColumn(b, x);
                    Grid.SetRowSpan(b, lessonRepeated);
                    Grid.SetRow(b, y);
                    b.UseLayoutRounding = true;
                    timetableGrid.Children.Add(b);
                }
            }
        }

        private void TimetableLessonClicked(object sender, RoutedEventArgs e)
        {
            TimetableCell cell = (TimetableCell)((Button)sender).Tag;
            if (subjectInformationCurrentlyOpen)
            {
                subjectColorPicker.Tag = cell;
                subjectColorPicker.Color = SubjectData.GetColorBySubjectAcronym(cell.Subject);
                roomText.Text = cell.Room;
                infoText.Text = cell.Information;
                restoreDefaultButton.Tag = cell.Subject;
                return;
            }

            dataPanel.Children.Clear();
            subjectInformationCurrentlyOpen = true;

            roomText = new TextBox { Text = cell.Room, IsReadOnly = true };
            infoText = new TextBox { Text = cell.Information, IsReadOnly = true };
            subjectColorPicker = new ColorPicker
            {
                Color = SubjectData.GetColorBySubjectAcronym(cell.Subject),
                ColorSpectrumShape = ColorSpectrumShape.Ring,
                IsMoreButtonVisible = false,
                IsColorSliderVisible = true,
                IsColorChannelTextInputVisible = false,
                IsHexInputVisible = false,
                IsAlphaEnabled = false,
                IsAlphaSliderVisible = true,
                IsAlphaTextInputVisible = true,
                IsColorPreviewVisible = false
            };
            subjectColorPicker.ColorChanged += ColorChanged;
            subjectColorPicker.Tag = cell;

            Border informationBorder = new Border { BorderThickness = new Thickness(5) };
            Border roomBorder = new Border { BorderThickness = new Thickness(5) };
            Viewbox colorBox = new Viewbox { HorizontalAlignment = HorizontalAlignment.Stretch };

            restoreDefaultButton = new Button { Content = "Standardfarbe wiederherstellen" };

            restoreDefaultButton.Tag = cell.Subject;
            restoreDefaultButton.Click += ShowRestoreDefaultColor_Click;

            roomBorder.Child = roomText;
            informationBorder.Child = infoText;
            colorBox.Child = subjectColorPicker;
            dataPanel.Children.Add(roomBorder);
            dataPanel.Children.Add(informationBorder);
            dataPanel.Children.Add(colorBox);
            dataPanel.Children.Add(restoreDefaultButton);

        }

        private void ColorChanged(object sender, ColorChangedEventArgs e)
        {
            TimetableCell cell = (TimetableCell)((ColorPicker)sender).Tag;
            ChangeColorForSubject(cell.Subject, ((ColorPicker)sender).Color);
        }
        private void ChangeColorForSubject(string subject, Color color)
        {
            color.A = 100;
            DataManager.subjectColors[subject.ToUpper()] = color;
            foreach (var item in timetableGrid.Children)
            {
                if (((Button)item).Tag != null)
                {
                    TimetableCell buttonCell = (TimetableCell)((Button)item).Tag;
                    if (buttonCell.Subject == subject)
                    {
                        ((Button)item).Background = new SolidColorBrush(color);
                    }
                }
            }
        }

        private async void ShowRestoreDefaultColor_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            string subject = b.Tag.ToString();
            ContentDialog dialog = new ContentDialog();
            dialog.Title = $"Standardfarbe für '{subject}' wiederherstellen?";
            dialog.PrimaryButtonText = "Ja";
            dialog.CloseButtonText = "Abbrechen";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = new ContentDialog();

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                ChangeColorForSubject(subject, SubjectData.GetBaseColorBySubjectAcronym(subject));
            }
        }

        //public static async Task<String> LoadData()
        //{
        //    //string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + path;
        //    //string idk = folderPath + filename + ext;
        //    var openPicker = new FileOpenPicker();
        //    openPicker.FileTypeFilter.Add("*");
        //    StorageFile file = await openPicker.PickSingleFileAsync();
        //    //StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appdata:///local/timetable.json"));
        //    //if (!File.Exists(folderPath + filename + ext))
        //    //{
        //    //    return null;
        //    //}
        //    string jsonString = await Windows.Storage.FileIO.ReadTextAsync(file);
        //    return jsonString;
        //    // Dictionary<string, DummyColorClass> dummyDict = JsonSerializer.Deserialize<Dictionary<string, DummyColorClass>>(jsonString);
        //    // subjectColors = new Dictionary<string, Color>();
        //    // foreach (var key in dummyDict.Keys)
        //    // {
        //    //     subjectColors[key] = dummyDict[key].GetColor();
        //    // }
        //}
    }
}
