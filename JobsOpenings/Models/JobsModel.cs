namespace JobsOpenings.Models
{
    public class JobsModel
    {
        public class JobOpenings
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public int LocationId { get; set; }
            public int DepartmentId { get; set; }
            public DateTime ClosingDate { get; set; }
        }

        public class PageRequest
        {
            public string q { get; set; }
            public int PageNo { get; set; }
            public int PageSize { get; set; }
            public int? LocationId { get; set; }
            public int? DepartmentId { get; set; }
        }

        public class PageResponse
        {
            public int Total { get; set; }
            public List<Data> data { get; set; }
        }
        public class Data
        {
            public int Id { get; set; }
            public string Code { get; set; }
            public string Title { get; set; }
            public string Location { get; set; }
            public string Department { get; set; }
            public DateTime PostedDate { get; set; }
            public DateTime ClosingDate { get; set; }
        }

        public class Details
        {
            public int Id { get; set; }
            public string Code { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public Location Location { get; set; }
            public Department Department { get; set; }
            public DateTime PostedDate { get; set; }
            public DateTime ClosingDate { get; set; }

        }
    }
}
