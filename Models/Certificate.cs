namespace WB_labb3_API_new_.Models
{
    public class Certificate
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime? DateAchieved { get; set; }
        public string CredentialUrl { get; set; } = string.Empty;
    }
}
