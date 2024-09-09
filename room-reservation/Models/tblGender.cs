namespace room_reservation.Models
{
    public class tblGender
    {
        public int Id { get; set; }
        public string GenderAR { get; set; }
        public string GenderEN { get; set; }
        public Guid guid { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<tblBuildings> BuildingsCollection { get; set; }
        public ICollection<tblBookings> BookingsCollection { get; set; }


    }
}
