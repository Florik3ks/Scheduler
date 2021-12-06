using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Scheduler
{
    public sealed partial class AufgabenPage : Page
    {
        ObservableCollection<Assignment> assignmentList;
        ObservableCollection<Assignment> doneAssignmentList;
        public AufgabenPage()
        {
            this.InitializeComponent();
            assignmentList = new ObservableCollection<Assignment>();
            doneAssignmentList = new ObservableCollection<Assignment>();
            AssignmentList.ItemsSource = assignmentList;
            AssignmentList2.ItemsSource = doneAssignmentList;

            Assignment a = new Assignment("hellu");
            assignmentList.Add(a);

        }
        private void OnMoveButtonClick(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            string id = b.Tag.ToString();
            foreach(var item in assignmentList)
            {
                if (item.ID == id)
                {
                    assignmentList.Remove(item);
                    doneAssignmentList.Add(item);
                    return;
                }
            }
            foreach (var item in doneAssignmentList)
            {
                if (item.ID == id)
                {
                    assignmentList.Add(item);
                    doneAssignmentList.Remove(item);
                    return;
                }
            }
            return;

        }

    }
}
