namespace room_reservation.ViewModel
{
    public class RoomTypeViewModel
    {

        public Guid guid { get; set; } = Guid.NewGuid();
        public string RoomAR { get; set; }
        
        public bool IsDeleted { get; set; }
        
    }
}
