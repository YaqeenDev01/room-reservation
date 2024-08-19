using room_reservation.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace room_reservation.ViewModel

{
    public class RoomTypeViewModel
    {
        public int Id { get; set; }
        public Guid guid { get; set; } = Guid.NewGuid();


        public string RoomAR { get; set; }
        
        public bool IsDeleted { get; set; }

        
    }
}
