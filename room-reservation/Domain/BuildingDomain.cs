using Microsoft.EntityFrameworkCore;
using room_reservation.Models;
using room_reservation.ViewModel;

namespace room_reservation.Domain
{
    public class BuildingDomain
    {
        private readonly KFUSpaceContext _context;
        public BuildingDomain(KFUSpaceContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BuildingViewModel>> GetAllBuilding()
        {
            return await _context.tblBuildings.Select(x => new BuildingViewModel
            {
                Id = x.Id,
                BuildingNameEn = x.BuildingNameEn,
                BuildingNameAr = x.BuildingNameAr,
                BuildingNo = x.BuildingNo,
                Code = x.Code,
                Guid = x.Guid,
                //Floors = x.Floors,
                //Permissions = x.Permissions,
            }).ToListAsync();
        }

        public async Task<IEnumerable<tblBuildings>> getAllBuilding()
        {
            return await _context.tblBuildings.ToListAsync();
        }
        public string InsertBuilding(BuildingViewModel buildings)
        {
            try
            {
                tblBuildings buildings1 = new tblBuildings();
                buildings1.Id = buildings.Id;
                buildings1.BuildingNameEn = buildings.BuildingNameEn;
                buildings1.BuildingNameAr = buildings.BuildingNameAr;
                buildings1.Code = buildings.Code;
                buildings1.BuildingNo = buildings.BuildingNo;
                buildings1.Guid = Guid.NewGuid();

                _context.tblBuildings.Add(buildings1);
                _context.SaveChanges();
                return "successful";
            }
            catch (Exception ex)
            {
                // تسجيل الخطأ أو معالجته هنا
                return $"Error: {ex.Message}";
            }
        }
     
        public tblBuildings getBuildingByGuid(Guid id)

        {
            return _context.tblBuildings.FirstOrDefault(x => x.Guid == id);
        }

        public string updatBuilding(BuildingViewModel buildings)
        {
            tblBuildings buildings1 =getBuildingByGuid(buildings.Guid);
            buildings1.Id = buildings.Id;
            buildings1.BuildingNameEn = buildings.BuildingNameEn;
            buildings1.BuildingNameAr = buildings.BuildingNameAr;
            buildings1.Code = buildings.Code;
            buildings1.BuildingNo = buildings.BuildingNo;
            buildings1.Guid = buildings1.Guid;

            _context.tblBuildings.Update(buildings1);
            _context.SaveChanges();
            return "sucssful";
        }
        public BuildingViewModel getBuildingByid(Guid id)
        {
            var buildingId= _context.tblBuildings.FirstOrDefault(x => x.Guid == id);
            BuildingViewModel models = new BuildingViewModel
            {
                Id = buildingId.Id,
                BuildingNameAr = buildingId.BuildingNameAr,
                BuildingNo = buildingId.BuildingNo,
                BuildingNameEn = buildingId.BuildingNameEn,
                Code = buildingId.Code,
            };
            return models;
        }

        public string DeletBulding(BuildingViewModel buildings)
        {
            try
            {
                tblBuildings buildings1 = getBuildingByGuid(buildings.Guid);
                buildings1.Id = buildings.Id;
                buildings1.BuildingNameEn = buildings.BuildingNameEn;
                buildings1.BuildingNameAr = buildings.BuildingNameAr;
                buildings1.Code = buildings.Code;
                buildings1.BuildingNo = buildings.BuildingNo;

                _context.tblBuildings.Remove(buildings1);
                _context.SaveChanges();
                return "successful";
            }
            catch (Exception ex)
            {
                // تسجيل الخطأ أو معالجته هنا
                return $"Error: {ex.Message}";
            }
        }
    }
}