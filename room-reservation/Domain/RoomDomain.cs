using Microsoft.AspNetCore.Mvc;
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
            return await _context.tblRooms
                .Include(r => r.Floor)
                .ThenInclude(r=>r.Building)
                .Include(r => r.RoomType)
                .Where(r => !r.IsDeleted)
                .Select(r => new RoomViewModel
                {
                    Id = r.Id,
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


        public RoomViewModel GetRoomByGuid(int Id)
        {
            try
            {
                var room =  _context.tblRooms
                    .Where(r => r.Id == Id && !r.IsDeleted)
                    .Select(r => new RoomViewModel
                    {
                        Id = r.Id,
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
                throw new Exception($"حصل خطأ: {ex.Message}");
            }
        }

        public async Task<RoomViewModel>GetRoomById(int Id)
        {
            return await _context.tblRooms.Where(r => r.Id == Id).Select(
                r => new RoomViewModel
                {
                    Id = r.Id,
                    RoomNo = r.RoomNo,
                    SeatCapacity = r.SeatCapacity,
                    IsActive = r.IsActive,
                    FloorId = r.FloorId,
                    RoomTypeId = r.RoomTypeId,
                    RoomType = r.RoomType
                }
                ).FirstOrDefaultAsync();

        }
        [HttpPost]
        public async Task<int> InsertRoom(RoomViewModel room)
        {
            try
            {
                var roomInfo = new tblRooms
                {

                RoomNo = room.RoomNo,
                SeatCapacity = room.SeatCapacity,
                IsActive = room.IsActive,
                FloorId = room.FloorId,
                RoomTypeId = room.RoomTypeId,
                RoomType = room.RoomType
                };  

                _context.Add(roomInfo);
                 await _context.SaveChangesAsync();
                return 1;
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Error: {exception.Message}");
                return 0;
            }
        }

        public async Task<bool> EditRoom(RoomViewModel roomViewModel)
        {
            try
            {
                var room = await _context.tblRooms
                    .FirstOrDefaultAsync(x => x.Id == roomViewModel.Id);


                room.RoomNo = roomViewModel.RoomNo;
                room.SeatCapacity = roomViewModel.SeatCapacity;
                room.IsActive = roomViewModel.IsActive;
                room.FloorId = roomViewModel.FloorId;
                room.RoomTypeId = roomViewModel.RoomTypeId;
                room.RoomType = roomViewModel.RoomType;

                _context.tblRooms.Update(room);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        public async Task DeleteRoom(int id)
        {
            var room = _context.tblRooms.Where(r => r.Id == id).SingleOrDefault();
            room.IsDeleted = true;
            await _context.SaveChangesAsync();

        }

    }
}

