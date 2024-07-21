using System.ComponentModel.DataAnnotations;

namespace room_reservation.Models
{
    public class RoomsLog
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string GrantdBy { get; set; }
        public string OperationType { get; set; }
        public DateTime OperationDate { get; set; }
        public string? AdditionalDetails { get; set; }
    }
}
