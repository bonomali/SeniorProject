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
    public partial class LineGraph : UserControl
    {
        private List<List<string>> _dateList = new List<List<string>>();
        private bool _teacherGraphs = false;    //indicate if graphing for teacher or parent

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public string YAxisLabel { get; set; }
        public string GraphTitle { get; set; }

        static ChannelFactory<IMetaInterface> channelFactory = new
        ChannelFactory<IMetaInterface>("SchoolToHomeServiceEndpoint");

        static IMetaInterface proxy = channelFactory.CreateChannel();

        public bool TeacherGraphs
        {
            get { return _teacherGraphs; }
            set { _teacherGraphs = value; }
        }

        public LineGraph()
        {
            InitializeComponent();
            SeriesCollection = new SeriesCollection();
            this.DataContext = this;
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
            
            YAxisLabel = "Behavior Rating";

            if (_teacherGraphs)//graph teacher graphs
                _dateList = proxy.GetBehaviorGraphingData(formName, firstName, lastName, startDate, endDate);
            else  //graph parent graphs
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
                window.Topmost = true;
            }
        }
    }
}
