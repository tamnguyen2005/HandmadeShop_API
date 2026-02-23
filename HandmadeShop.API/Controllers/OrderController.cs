using HandmadeShop.Application.DTOs.Order;
using HandmadeShop.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HandmadeShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddNewOrder(CreateOrderRequest request)
        {
            await _orderService.CreateOrderAsync(request);
            return StatusCode(StatusCodes.Status201Created, new { message = "Create new order successfully !" });
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateOrderStatus(Guid id, [FromBody] string status)
        {
            await _orderService.UpdateOrderStatusAsync(id, status);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUserOrder()
        {
            return Ok(await _orderService.GetAllUserOrderAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderDetailById(Guid id)
        {
            return Ok(await _orderService.GetOrderDetailAsync(id));
        }
    }
}