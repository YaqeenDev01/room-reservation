using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using room_reservation.Models;
using room_reservation.ViewModel;


namespace room_reservation.Domain
{
    public class RoomDomain
    {
        private readonly KFUSpaceContext _context;
        private readonly FloorDomain _floorDomain;
        public RoomDomain(KFUSpaceContext context,FloorDomain floorDomain)
        {
            _context = context;
            _floorDomain = floorDomain;
        }

        public async Task<IEnumerable<RoomViewModel>> GetAllRooms()
        {
            return await _context.tblRooms
                .Include(r => r.Floor)
                .ThenInclude(r => r.Building)
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
                    RoomAR = r.RoomType.RoomTypeAR,
                    BuildingNameAr = r.Floor.Building.BuildingNameAr,
                    FloorNo = r.Floor.FloorNo,
                    Floor = r.Floor,
                    Guid = r.Floor.Building.Guid,


                }).ToListAsync();
        }
        public RoomViewModel GetFloorIdByGuid(Guid id)
        {
            try
            {
                var floor = _context.tblFloors
                    .Where(x => x.Guid == id)
                    .Select(f => new RoomViewModel
                    {
                        Id = f.Id,
                        Guid = f.Guid,
                     
                        //Rooms = f.Rooms // not used 
                    })
                    .FirstOrDefault();
                       
                return floor;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception($"Error retrieving floor: {ex.Message}");
            }
        }
        public IEnumerable<tblFloors> GetAllFloors()
        {
            return  _context.tblFloors;
        }

        public async Task <RoomViewModel> GetRoomByGuid(Guid guid)
        {
            try
            {
                return await  _context.tblRooms
                    .Include(x => x.Floor).ThenInclude(x => x.Building).Where(r => r.guid == guid && !r.IsDeleted)
                    .Select(r => new RoomViewModel
                    {
                        Id = r.Id,
                        RoomNo = r.RoomNo,
                        SeatCapacity = r.SeatCapacity,
                        IsActive = r.IsActive,
                        FloorId = r.FloorId,
                        RoomTypeId = r.RoomTypeId,
                        Guid = r.guid,
                        RoomType = r.RoomType,
                        FloorNo = r.Floor.FloorNo,
                        BuildingNameAr = r.Floor.Building.BuildingNameAr,
                        BuildingGuid = r.Floor.Building.Guid,
                       
                    })
                    .FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {
                throw new Exception($"حصل خطأ: {ex.Message}");
            }
        }

        public async Task<tblRooms> GetRoomById(int Id)
        {
            var room = _context.tblRooms
                .Include(b => b.Floor)
                .FirstOrDefault(x => x.Id == Id);
            return room;
        }
    
        public async Task<int> InsertRoom(RoomViewModel room)
        {
            try
            {
               var floor =  _floorDomain.GetFloorByGuid(room.FloorGuid);
                var roomInfo = new tblRooms
                {

                RoomNo = room.RoomNo,
                
                SeatCapacity = room.SeatCapacity,
                IsActive = room.IsActive,
                FloorId = floor.Id,
                RoomTypeId = room.RoomTypeId,
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
        
        public async Task<IEnumerable<RoomViewModel>> GetRoomByFilter(Guid? buildingGuid, Guid? floorGuid,Guid? roomTypeGuid,int? seatCapacity )
        {
            var rooms= await _context.tblRooms.Include(rt => rt.RoomType).Include(f => f.Floor)
                .ThenInclude(b => b.Building)
                .Where(x => 
                  (!buildingGuid.HasValue || x.Floor.Building.Guid == buildingGuid) && 
                     (!floorGuid.HasValue || x.Floor.Guid == floorGuid)
                     && (!roomTypeGuid.HasValue || x.RoomType.guid == roomTypeGuid)
                   && (!seatCapacity.HasValue ||x.SeatCapacity>=seatCapacity))
                    .Select(r=>new RoomViewModel
                {
                    Id=r.Id,
                    RoomNo = r.RoomNo,
                    SeatCapacity = r.SeatCapacity,
                    RoomAR = r.RoomType.RoomTypeAR,
                    Floor = r.Floor,
                    FloorId = r.FloorId,
                    Guid = r.guid,
                    IsActive = r.IsActive,
                    BuildingNameAr = r.Floor.Building.BuildingNameAr,
                    FloorNo = r.Floor.FloorNo,

                }).ToListAsync();
            return rooms;
        }

        

        public async Task DeleteRoom(Guid guid)
        {
            var room = _context.tblRooms.Where(r => r.guid == guid).SingleOrDefault();
            room.IsDeleted = true;
            await _context.SaveChangesAsync();

        }

    }
}

