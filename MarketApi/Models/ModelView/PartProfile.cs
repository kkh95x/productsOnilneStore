using AutoMapper;

namespace MarketApi.Models.ModelView
{
    public class PartProfile:Profile
    {
        public PartProfile() {
            
       base.CreateMap<Part,PartWitouthCars>().ReverseMap();
        }
    }
}
