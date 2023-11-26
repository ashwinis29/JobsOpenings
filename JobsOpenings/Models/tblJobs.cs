using System.Text.Json.Serialization;

namespace JobsOpenings.Models
{
    public class tblJobs
    {
        public int Id { get; set; }
        public string code { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public Nullable<int> locationId { get; set; }
        public Nullable<int> departmentId { get; set; }
        public System.DateTime postedDate { get; set; }
        public System.DateTime closingDate { get; set; }
    }
}
