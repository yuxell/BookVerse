namespace BookWebApp.Models.ViewModels.Reviews
{
    public class Review_VM
    {
        public int BookID { get; set; }
        public string ReviewText { get; set; }
        public int Rating { get; set; }
        public int? UserID { get; set; }
    }
}
