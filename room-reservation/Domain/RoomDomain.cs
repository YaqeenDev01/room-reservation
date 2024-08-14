using Microsoft.EntityFrameworkCore;
using room_reservation.Models;
using room_reservation.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            return await _context.tblRooms.Include(r => r.Floor).ThenInclude(r=>r.Building).Include(r => r.RoomType)
                .Where(r => !r.IsDeleted)
                .Select(r => new RoomViewModel
                {
                    Guid = r.guid,
                    RoomNo = r.RoomNo,
                    SeatCapacity = r.SeatCapacity,
                    IsActive = r.IsActive,
                    FloorId = r.FloorId,
                    RoomTypeId = r.RoomTypeId,
                    RoomAR = r.RoomType.RoomAR,
                    BuildingNameAr = r.Floor.Building.BuildingNameAr,
                    Floor = r.Floor,
                    

                }).ToListAsync();
        }

        public IEnumerable<tblFloors> GetAllFloors()
        {
            return  _context.tblFloors;
        }

        public IEnumerable<tblRoomType> GetAllRoomTypes()
        {
            return _context.tblRoomType;
        }

        public RoomViewModel GetRoomByGuid(Guid guid)
        {
            try
            {
                var room =  _context.tblRooms
                    .Where(r => r.guid == guid && !r.IsDeleted)
                    .Select(r => new RoomViewModel
                    {
                        Guid = r.guid,
                        RoomNo = r.RoomNo,
                        SeatCapacity = r.SeatCapacity,
                        IsActive = r.IsActive,
                        FloorId = r.FloorId,
                        RoomTypeId = r.RoomTypeId,
                        RoomType = r.RoomType,

                    })
                    .FirstOrDefault();

                return room;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving room: {ex.Message}");
            }
        }

        public tblRooms GetRoomById(Guid guid)
        {
            var roomId =  _context.tblRooms.Include(r => r.Floor).Include(r => r.RoomType)
                .FirstOrDefault(r => r.guid == guid && !r.IsDeleted);
            return roomId;
        }

        public string InsertRoom(RoomViewModel room)
        {
            try
            {
                tblRooms roomInfo = new tblRooms();
                
                    roomInfo.guid = Guid.NewGuid();
                    roomInfo.RoomNo = room.RoomNo;
                    roomInfo.SeatCapacity = room.SeatCapacity;
                    roomInfo.IsActive = room.IsActive;
                    roomInfo.FloorId = room.FloorId;
                    roomInfo.RoomTypeId = room.RoomTypeId;
                    roomInfo.RoomType = room.RoomType;
                    

                _context.tblRooms.Add(roomInfo);
                 _context.SaveChanges();
                return "1";
            }
            catch (Exception exception)
            {
                return $"Error: {exception.Message}";
            }
        }

        public string EditRoom(RoomViewModel room)
        {
            try
            {
                var roomInfo =  GetRoomById(room.Guid);
     

                roomInfo.RoomNo = room.RoomNo;
                roomInfo.SeatCapacity = room.SeatCapacity;
                roomInfo.IsActive = room.IsActive;
                roomInfo.FloorId = room.FloorId;
                roomInfo.RoomTypeId = room.RoomTypeId;
                roomInfo.RoomType = room.RoomType;

                _context.tblRooms.Update(roomInfo);
                 _context.SaveChanges();
                return "1";
            }
            catch (Exception exception)
            {
                return $"Error: {exception.Message}";
            }
        }

        public string DeleteRoom(Guid guid)
        {
            try
            {
                tblRooms roomInfo =  GetRoomById(guid);
   

                roomInfo.IsDeleted = true;
                _context.tblRooms.Remove(roomInfo);
                 _context.SaveChanges();
                return "1";
            }
            catch (Exception exception)
            {
                return $"Error: {exception.Message}";
            }
        }
    }
}
