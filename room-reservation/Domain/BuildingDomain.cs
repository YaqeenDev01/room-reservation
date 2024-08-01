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
            return await _context.tblBuildings.Where(x => x.IsDeleted == false).Select(x => new BuildingViewModel
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

        public int InsertBuilding(BuildingViewModel buildings)
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
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public BuildingViewModel getBuildingByid(Guid id)
        {
            var buildingId = _context.tblBuildings.FirstOrDefault(x => x.Guid == id && x.IsDeleted == false);
            BuildingViewModel models = new BuildingViewModel
            {
                Guid = buildingId.Guid,
                BuildingNameAr = buildingId.BuildingNameAr,
                BuildingNo = buildingId.BuildingNo,
                BuildingNameEn = buildingId.BuildingNameEn,
                Code = buildingId.Code,
            };
            return models;
        }
        public tblBuildings getBuildingByGuid(Guid id)

        {
            return _context.tblBuildings.FirstOrDefault(x => x.Guid == id);
        }

        public int updatBuilding(BuildingViewModel buildings)
        {
            try
            {
                tblBuildings buildingsinfo = getBuildingByGuid(buildings.Guid);

                buildingsinfo.BuildingNameEn = buildings.BuildingNameEn;
                buildingsinfo.BuildingNameAr = buildings.BuildingNameAr;
                buildingsinfo.Code = buildings.Code;
                buildings.BuildingNo = buildings.BuildingNo;

                _context.Update(buildingsinfo );
                _context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int DeleteBuilding(Guid id)
        {
            try
            {
                tblBuildings buildinginfo = getBuildingByGuid(id);

                buildinginfo.IsDeleted = true;
                _context.tblBuildings.Update(buildinginfo);
                _context.SaveChanges();

                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
