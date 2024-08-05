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

        public IEnumerable<RoomViewModel> GetAllRooms()
        {
            return _context.tblRooms.Select(r => new RoomViewModel
            {
                Guid = r.guid,
                RoomNo = r.RoomNo,
                SeatCapacity = r.SeatCapacity,
                IsActive = r.IsActive,
                FloorId = r.FloorId,
                RoomTypeId = r.RoomTypeId,
                Floor = r.Floor,
                RoomType = r.RoomType
            }).ToList();
        }

        public RoomViewModel GetRoomById(Guid Guid)
        {
            var room = _context.tblRooms.SingleOrDefault(r => r.guid == Guid);
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

        public int InsertRoom(RoomViewModel room)
        {
            try
            {
                tblRooms roomInfo = new tblRooms();

                roomInfo.RoomNo = room.RoomNo;
                roomInfo.SeatCapacity = room.SeatCapacity;
                roomInfo.IsActive = room.IsActive;
                roomInfo.FloorId = room.FloorId;
                roomInfo.RoomTypeId = room.RoomTypeId;
                roomInfo.guid = room.Guid;
                

                _context.tblRooms.Add(roomInfo);
                _context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"حدث خطأ {ex.Message}");
                return 0;
            }
        }

        public int EditRoom(RoomViewModel room)
        {
            try
            {
                var roomInfo = _context.tblRooms.SingleOrDefault(r => r.guid == room.Guid);
                if (roomInfo == null) return 0;

                roomInfo.RoomNo = room.RoomNo;
                roomInfo.SeatCapacity = room.SeatCapacity;
                roomInfo.IsActive = room.IsActive;
                roomInfo.FloorId = room.FloorId;
                roomInfo.RoomType = room.RoomType;

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