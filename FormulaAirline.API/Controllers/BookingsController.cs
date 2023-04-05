using FormulaAirline.API.Models;
using FormulaAirline.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FormulaAirline.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly ILogger<BookingsController> _Logger;
        private readonly IMessageProducer _messageProducer;
        public static readonly List<Booking> _Bookings = new List<Booking>();
        public BookingsController(ILogger<BookingsController> logger, IMessageProducer messageProducer)
        {
            _Logger = logger;
            _messageProducer = messageProducer;
        }

        [HttpPost]
        public IActionResult CreatBooking(Booking booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            _Bookings.Add(booking);
            _messageProducer.SendingMessage(booking);
            return Ok();
        }
        
    }
}
