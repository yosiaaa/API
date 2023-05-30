using API.Contracts;
using API.Models;
using API.Repositories;
using API.ViewModels.Bookings;
using API.ViewModels.Educations;
using API.ViewModels.Others;
using API.ViewModels.Roles;
using API.ViewModels.Rooms;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomController : BaseController<Room, RoomVM>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IMapper<Room, RoomVM> _mapper;
    public RoomController(IRoomRepository roomRepository, IMapper<Room, RoomVM> mapper) : base(roomRepository, mapper)
    {
        _roomRepository = roomRepository;
        _mapper = mapper;
    }

    //kel 4
    [HttpGet("AvailableRoom")]
    public IActionResult GetAvailableRoom()
    {
        try
        {
            var room = _roomRepository.GetAvailableRoom();
            if (room is null)
            {
                return NotFound(new ResponseVM<RoomBookedTodayVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Available Room not found",
                    Data = null
                });
            }

            return Ok(new ResponseVM<IEnumerable<RoomBookedTodayVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success",
                Data = room
            });
        }
        catch
        {
            return BadRequest(new ResponseVM<RoomBookedTodayVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Available Room Error",
                Data = null
            });
        }
    }


    //kel 1
    [HttpGet("CurrentlyUsedRooms")]
    public IActionResult GetCurrentlyUsedRooms()
    {
        var room = _roomRepository.GetCurrentlyUsedRooms();
        if (room is null)
        {
            return NotFound(new ResponseVM<RoomUsedVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Room Used not found",
                Data = null
            });
        }

        return Ok(new ResponseVM<IEnumerable<RoomUsedVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success",
            Data = room
        });
    }

    //kel 1
    [HttpGet("CurrentlyUsedRoomsByDate")]
    public IActionResult GetCurrentlyUsedRooms(DateTime dateTime)
    {
        var room = _roomRepository.GetByDate(dateTime);
        if (room is null)
        {
            return NotFound(new ResponseVM<MasterRoomVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Currently Used Rooms not found",
                Data = null
            });
        }

        return Ok(new ResponseVM<IEnumerable<MasterRoomVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success",
            Data = room
        });
    }
}