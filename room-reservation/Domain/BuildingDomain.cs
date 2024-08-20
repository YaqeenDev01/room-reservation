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

        //public  async Task <BuildingViewModel > getBuildingByguid(Guid Guid)
        //{
        //    return await _context.tblBuildings.Where(x => x.Guid == Guid).Select (x => new BuildingViewModel
        //    {
        //        Guid = x.Guid,
        //        BuildingNameAr = x.BuildingNameAr,
        //        BuildingNo = x.BuildingNo,
        //        BuildingNameEn = x.BuildingNameEn,
        //        Code = x.Code,
        //    }).FirstOrDefaultAsync();

        //}
        public tblBuildings getBuildingByGuid(Guid id)

        {
            return  _context.tblBuildings.FirstOrDefault(x => x.Guid == id);
        }

        public async Task<bool> UpdatBuilding(BuildingViewModel buildings)
        {
            try
            { 
                tblBuildings buildingsinfo = getBuildingByGuid(buildings.Guid);

                buildingsinfo.BuildingNameEn = buildings.BuildingNameEn;
                buildingsinfo.BuildingNameAr = buildings.BuildingNameAr;
                buildingsinfo.Code = buildings.Code;
                buildings.BuildingNo = buildings.BuildingNo;

                _context.Update(buildingsinfo );
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task DeleteBuilding(Guid id)
        {
            //try
            //{
                tblBuildings buildinginfo = getBuildingByGuid(id);

                buildinginfo.IsDeleted = true;
                //_context.tblBuildings.Update(buildinginfo);
               await _context.SaveChangesAsync();

            //    return 1;
            //}
            //catch (Exception ex)
            //{
            //    return 0;
            //}
        }
    }
}
