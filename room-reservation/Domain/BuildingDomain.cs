using Microsoft.AspNetCore.Mvc;
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
                BuildingNameEn = x.BuildingNameEn,
                BuildingNameAr = x.BuildingNameAr,
                BuildingNo = x.BuildingNo,
                Code = x.Code,
                Guid = x.Guid,
                BuildingId = x.Id
  
            }).ToListAsync();
        }
       
        public async Task <int> InsertBuilding(BuildingViewModel buildings)
        {
            try
            {

                tblBuildings buildingCode= _context.tblBuildings.AsNoTracking().SingleOrDefault(A => A.Code == buildings.Code);
                if (buildingCode != null)
                    return 3; // This code is already there

                tblBuildings buildingnum = _context.tblBuildings.AsNoTracking().SingleOrDefault(A => A.BuildingNo == buildings.BuildingNo);
                if (buildingnum != null)
                    return 4;


                tblBuildings buildings1 = new tblBuildings();
                buildings1.BuildingNameEn = buildings.BuildingNameEn;
                buildings1.BuildingNameAr = buildings.BuildingNameAr;
                buildings1.Code = buildings.Code;
                buildings1.BuildingNo = buildings.BuildingNo;
                buildings1.Guid = Guid.NewGuid();

                _context.tblBuildings.Add(buildings1);
                 await _context.SaveChangesAsync();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public  BuildingViewModel getBuildingByguid(Guid Guid)
        {
            var buildingId = _context.tblBuildings.FirstOrDefault(x => x.Guid == Guid && x.IsDeleted == false);
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
            return  _context.tblBuildings.FirstOrDefault(x => x.Guid == id);
        }

        public async Task<int> UpdatBuilding(BuildingViewModel buildings)
        {
            try
            {
                tblBuildings buildingsinfo = getBuildingByGuid(buildings.Guid);

                tblBuildings buildingCode = _context.tblBuildings.AsNoTracking().SingleOrDefault(A => A.Code == buildings.Code);
                if (buildingCode != null && buildingCode.Guid != buildings.Guid )
                    return 3;
                tblBuildings buildingnum = _context.tblBuildings.AsNoTracking().SingleOrDefault(A => A.BuildingNo == buildings.BuildingNo);
                if (buildingnum != null && buildingnum.Guid != buildings.Guid)
                    return 4;


                buildingsinfo.BuildingNameEn = buildings.BuildingNameEn;
                buildingsinfo.BuildingNameAr = buildings.BuildingNameAr;
                buildingsinfo.Code = buildings.Code;
                buildingsinfo.BuildingNo = buildings.BuildingNo;


                _context.Update(buildingsinfo );
                await _context.SaveChangesAsync();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task DeleteBuilding(Guid id)
        {
           
                tblBuildings buildinginfo = getBuildingByGuid(id);

                buildinginfo.IsDeleted = true;
                //_context.tblBuildings.Update(buildinginfo);
               await _context.SaveChangesAsync();

        }
    }
}
