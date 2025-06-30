using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoAPI.Common;
using BasketService.Models;

namespace BasketService.Models
{
    public class Product : BaseEntity
    {

        public string Name { get; set; }

        public int Price { get; set; }

        public string PictureUrl { get; set; }

        public int BrandId { get; set; }

        public Brand Brand { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }




    }
}
