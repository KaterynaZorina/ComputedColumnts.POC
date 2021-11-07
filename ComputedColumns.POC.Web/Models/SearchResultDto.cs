namespace ComputedColumns.POC.Web.Models
{
    public class SearchResultDto
    {
        public int ResultsCount { get; set; }

        public string ElapsedTimeStr { get; set; }

        public bool IsComputedUsed { get; set; }
    }
}