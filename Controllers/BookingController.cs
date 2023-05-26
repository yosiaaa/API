using API.Contracts;
using API.Models;
using API.ViewModels.Accounts;
using API.ViewModels.Bookings;
using API.ViewModels.Response;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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

        [HttpGet]
        public IActionResult GetAll()
        {
            var bookings = _bookingRepository.GetAll();
            if (!bookings.Any())
            {
                return NotFound(new ResponseVM<BookingVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Messages = "Booking Not Found",

                });
            }

            var data = bookings.Select(_bookingMapper.Map).ToList();

            return Ok(new ResponseVM<List<BookingVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Messages = "Success Get All Booking",
                Data = new List<BookingVM>(data)
            });
        }

        [HttpGet("BookingDetail")]
        public IActionResult GetAllBookingDetail()
        {
            try
            {
                var bookingDetails = _bookingRepository.GetAllBookingDetail();

                return Ok(new ResponseVM<List<BookingDetailVM>>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Messages = "Data Di Tampilkan",
                    Data = bookingDetails.ToList()
                });
            }
            catch
            {
                return NotFound(new ResponseVM<BookingDetailVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Messages = "Data tidak bisa ditampilkan"
                });
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

                    return NotFound(new ResponseVM<BookingDetailVM>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Messages = "Not Found"
                    });
                }

                return Ok(new ResponseVM<BookingDetailVM>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Messages = "GetByGuid Success",
                    Data = booking
                });
            }
            catch
            {
                return NotFound(new ResponseVM<BookingDetailVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Messages = "Not Found"
                });
            }
        }


        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var booking = _bookingRepository.GetByGuid(guid);
            if (booking is null)
            {
                return NotFound(new ResponseVM<BookingVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Messages = "Booking By Guid Not Found",
                });
            }

            var data = _bookingMapper.Map(booking);

            return Ok(new ResponseVM<BookingVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Messages = "Success Get By Guid",
                Data = data
            });
        }

        [HttpPost]
        public IActionResult Create(BookingVM bookingVM)
        {
            var bookingConverted = _bookingMapper.Map(bookingVM);

            var result = _bookingRepository.Create(bookingConverted);
            if (result is null)
            {
                return BadRequest(new ResponseVM<BookingVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Messages = "Booking Failed",
                    Data = null
                });
            }

            var resultConverted = _bookingMapper.Map(result);

            return Ok(new ResponseVM<BookingVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Messages = "Success Create Booking",
                Data = resultConverted
            });
        }

        [HttpPut]
        public IActionResult Update(BookingVM bookingVM)
        {
            var bookingConverted = _bookingMapper.Map(bookingVM);

            var isUpdated = _bookingRepository.Update(bookingConverted);
            if (!isUpdated)
            {
                return BadRequest(new ResponseVM<BookingVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Messages = "Failed to Update Booking",
                });
            }
            var resultUpdateConverted = _bookingMapper.Map(bookingConverted);

            return Ok(new ResponseVM<BookingVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Messages = "Success Update Booking",
                Data = resultUpdateConverted
            });
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _bookingRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest(new ResponseVM<BookingVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Messages = "Failed to Delete Booking",
                });
            }
            return Ok(new ResponseVM<Guid>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Messages = "Success Delete Booking"
            });
        }

        [HttpGet("bookingduration")]
        public IActionResult GetDuration()
        {
            var bookingLengths = _bookingRepository.GetBookingDuration();
            if (!bookingLengths.Any())
            {
                return NotFound(new ResponseVM<BookingDurationVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Messages = "Booking Duration Not Found",
                });
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
