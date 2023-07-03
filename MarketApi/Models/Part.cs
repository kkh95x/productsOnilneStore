using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MarketApi.Models
{
    public class Part
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quntity { get; set; }

        public int SupplierId { get; set; }
        public Supplier supplierModel { get; set; }
        public List<Car>? Cars { get; set; }


    }
}
