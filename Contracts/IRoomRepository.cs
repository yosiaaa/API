using API.Models;
using API.ViewModels.Rooms;

namespace API.Contracts
{
    public interface IRoomRepository : IGeneralRepository<Room>
    {
        //kel 4
        IEnumerable<RoomBookedTodayVM> GetAvailableRoom();

        //kel 1
        IEnumerable<MasterRoomVM> GetByDate(DateTime dateTime);
        IEnumerable<RoomUsedVM> GetCurrentlyUsedRooms();
    }
}
