using LiveCharts;
using LiveCharts.Defaults;
using SchoolToHomeBehaviorTracking_Interface;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Windows.Controls;

namespace SchoolToHomeBehaviorTracking_Client
{
    /// <summary>
    /// Interaction logic for ScatterPlotGraph.xaml
    /// Graphing sleep and incident data in scatter plot
    /// </summary>
    public partial class ScatterPlotGraph : UserControl
    {
        private List<List<string>> _dateList = new List<List<string>>();
        private bool _teacherGraphs = false;    //indicate if graphing for teacher or parent

        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public string YAxisLabel { get; set; }
        public string GraphTitle { get; set; }
        public ChartValues<ObservablePoint> ScatterValues { get; set; }

        static ChannelFactory<IMetaInterface> channelFactory = new
        ChannelFactory<IMetaInterface>("SchoolToHomeServiceEndpoint");

        static IMetaInterface proxy = channelFactory.CreateChannel();

        public bool TeacherGraphs
        {
            get { return _teacherGraphs; }
            set { _teacherGraphs = value; }
        }

        public ScatterPlotGraph()
        {
            InitializeComponent();
            ScatterValues = new ChartValues<ObservablePoint>();
            this.DataContext = this;
        }

        //graph sleep data from home tracking forms
        public void GraphSleepData(string formName, string firstName, string lastName, string startDate, string endDate)
        {
            _dateList.Clear();
            ScatterValues.Clear();
            
            YAxisLabel = "Hours Slept";

            //graph teacher graphs (data from home tracking forms shared with teahcer)
            if (_teacherGraphs)
                _dateList = proxy.GetBehaviorGraphingData(formName, firstName, lastName, startDate, endDate);
            else  //graph parent graphs (data from all home tracking forms completed by parent)
                _dateList = proxy.GetHomeTrackingGraphingData(firstName, lastName, startDate, endDate);

            Labels = _dateList[0].ToArray();
            graphTitle.Text = formName + " Sleep";
            int counter = 0;

            foreach (string x in _dateList[2])
            {
                try
                {
                    ScatterValues.Add(new ObservablePoint(counter, Convert.ToDouble(x)));
                    counter++;
                }
                catch { }
            }
        }

        //graph number of incidents per day from incident reports
        public void GraphIncidentData(string formName, string firstName, string lastName, string startDate, string endDate)
        {
            _dateList.Clear();
            ScatterValues.Clear();

            YAxisLabel = "Number Incidents";

            _dateList = proxy.GetIncidentGraphingData(firstName, lastName, startDate, endDate);

            Labels = _dateList[0].ToArray();
            graphTitle.Text = formName;
            int counter = 0;

            foreach (string x in _dateList[1])
            {
                try
                {
                    ScatterValues.Add(new ObservablePoint(counter, Convert.ToInt32(x)));
                    counter++;
                }
                catch { }
            }
        }
    }
}
