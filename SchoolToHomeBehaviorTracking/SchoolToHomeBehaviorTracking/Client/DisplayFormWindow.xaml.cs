using SchoolToHomeBehaviorTracking_Interface;
using System.Windows;
using System.Windows.Controls;

namespace SchoolToHomeBehaviorTracking_Client
{
    /// <summary>
    /// Interaction logic for ParentFormWindow.xaml
    /// Display form corresponding to point on graph
    /// </summary>
    public partial class DisplayFormWindow : Window
    {
        StudentFormData _form;
        private static string HOMETRACKINGFORM = "Home Tracking Form";

        public DisplayFormWindow(StudentFormData form)
        {
            InitializeComponent();
            teacherSubmitFormUC.backButton.Visibility = System.Windows.Visibility.Collapsed;
            parentSubmitFormUC.backButton.Visibility = System.Windows.Visibility.Collapsed;
            _form = form;

            if (_form.FormName == HOMETRACKINGFORM)
            {
                parentSubmitFormUC.ViewParentForm(_form);
                parentSubmitForm.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                teacherSubmitFormUC.ViewTeacherForm(_form);
                teacherSubmitForm.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width < 725)
            {
                MyScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
            }
            else
            {
                MyScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
            }
            if (e.NewSize.Height < 575)
            {
                MyScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            }
            else
            {
                MyScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
            }
        }
    }
}
