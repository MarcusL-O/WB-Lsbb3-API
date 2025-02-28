namespace WB_labb3_API_new_.Models
{
    public class Certificate
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Status { get; set; }
        public DateTime? DateAchieved { get; set; }
        public string CredentialUrl { get; set; }
    }
}
