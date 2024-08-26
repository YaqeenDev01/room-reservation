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

        public async Task<int> InsertBuilding(BuildingViewModel buildings)
        {
            try
            {   // التحقق من وجود رمز المبنى بالفعل
                var existingBuildingCode = await _context.tblBuildings.AsNoTracking().SingleOrDefaultAsync(b => b.Code == buildings.Code);

                if (existingBuildingCode != null)
                {
                    return 3; // رمز المبنى موجود بالفعل
                }
                // التحقق من وجود رقم المبنى بالفعل وعدم حذفه
                var existingBuildingNumber = await _context.tblBuildings.AsNoTracking().SingleOrDefaultAsync(b => b.BuildingNo == buildings.BuildingNo && b.IsDeleted == false);

                if (existingBuildingNumber != null)
                {
                    return 4; // رقم المبنى موجود بالفعل ولم يتم حذفه
                }

                // إذا لم يتم العثور على رمز المبنى أو رقم المبنى، يتم إنشاء المبنى الجديد
                var newBuilding = new tblBuildings
                {
                    BuildingNameEn = buildings.BuildingNameEn,
                    BuildingNameAr = buildings.BuildingNameAr,
                    Code = buildings.Code,
                    BuildingNo = buildings.BuildingNo,
                    Guid = Guid.NewGuid()
                };

                _context.tblBuildings.Add(newBuilding);
                await _context.SaveChangesAsync();

                return 1; // نجاح العملية
            }
            catch (Exception)
            {
                // يمكن توسيع التعامل مع الاستثناءات لاحقاً، إذا لزم الأمر
                return 0; // حدث خطأ
            }
        }

        public async Task <BuildingViewModel> getBuildingByguid(Guid Guid)
        {
            var buildingId =  await _context.tblBuildings.FirstOrDefaultAsync(x => x.Guid == Guid && x.IsDeleted == false);
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

      
        public async Task <tblBuildings> getBuildingByGuid(Guid id)

         {
            return await  _context.tblBuildings.FirstOrDefaultAsync(x => x.Guid == id);
        }

        public async Task<int> UpdatBuilding(BuildingViewModel buildings)
        {
            try
            {   // التحقق من وجود رمز المبنى بالفعل
                tblBuildings buildingsinfo = await getBuildingByGuid(buildings.Guid);

                var existingBuildingCode = await _context.tblBuildings.AsNoTracking().SingleOrDefaultAsync(b => b.Code == buildings.Code);

                if (existingBuildingCode != null && existingBuildingCode.Guid != buildings.Guid)
                {
                    return 3; // رمز المبنى موجود بالفعل
                }
                // التحقق من وجود رقم المبنى بالفعل وعدم حذفه
                var existingBuildingNumber = await _context.tblBuildings.AsNoTracking().SingleOrDefaultAsync(b => b.BuildingNo == buildings.BuildingNo && b.IsDeleted == false);

                if (existingBuildingNumber != null && existingBuildingNumber.Guid != buildings.Guid)
                {
                    return 4; // رقم المبنى موجود بالفعل ولم يتم حذفه
                }

                // إذا لم يتم العثور على رمز المبنى أو رقم المبنى، يتم إنشاء المبنى الجديد
                
                buildingsinfo.BuildingNameEn = buildings.BuildingNameEn;
                buildingsinfo.BuildingNameAr = buildings.BuildingNameAr;
                buildingsinfo.Code = buildings.Code;
                buildingsinfo.BuildingNo = buildings.BuildingNo;

                _context.tblBuildings.Update(buildingsinfo);
                await _context.SaveChangesAsync();

                return 1; // نجاح العملية
            }
            catch (Exception)
            {
                // يمكن توسيع التعامل مع الاستثناءات لاحقاً، إذا لزم الأمر
                return 0; // حدث خطأ
            }
        }

        //public async Task<int> UpdatBuilding(BuildingViewModel buildings)
        //{
        //    try
        //    {
        //        tblBuildings buildingsinfo = await getBuildingByGuid(buildings.Guid);

        //        tblBuildings buildingCode = _context.tblBuildings.AsNoTracking().SingleOrDefault(A => A.Code == buildings.Code);
        //        if (buildingCode != null && buildingCode.Guid != buildings.Guid)
        //            return 3;
        //        tblBuildings buildingnum = _context.tblBuildings.AsNoTracking().SingleOrDefault(A => A.BuildingNo == buildings.BuildingNo && A.IsDeleted == false);
        //        if (buildingnum != null && buildingnum.Guid != buildings.Guid)
        //            return 4;

        //        buildingsinfo.BuildingNameEn = buildings.BuildingNameEn;
        //        buildingsinfo.BuildingNameAr = buildings.BuildingNameAr;
        //        buildingsinfo.Code = buildings.Code;
        //        buildingsinfo.BuildingNo = buildings.BuildingNo;


        //        _context.Update(buildingsinfo);
        //        await _context.SaveChangesAsync();
        //        return 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        return 0;
        //    }
        //}
        public async Task DeleteBuilding(Guid id)
        {
           
                tblBuildings buildinginfo = await getBuildingByGuid(id);

                buildinginfo.IsDeleted = true;
                //_context.tblBuildings.Update(buildinginfo);
               await _context.SaveChangesAsync();

        }
    }
}
