using AutoMapper;

namespace MarketApi.Models.ModelView
{
    public class CarProfile:Profile
    {
        public CarProfile() {
            CreateMap<Car, CarWithoutParts>().ReverseMap();

        }
    }
}
