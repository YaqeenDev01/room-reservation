using room_reservation.Models;
using room_reservation.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace room_reservation.Domain
{
    public class RoomDomain
    {
        private readonly KFUSpaceContext _context;

        public RoomDomain(KFUSpaceContext context)
        {
            _context = context;
        }

        public IEnumerable<RoomViewModel> getAllRooms()
        {
            return _context.tblRooms.Where(r => !r.IsDeleted).Select(r => new RoomViewModel
            {
                Id = r.Id,
                RoomNo = r.RoomNo,
                SeatCapacity = r.SeatCapacity,
                IsActive = r.IsActive,
                FloorId = r.FloorId,
                RoomTypeId = r.RoomTypeId,
                IsDeleted = r.IsDeleted,
                Guid = r.Guid,
                Floor = r.Floor,
                RoomType = r.RoomType
            }).ToList();
        }

        public RoomViewModel GetRoomById(int id)
        {
            var room = _context.tblRooms.SingleOrDefault(r => r.Id == id && !r.IsDeleted);
            if (room == null) return null;

            return new RoomViewModel
            {
                Id = room.Id,
                RoomNo = room.RoomNo,
                SeatCapacity = room.SeatCapacity,
                IsActive = room.IsActive,
                FloorId = room.FloorId,
                RoomTypeId = room.RoomTypeId,
                IsDeleted = room.IsDeleted,
                Guid = room.Guid,
                Floor = room.Floor,
                RoomType = room.RoomType
            };
        }

        public int InsertRoom(RoomViewModel room)
        {
            try
            {
                tblRooms roomInfo = new tblRooms
                {
                    RoomNo = room.RoomNo,
                    SeatCapacity = room.SeatCapacity,
                    IsActive = room.IsActive,
                    FloorId = room.FloorId,
                    RoomTypeId = room.RoomTypeId,
                    IsDeleted = false,
                    Guid = room.Guid
                };

                _context.tblRooms.Add(roomInfo);
                _context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting room: {ex.Message}");
                return 0;
            }
        }

        public int EditRoom(RoomViewModel room)
        {
            try
            {
                var roomInfo = _context.tblRooms.SingleOrDefault(r => r.Id == room.Id && !r.IsDeleted);
                if (roomInfo == null) return 0;

                roomInfo.RoomNo = room.RoomNo;
                roomInfo.SeatCapacity = room.SeatCapacity;
                roomInfo.IsActive = room.IsActive;
                roomInfo.FloorId = room.FloorId;
                roomInfo.RoomTypeId = room.RoomTypeId;
                roomInfo.IsDeleted = room.IsDeleted;

                _context.tblRooms.Update(roomInfo);
                _context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error editing room: {ex.Message}");
                return 0;
            }
        }

        public int DeleteRoom(int id)
        {
            try
            {
                var room = _context.tblRooms.SingleOrDefault(r => r.Id == id && !r.IsDeleted);
                if (room == null) return 0;

                room.IsDeleted = true;
                _context.tblRooms.Update(room);
                _context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting room: {ex.Message}");
                return 0;
            }
        }

        public IEnumerable<tblFloors> getAllFloors()
        {
            return _context.tblFloors.ToList();
        }

        public IEnumerable<tblRoomType> getAllRoomTypes()
        {
            return _context.tblRoomType.ToList();
        }
    }
}
