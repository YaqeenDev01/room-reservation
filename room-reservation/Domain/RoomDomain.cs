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
            return await _context.tblRooms.Include(r => r.Floor).Include(r => r.RoomType)
                .Where(r => !r.IsDeleted)
                .Select(r => new RoomViewModel
                {
                    Guid = r.guid,
                    RoomNo = r.RoomNo,
                    SeatCapacity = r.SeatCapacity,
                    IsActive = r.IsActive,
                    FloorId = r.FloorId,
                    FloorNo = r.Floor.FloorNo,
                    RoomTypeId = r.RoomTypeId,
                    RoomTypeName = r.RoomType.RoomAR
                }).ToListAsync();
        }

        public async Task<IEnumerable<tblFloors>> GetAllFloors()
        {
            return await _context.tblFloors.Where(f => !f.IsDeleted).ToListAsync();
        }

        public async Task<IEnumerable<tblRoomType>> GetAllRoomTypes()
        {
            return await _context.tblRoomType.Where(rt => !rt.IsDeleted).ToListAsync();
        }

        public async Task<RoomViewModel> GetRoomByGuid(Guid guid)
        {
            try
            {
                var room = await _context.tblRooms.Include(r => r.Floor).Include(r => r.RoomType)
                    .Where(r => r.guid == guid && !r.IsDeleted)
                    .Select(r => new RoomViewModel
                    {
                        Guid = r.guid,
                        RoomNo = r.RoomNo,
                        SeatCapacity = r.SeatCapacity,
                        IsActive = r.IsActive,
                        FloorId = r.FloorId,
                        FloorNo = r.Floor.FloorNo,
                        RoomTypeId = r.RoomTypeId,
                        RoomTypeName = r.RoomType.RoomAR
                    })
                    .FirstOrDefaultAsync();

                return room;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving room: {ex.Message}");
            }
        }

        public async Task<tblRooms> GetRoomById(Guid guid)
        {
            return await _context.tblRooms.Include(r => r.Floor).Include(r => r.RoomType)
                .FirstOrDefaultAsync(r => r.guid == guid && !r.IsDeleted);
        }

        public async Task<string> InsertRoom(RoomViewModel room)
        {
            try
            {
                var roomInfo = new tblRooms
                {
                    guid = room.Guid,
                    RoomNo = room.RoomNo,
                    SeatCapacity = room.SeatCapacity,
                    IsActive = room.IsActive,
                    FloorId = room.FloorId,
                    RoomTypeId = room.RoomTypeId,
                    IsDeleted = false
                };

                _context.tblRooms.Add(roomInfo);
                await _context.SaveChangesAsync();
                return "1";
            }
            catch (Exception exception)
            {
                return $"Error: {exception.Message}";
            }
        }

        public async Task<string> EditRoom(RoomViewModel room)
        {
            try
            {
                var roomInfo = await GetRoomById(room.Guid);
                if (roomInfo == null)
                {
                    return $"Error: Room with GUID {room.Guid} not found.";
                }

                roomInfo.RoomNo = room.RoomNo;
                roomInfo.SeatCapacity = room.SeatCapacity;
                roomInfo.IsActive = room.IsActive;
                roomInfo.FloorId = room.FloorId;
                roomInfo.RoomTypeId = room.RoomTypeId;

                _context.tblRooms.Update(roomInfo);
                await _context.SaveChangesAsync();
                return "1";
            }
            catch (Exception exception)
            {
                return $"Error: {exception.Message}";
            }
        }

        public async Task<string> DeleteRoom(Guid guid)
        {
            try
            {
                var roomInfo = await GetRoomById(guid);
                if (roomInfo == null)
                {
                    return $"Error: Room with GUID {guid} not found.";
                }

                roomInfo.IsDeleted = true;
                _context.tblRooms.Update(roomInfo);
                await _context.SaveChangesAsync();
                return "1";
            }
            catch (Exception exception)
            {
                return $"Error: {exception.Message}";
            }
        }
    }
}
