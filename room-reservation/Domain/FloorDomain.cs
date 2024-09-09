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
            return await _context.tblFloors.Where(floor => !floor.IsDeleted).Include(f => f.Building) 
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
        
                public async Task <int> addFloor(FloorViewModel floor)
                {

                    try
                    {
                        // Check if FloorNo and building ID exists or not in db 
                        var floorExists = _context.tblFloors
                            .Any(fn => fn.FloorNo == floor.FloorNo && fn.BuildingId == floor.BuildingId && fn.IsDeleted==false);
                        
                        if (floorExists)
                        {
                            //if it duplicates exists 
                            return 2;
                        }
                        tblFloors floorInfo = new tblFloors();
                       // floorInfo.Id = floor.Id;
                        floorInfo.FloorNo = floor.FloorNo;
                        floorInfo.Guid = Guid.NewGuid();
                        floorInfo.BuildingId = floor.BuildingId;
                        //floorInfo.Rooms = floor.Rooms;
                        floorInfo.IsDeleted = false;
                        _context.tblFloors.Add(floorInfo);
                        await _context.SaveChangesAsync();
                        return 1;


                    }
                    catch (Exception exception)
                    {    
                       // Console.WriteLine($"Error: {exception.Message}");
                        return 0;
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
                
                public async Task<IList<FloorViewModel>> GetFloorByBuildingGuid(Guid id)
                {
                    try
                    {
                        var floor = await _context.tblFloors
                            .Where(x => x.Building.Guid == id && x.IsDeleted == false)
                            .Select(f => new FloorViewModel
                            {
                                //Id = f.Id,
                                FloorNo = f.FloorNo,
                                Guid = f.Guid,
                                BuildingId = f.BuildingId,
                                BuildingNameAr = f.Building.BuildingNameAr,
                                BuildingNo = f.Building.BuildingNo,
                                //Rooms = f.Rooms // not used 
                            }).ToListAsync();
                       
                        return floor;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Error retrieving floor: {ex.Message}");
                    }
                }
                
           


                public tblFloors GetFloorById(Guid id)
                {
                    var floorId = _context.tblFloors.Include(b => b.Building).FirstOrDefault(x => x.Guid == id);
                   return floorId;
                }

                public async Task<int>  editFloor(FloorViewModel floor)
                {
                    try
                    {
                        // Check if FloorNo and building ID exists or not in db 
                        var floorExists = _context.tblFloors
                            .Any(fn => fn.FloorNo == floor.FloorNo && fn.BuildingId == floor.BuildingId);
                        
                        if (floorExists)
                        {
                            //if it doesnt exist
                            return 0;
                        }
                        var floorInfo = GetFloorById(floor.Guid);
                        //floorInfo.Id = floor.Id;
                        floorInfo.FloorNo = floor.FloorNo;
                        floorInfo.BuildingId = floor.BuildingId;

                        _context.tblFloors.Update(floorInfo);
                       await _context.SaveChangesAsync();
                        return 1;
                        
                    }
                    catch (Exception exception)
                    {
                       // Console.WriteLine($"Error: {exception.Message}");
                        return 0;
                    }
                }
                public async Task DeleteFloor(Guid id)
                {
                    tblFloors floor = GetFloorById(id);                        // is deleted will delete the record in web 
                        floor.IsDeleted = true;
                        // remove will delete the record from Db
                       // _context.tblFloors.Remove(floorInfo);
                        await _context.SaveChangesAsync();
                    

                   
                }
    }
}