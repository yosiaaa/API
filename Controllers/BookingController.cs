using API.Contracts;
using API.Models;
using API.Repositories;
using API.ViewModels.Accounts;
using API.ViewModels.Bookings;
using API.ViewModels.Others;
using API.ViewModels.Universities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Net;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingController : BaseController<Booking, BookingVM>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IMapper<Booking, BookingVM> _mapper;
    public BookingController(IBookingRepository booking, IMapper<Booking, BookingVM> mapper) : base(booking, mapper)
    {
        _bookingRepository = booking;
        _mapper = mapper;
    }

    //kel 4
    [Authorize(Roles = "Manager")]
    [HttpGet("BookingDetail")]
    public IActionResult GetAllBookingDetail()
    {
        try
        {
            var bookingDetails = _bookingRepository.GetAllBookingDetail();

            return Ok(new ResponseVM<IEnumerable<BookingDetailVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "success",
                Data = bookingDetails
            });

        }
        catch
        {
            return NotFound(new ResponseVM<BookingDetailVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Booking Detail not Found",
                Data = null
            });
        }
    }

    //kel 4
    [HttpGet("BookingDetail/{guid}")]
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
                    Message = "Booking Detail not found",
                    Data = null
                });
            }

            return Ok(new ResponseVM<BookingDetailVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success",
                Data = booking
            });
        }
        catch
        {
            return BadRequest(new ResponseVM<BookingVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Booking Detail Error",
                Data = null
            });
        }
    }


    //kel 3
    [HttpGet("bookingduration")]
    public IActionResult GetDuration()
    {
        var bookingLengths = _bookingRepository.GetBookingDuration();
        if (!bookingLengths.Any())
        {
            return NotFound(new ResponseVM<BookingDetailVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Booking Duration not found",
                Data = null
            });
        }

        return Ok(new ResponseVM<IEnumerable<BookingDurationVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success",
            Data = bookingLengths
        });

    }
}