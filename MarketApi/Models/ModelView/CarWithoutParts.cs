namespace MarketApi.Models.ModelView
{
    public class CarWithoutParts
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public DateTime Year { get; set; }
        public string Gear { get; set; }
        public double Km { get; set; }
    }
}
