using LiveCharts;
using LiveCharts.Wpf;
using SchoolToHomeBehaviorTracking_Interface;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Windows.Controls;

namespace SchoolToHomeBehaviorTracking_Client
{
    /// <summary>
    /// Interaction logic for Graph.xaml
    /// Graph  data 
    /// </summary>
    public partial class Graph : UserControl
    {
        private List<List<string>> _dateList = new List<List<string>>();
        private bool _teacherGraphs = false;    //indicate if graphing for teacher or parent

        static ChannelFactory<IWCFService> channelFactory = new
        ChannelFactory<IWCFService>("SchoolToHomeServiceEndpoint");

        static IWCFService proxy = channelFactory.CreateChannel();

        public bool TeacherGraphs
        {
            get { return _teacherGraphs; }
            set { _teacherGraphs = value; }
        }

        public Graph()
        {
            InitializeComponent();
            SeriesCollection = new SeriesCollection();
            DataContext = this;
        }

        public void GraphBehaviorData(string formName, string firstName, string lastName, string startDate, string endDate)
        {
            _dateList.Clear();
            SeriesCollection.Clear();
            
            SeriesCollection.Add(new LineSeries
            {
                Values = new ChartValues<int>(),
                Foreground = System.Windows.Media.Brushes.Black,
                Fill = System.Windows.Media.Brushes.LightCyan
            });
            
            XAxisLabel = "Behavior Rating";

            if (_teacherGraphs)
                _dateList = proxy.GetBehaviorGraphingData(formName, firstName, lastName, startDate, endDate);
            else
                _dateList = proxy.GetHomeTrackingGraphingData(firstName, lastName, startDate, endDate);

            Labels = _dateList[0].ToArray();
            graphTitle.Text = formName;

            foreach (string x in _dateList[1])
            {
                try
                {
                    SeriesCollection[0].Values.Add(Convert.ToInt32(x));
                }
                catch { }
            }
        }

        public void GraphSleepData(string formName, string firstName, string lastName, string startDate, string endDate)
        {
            _dateList.Clear();
            SeriesCollection.Clear();

            SeriesCollection.Add(new LineSeries
            {
                Values = new ChartValues<int>(),
                Foreground = System.Windows.Media.Brushes.Black,
                Fill = System.Windows.Media.Brushes.LightCyan
            });

            XAxisLabel = "Hours Slept";
            _dateList = proxy.GetBehaviorGraphingData(formName, firstName, lastName, startDate, endDate);

            Labels = _dateList[0].ToArray();
            graphTitle.Text = formName + " Sleep";

            foreach (string x in _dateList[2])
            {
                try
                {
                    SeriesCollection[0].Values.Add(Convert.ToInt32(x));
                }
                catch { }
            }
        }

        public void GraphIncidentData(string firstName, string lastName, string startDate, string endDate)
        {

        }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public string XAxisLabel { get; set; }
        public string GraphTitle { get; set; }

        private void CartesianChart_DataClick(object sender, ChartPoint chartPoint)
        {
            int point = Convert.ToInt32(chartPoint.X);
            string id = _dateList[3][point];
            StudentFormData f;
            if (_teacherGraphs)
                f = proxy.GetTeacherViewableFormByID(id);
            else
                f = proxy.GetParentViewableFormByID(id);
                
            if (f != null)
            {
                DisplayFormWindow window = new DisplayFormWindow(f);
                window.Show();
            }
        }
    }
}
