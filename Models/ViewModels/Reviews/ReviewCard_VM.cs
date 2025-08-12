namespace BookWebApp.Models.ViewModels.Reviews
{
    public class ReviewCard_VM
    {
        public string UserName { get; set; }
        public int BookID{ get; set; }
        public string BookTitle { get; set; }
        public string CoverImage { get; set; }
        public string Comment { get; set; }
        public double Rating { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
