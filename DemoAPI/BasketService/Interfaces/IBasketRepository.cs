using BasketService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.Interfaces
{
    public interface IBasketRepository 
    {

        public Task<CustomerBasket?> GetBasketAsync(string BasketId);

        public Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket);

        public  Task<bool> RemoveBasketAsync(string BasketId);
        


    }
}
