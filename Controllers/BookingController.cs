using API.Contracts;
using API.Models;
using API.ViewModels.Bookings;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper<Booking, BookingVM> _bookingMapper;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRoomRepository _roomRepository;
        public BookingController(IBookingRepository bookingRepository, IMapper<Booking, BookingVM> bookingMapper, IEmployeeRepository employeeRepository, IRoomRepository roomRepository)
        {
            _bookingRepository = bookingRepository;
            _bookingMapper = bookingMapper;
            _employeeRepository = employeeRepository;
            _roomRepository = roomRepository;
        }

        [HttpGet("BookingDetail")]
        public IActionResult GetAllBookingDetail()
        {
            try
            {
                var bookingDetails = _bookingRepository.GetAllBookingDetail();

                return Ok(bookingDetails);

            }
            catch
            {
                return Ok("error");
            }
        }

        [HttpGet("BookingDetailByGuid")]
        public IActionResult GetDetailByGuid(Guid guid)
        {
            try
            {
                var booking = _bookingRepository.GetBookingDetailByGuid(guid);
                if (booking is null)
                {

                    return NotFound();
                }

                return Ok(booking);
            }
            catch
            {
                return Ok("error");
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var bookings = _bookingRepository.GetAll();
            if (!bookings.Any())
            {
                return NotFound();
            }

            var data = bookings.Select(_bookingMapper.Map).ToList();

            return Ok(data);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var booking = _bookingRepository.GetByGuid(guid);
            if (booking is null)
            {
                return NotFound();
            }

            var data = _bookingMapper.Map(booking);

            return Ok(data);
        }

        [HttpPost]
        public IActionResult Create(BookingVM bookingVM)
        {
            var bookingConverted = _bookingMapper.Map(bookingVM);

            var result = _bookingRepository.Create(bookingConverted);
            if (result is null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(BookingVM bookingVM)
        {
            var bookingConverted = _bookingMapper.Map(bookingVM);

            var isUpdated = _bookingRepository.Update(bookingConverted);
            if (!isUpdated)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _bookingRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpGet("bookingduration")]
        public IActionResult GetDuration()
        {
            var bookingLengths = _bookingRepository.GetBookingDuration();
            if (!bookingLengths.Any())
            {
                return NotFound();
            }

            return Ok(bookingLengths);
        }

        /*[HttpGet("bookinglength")]
        public IActionResult GetDuration()
        {
            var bookingLengths = _bookingRepository.GetBookingDuration();
            if (!bookingLengths.Any())
            {
                return NotFound();
            }

            return Ok(bookingLengths);
        }*/
    }
}
