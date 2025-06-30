using BasketService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BasketService.Models;
using BasketService.Dtos;
using AutoMapper;
using DemoAPI.Common;

namespace BasketService.Controllers
{
    public class BasketsController : BaseAPIController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketsController(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);

            return Ok(basket ?? new CustomerBasket(id));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
        {

            var MappedBasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(basket);

            var updatedBasket = await _basketRepository.UpdateBasketAsync(MappedBasket);

            if (updatedBasket is null)
            {
                return BadRequest("Problem updating the basket");
            }

            return Ok(updatedBasket);
        }

        [HttpDelete]
        public async Task DeleteBasket(string id)
        {
            var deleted = await _basketRepository.RemoveBasketAsync(id);

        }

    }
}
