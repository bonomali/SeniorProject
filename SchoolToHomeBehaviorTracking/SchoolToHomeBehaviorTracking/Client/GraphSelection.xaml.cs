using SchoolToHomeBehaviorTracking_Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;

namespace SchoolToHomeBehaviorTracking_Client
{
    /// <summary>
    /// Interaction logic for GraphSelection.xaml
    /// Logic for interface to select which forms to graph and date range
    /// </summary>
    public partial class GraphSelection : UserControl
    {
        private string _studentName;
        private string _startDate;
        private string _endDate;
        private string ALL_CATEGORY = "All Forms";
        private static string INTERVENTIONFORM = "Intervention Form";
        private static string PROGRESSREPORTFORM = "Progress Report Form";
        private static string HOMETRACKINGFORM = "Home Tracking Form";
        private static string INCIDENTFORM = "Incident Form";
        private List<string> _toGraphForms = new List<string>();
        private bool _teacherGraphs = true; //track if teacher graphs need to be displayed or parent

        private Delegate _handleBackButtonClickMethod;

       static ChannelFactory<IMetaInterface> channelFactory = new
       ChannelFactory<IMetaInterface>("SchoolToHomeServiceEndpoint");
       static IMetaInterface proxy = channelFactory.CreateChannel();

        public Delegate CallingBackButtonClickMethod
        {
            set { _handleBackButtonClickMethod = value; }
        }

        public string StudentName
        {
            get { return _studentName; }
            set { _studentName = value; }
        }

        public string StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        public string EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        public ObservableCollection<GraphSelectionModel> Forms { get; set;}
        public string FormName { get; set; }

        public GraphSelection()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        public void DisplayStudentFormList()
        {
            Clear();
            formsList.ItemsSource = null;

            if (_studentName != null)
            {
                string lname = _studentName.Split(',')[0];
                string fname = _studentName.Split(' ')[1];

                List<string> list = proxy.GetStudentFormsByCategory(fname, lname, ALL_CATEGORY);
                Forms = new ObservableCollection<GraphSelectionModel>();
                foreach (var l in list)
                {
                    if (l != INTERVENTIONFORM && l != PROGRESSREPORTFORM)
                    {
                        GraphSelectionModel item = new GraphSelectionModel(l, false);
                        Forms.Add(item);
                    }
                }
            }
            if (Forms.Count == 0)
            {
                formsList.Visibility = System.Windows.Visibility.Collapsed;
                noForms.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                formsList.Visibility = System.Windows.Visibility.Visible;
                noForms.Visibility = System.Windows.Visibility.Collapsed;
                formsList.ItemsSource = Forms;
            }
            _teacherGraphs = true;
        }

        public void DisplayChildFormList()
        {
            Clear();
            formsList.ItemsSource = null;

            Forms = new ObservableCollection<GraphSelectionModel>();
            GraphSelectionModel item = new GraphSelectionModel(HOMETRACKINGFORM, false);
            Forms.Add(item);

            formsList.Visibility = System.Windows.Visibility.Visible;
            formsList.ItemsSource = Forms;
            _teacherGraphs = false;
        }

        private void graphButton_Click(object sender, RoutedEventArgs e)
        {
            //check that dates are in correct format
            Clear();
            string[] format = { "MM/dd/yyyy", "M/dd/yyyy", "M/d/yyyy", "MM/d/yyyy" };
            DateTime expectedDate1;
            DateTime expectedDate2;
            if (!DateTime.TryParseExact(StartDate, format, new CultureInfo("en-US"), DateTimeStyles.None, out expectedDate1) ||
                !DateTime.TryParseExact(EndDate, format, new CultureInfo("en-US"), DateTimeStyles.None, out expectedDate2))
            {
                invalidDateFormatMess.Visibility = System.Windows.Visibility.Visible;
                return;
            }
;
            foreach (GraphSelectionModel item in formsList.SelectedItems)
            {
                graphScrollViewer.ScrollToTop();

                if(_toGraphForms.Count < 4)
                    _toGraphForms.Add(item.FormName);
            }
            if (_teacherGraphs)
                GraphTeacher();
            else
                GraphParent();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Clear();
            _handleBackButtonClickMethod.DynamicInvoke();
        }

        private void GraphTeacher()
        {
            if (_studentName == null)
                return;

            string lname = _studentName.Split(',')[0];
            string fname = _studentName.Split(' ')[1];

            if (_toGraphForms.Contains(HOMETRACKINGFORM))
            {
                lineGraph5UC.TeacherGraphs = true;
                lineGraph5UC.GraphBehaviorData(HOMETRACKINGFORM, fname, lname, _startDate, _endDate);
                lineGraph5.Visibility = System.Windows.Visibility.Visible;

                scatterGraph1UC.TeacherGraphs = true;
                scatterGraph1UC.GraphSleepData(HOMETRACKINGFORM, fname, lname, _startDate, _endDate);
                scatterGraph1.Visibility = System.Windows.Visibility.Visible;
                _toGraphForms.Remove(HOMETRACKINGFORM);
            }
            if (_toGraphForms.Contains(INCIDENTFORM))
            {
                scatterGraph2UC.TeacherGraphs = true;
                scatterGraph2UC.GraphIncidentData(INCIDENTFORM, fname, lname, _startDate, _endDate);
                scatterGraph2.Visibility = System.Windows.Visibility.Visible;
                _toGraphForms.Remove(INCIDENTFORM);
            }
            if (_toGraphForms.Count > 3)
            {
                lineGraph4UC.TeacherGraphs = true;
                lineGraph4UC.GraphBehaviorData(_toGraphForms[3], fname, lname, _startDate, _endDate);
                lineGraph4.Visibility = System.Windows.Visibility.Visible;
            }
            if (_toGraphForms.Count > 2)
            {
                lineGraph3UC.TeacherGraphs = true;
                lineGraph3UC.GraphBehaviorData(_toGraphForms[2], fname, lname, _startDate, _endDate);
                lineGraph3.Visibility = System.Windows.Visibility.Visible;
            }
            if (_toGraphForms.Count > 1)
            {
                lineGraph2UC.TeacherGraphs = true;
                lineGraph2UC.GraphBehaviorData(_toGraphForms[1], fname, lname, _startDate, _endDate);
                lineGraph2.Visibility = System.Windows.Visibility.Visible;
            }
            if (_toGraphForms.Count > 0)
            {
                lineGraph1UC.TeacherGraphs = true;
                lineGraph1UC.GraphBehaviorData(_toGraphForms[0], fname, lname, _startDate, _endDate);
                lineGraph1.Visibility = System.Windows.Visibility.Visible;
            }

            graphSelection.Visibility = System.Windows.Visibility.Collapsed;
            graphScrollViewer.Visibility = System.Windows.Visibility.Visible;
        }

        private void GraphParent()
        {
            if (_studentName == null)
                return;

            string lname = _studentName.Split(',')[0];
            string fname = _studentName.Split(' ')[1];

            lineGraph5UC.TeacherGraphs = false;
            lineGraph5UC.GraphBehaviorData(HOMETRACKINGFORM, fname, lname, _startDate, _endDate);
            lineGraph5.Visibility = System.Windows.Visibility.Visible;

            scatterGraph1UC.TeacherGraphs = false;
            scatterGraph1UC.GraphSleepData(HOMETRACKINGFORM, fname, lname, _startDate, _endDate);
            scatterGraph1.Visibility = System.Windows.Visibility.Visible;

            graphSelection.Visibility = System.Windows.Visibility.Collapsed;
            graphScrollViewer.Visibility = System.Windows.Visibility.Visible;
        }

        private void Clear()
        {
            _toGraphForms.Clear();
            startDate.Text = "";
            endDate.Text = "";
            invalidDateFormatMess.Visibility = System.Windows.Visibility.Collapsed;
            lineGraph1.Visibility = System.Windows.Visibility.Collapsed;
            lineGraph2.Visibility = System.Windows.Visibility.Collapsed;
            lineGraph3.Visibility = System.Windows.Visibility.Collapsed;
            lineGraph4.Visibility = System.Windows.Visibility.Collapsed;
            lineGraph5.Visibility = System.Windows.Visibility.Collapsed;
            scatterGraph1.Visibility = System.Windows.Visibility.Collapsed;
            scatterGraph2.Visibility = System.Windows.Visibility.Collapsed;
            graphScrollViewer.Visibility = System.Windows.Visibility.Collapsed;
            graphSelection.Visibility = System.Windows.Visibility.Visible;
        }
    }
}
