//Model for selecting which forms to graph
namespace SchoolToHomeBehaviorTracking_Client
{
    public class GraphSelectionModel
    {
        private string _formName;
        private bool _isSelected;

        public GraphSelectionModel(string formName, bool isSelected)
        {
            _formName = formName;
            _isSelected = isSelected;
        }

        public string FormName { get { return _formName; } }
        public bool IsSelected
        {
            get { return _isSelected;  }
            set { _isSelected = value; }
        }
    }
}
