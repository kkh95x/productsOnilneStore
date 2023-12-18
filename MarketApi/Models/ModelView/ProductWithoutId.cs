namespace MarketApi.Models.ModelView
{
    public class ProductWithoutId
    {
        public DateTime createdAt { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public double price { get; set; }
        public String photoUrl { get; set; }
    }
}
