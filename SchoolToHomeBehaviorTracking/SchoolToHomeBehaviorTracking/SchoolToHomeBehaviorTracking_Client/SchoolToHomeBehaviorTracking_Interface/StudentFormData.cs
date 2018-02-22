namespace SchoolToHomeBehaviorTracking_Interface
{
    public class StudentFormData
    {
        public string FormID { get; set; }
        public string FormName { get; set; }
        public string FormDescription { get; set; }
        public string StudentFName { get; set; }
        public string StudentLName { get; set; }
        public string FormDate { get; set; }
        public string EndDate { get; set; }
        public bool Shared { get; set; }

        public int BehaviorRating { get; set; }
        public string Data { get; set; }
    }
}
