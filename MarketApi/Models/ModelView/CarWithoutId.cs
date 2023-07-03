namespace MarketApi.Models.ModelView
{
    public class CarWithoutId
    {
        public string Model { get; set; }
        public DateTime Year { get; set; }
        public string Gear { get; set; }
        public double Km { get; set; }
        public  List<int> PartsId { get; set; }


    }
}
