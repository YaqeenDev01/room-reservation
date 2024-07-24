﻿using room_reservation.Models;

namespace room_reservation.ViewModel
{
    //In view models, we will include required and display name. 
    //Remove those that will not be displayed to the users. For instance, isDeleted is removed.
    //View model controls what input will be entered and their constraints.
    public class PermissionViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public Guid guid { get; set; } = Guid.NewGuid();

        //Ask Eng .... fk 
        public tblRoles Role { get; set; }
        public int RoleId { get; set; }
        public tblBuildings Building { get; set; }

        public int? BuildingId { get; set; }


    }
}
