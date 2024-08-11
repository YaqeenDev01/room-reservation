using Microsoft.EntityFrameworkCore;
using room_reservation.Models;
using room_reservation.ViewModel;

namespace room_reservation.Domain
{
    public class FloorDomain
    {
        private readonly KFUSpaceContext _context;

        public FloorDomain(KFUSpaceContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FloorViewModel>> GetAllFloors()
        {
            return await _context.tblFloors  .Include(f => f.Building) 
                .Select(f => new FloorViewModel
            {
               // Id = f.Id,
                FloorNo = f.FloorNo,
                Guid = f.Guid,
                BuildingId = f.BuildingId,
                BuildingNameAr =f.Building.BuildingNameAr,
                BuildingNo = f.Building.BuildingNo,
                Rooms = f.Rooms,
            }).ToListAsync(); // Select * from tblFloors
        }
        public IEnumerable<tblBuildings>GetAllBuilding()
        {
            return  _context.tblBuildings;
        }
        
                public string addFloor(FloorViewModel floor)
                {

                    try
                    {
                        tblFloors floorInfo = new tblFloors();
                       // floorInfo.Id = floor.Id;
                        floorInfo.FloorNo = floor.FloorNo;
                        floorInfo.Guid = Guid.NewGuid();
                        floorInfo.BuildingId = floor.BuildingId;
                        //floorInfo.Rooms = floor.Rooms;
                        _context.tblFloors.Add(floorInfo);
                        _context.SaveChanges();
                        return "1";


                    }
                    catch (Exception exception)
                    {
                        return $"Error: {exception.Message}";
                    }

                    {
                    }
                }
                public FloorViewModel GetFloorByGuid(Guid id)
                {
                    try
                    {
                        var floor = _context.tblFloors
                            .Where(x => x.Guid == id)
                            .Select(f => new FloorViewModel
                            {
                                //Id = f.Id,
                                FloorNo = f.FloorNo,
                                Guid = f.Guid,
                                BuildingId = f.BuildingId,
                                BuildingNameAr = f.Building.BuildingNameAr,
                                BuildingNo = f.Building.BuildingNo,
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

                public tblFloors GetFloorById(Guid id)
                {
                    var floorId = _context.tblFloors.Include(b => b.Building).FirstOrDefault(x => x.Guid == id);
                   return floorId;
                }

                public string editFloor(FloorViewModel floor)
                {
                    try
                    {
                        var floorInfo = GetFloorById(floor.Guid);
                        //floorInfo.Id = floor.Id;
                        floorInfo.FloorNo = floor.FloorNo;
                        floorInfo.BuildingId = floor.BuildingId;

                        _context.tblFloors.Update(floorInfo);
                        _context.SaveChanges();
                        return "1";
                        
                    }
                    catch (Exception exception)
                    {
                        return $"Error: {exception.Message}";
                    }
                }
                public string deleteFloor(Guid id)
                {

                    try
                    {
                        tblFloors floorInfo =  GetFloorById(id);
                        // is deleted will delete the record in web 
                        floorInfo.IsDeleted = true;
                        // remove will delete the record from Db
                        _context.tblFloors.Remove(floorInfo);
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