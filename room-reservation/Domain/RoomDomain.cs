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

        public async Task<IEnumerable<RoomViewModel>> GetAllRooms()
        {
            return _context.tblRooms.Where(r => !r.IsDeleted).Select(r => new RoomViewModel
            {
                Id = r.Id,
                RoomNo = r.RoomNo,
                SeatCapacity = r.SeatCapacity,
                IsActive = r.IsActive,
                FloorId = r.FloorId,
                RoomTypeId = r.RoomTypeId,
                Floor = r.Floor,
                RoomType = r.RoomType
            }).ToList();
        }

        public async Task<RoomViewModel> GetRoomById(Guid guid)
        {
            var room = _context.tblRooms.SingleOrDefault(r => r.guid == guid && !r.IsDeleted);
            if (room == null) return null;

            return new RoomViewModel
            {
                Guid = room.guid,
                RoomNo = room.RoomNo,
                SeatCapacity = room.SeatCapacity,
                IsActive = room.IsActive,
                FloorId = room.FloorId,
                RoomTypeId = room.RoomTypeId,
                Floor = room.Floor,
                RoomType = room.RoomType
            };
        }

        public async Task<string> InsertRoom(RoomViewModel room)
        {
            try
            {
                var roomInfo = new tblRooms
                {
                    guid = new Guid(),
                    RoomNo = room.RoomNo,
                    SeatCapacity = room.SeatCapacity,
                    IsActive = room.IsActive,
                    FloorId = room.FloorId,
                    RoomTypeId = room.RoomTypeId,
                    IsDeleted = false
                };

                _context.Add(roomInfo);
                _context.SaveChangesAsync();
                return "added";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting room: {ex.Message}");
                return ex.Message;
            }
        }

        public int EditRoom(RoomViewModel room)
        {
            try
            {
                var roomInfo = _context.tblRooms.SingleOrDefault(r => r.guid == room.Guid && !r.IsDeleted);
                if (roomInfo == null) return 0;

                roomInfo.RoomNo = room.RoomNo;
                roomInfo.SeatCapacity = room.SeatCapacity;
                roomInfo.IsActive = room.IsActive;
                roomInfo.FloorId = room.FloorId;
                roomInfo.RoomTypeId = room.RoomTypeId;

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

        public int DeleteRoom(Guid guid)
        {
            try
            {
                var room = _context.tblRooms.SingleOrDefault(r => r.guid == guid && !r.IsDeleted);
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

        public IEnumerable<tblFloors> GetAllFloors()
        {
            return _context.tblFloors.ToList();
        }

        public IEnumerable<tblRoomType> GetAllRoomTypes()
        {
            return _context.tblRoomType.ToList();
        }
    }
}
