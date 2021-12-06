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

            Assignment a = new Assignment("hellu", "E", DateTime.Now, description:"why am i doing this");
            assignmentList.Add(a);
            Assignment b = new Assignment("Informatik-HA", "INF", DateTime.Now, description:"Vortrag vorbereiten?");
            assignmentList.Add(b);
            Assignment c = new Assignment("irgendetwas mit falschem Fach", "sd", DateTime.Now, description: "nyanya");
            assignmentList.Add(c);
            Assignment d = new Assignment("wah", "D", DateTime.Now, description: "irgendetwas langeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeees nyanyanyanyanyanyanyanyanyanyanyanya");
            assignmentList.Add(d);
        }


        private void OnMoveButtonClick(object sender, RoutedEventArgs args)
        {
            Button b = sender as Button;
            string id = b.Tag.ToString();
            foreach (var item in assignmentList)
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

        private void List_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            if (e.Pointer.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Mouse || e.Pointer.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Pen)
            {
                VisualStateManager.GoToState(sender as Control, "moveButtonShown", true);
            }
        }

        private void List_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            VisualStateManager.GoToState(sender as Control, "moveButtonHidden", true);
        }
    }
}
