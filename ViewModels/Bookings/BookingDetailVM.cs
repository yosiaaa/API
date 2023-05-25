using API.Utility;

namespace API.ViewModels.Bookings
{
    public class BookingDetailVM
    {
        public Guid? Guid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public StatusLevel Status { get; set; }
        public string Remarks { get; set; }
        public string BookedNik { get; set; }
        public string FullName { get; set; }
        public string RoomName { get; set; }
    }
}
