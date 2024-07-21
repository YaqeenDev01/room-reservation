using System.ComponentModel.DataAnnotations;

namespace room_reservation.Models
{
    public class FloorsLog
    {
        [Key]
        public int Id { get; set; }
        public string OperationType { get; set; }
        public string GrantdBy { get; set; }
        public int FloorId { get; set; }
        public DateTime OperationDate { get; set; }
        public string? AdditionalDetails { get; set; }

    }
}
