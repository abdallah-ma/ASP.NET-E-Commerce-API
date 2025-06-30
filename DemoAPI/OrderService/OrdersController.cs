using OrderService.Models;
using OrderService.Dtos;
using Order = OrderService.Models.Order;
using OrderService.Interfaces;
using DemoAPI.Common;

using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using DemoAPI.Common.Errors;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using OrderService.Helpers;


namespace DemoAPI.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class OrdersController : BaseAPIController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;


        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost]

        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto order)
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var Address = _mapper.Map<AddressDto, Address>(order.ShipToAddress);

            var Order = await _orderService.CreateOrderAsync(BuyerEmail, Address, order.DeliveryMethodId, order.BasketId);

            if (Order == null)
            {
                return BadRequest(new ApiResponse(400, null));
            }

            return Ok(Order);
        }


        [HttpPut]

        public async Task<ActionResult<OrderToReturnDto>> UpdateOrder(Order order)
        {
            await _orderService.UpdateOrderAsync(order);

            return Ok();
        }

        [HttpGet]

        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var orders = await _orderService.GetUserOrdersAsync(Email);

            if (orders == null || !orders.Any())
            {
                return NotFound(new ApiResponse(404));
            }


            return Ok(_mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(orders));
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<OrderToReturnDto>> GetOrderForUser(int id)
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var order = await _orderService.GetUserOrderByIdAsync(id, Email);
            if (order == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(_mapper.Map<Order, OrderToReturnDto>(order));


        }

        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            var DeliveryMethods = await _orderService.GetDeliveryMethodsAsync();
            return Ok(DeliveryMethods);
        }

        [HttpGet("deliveryMethod/{id}")]

        public async Task<ActionResult<DeliveryMethod>> GetDeliveryMethod(int id)
        {
            var DeliveryMethod = await _orderService.GetDeliveryMethodAsync(id);
            if (DeliveryMethod == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(DeliveryMethod);

        }

    }
}
