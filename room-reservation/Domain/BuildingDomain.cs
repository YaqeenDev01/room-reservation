using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using room_reservation.Models;
 using room_reservation.ViewModel;
using System.Linq.Expressions;
using System.Security.Claims;

namespace room_reservation.Domain
{
    public class BuildingDomain
    {
        private readonly KFUSpaceContext _context;
        private readonly UserDomain _userDomain;
        public BuildingDomain(KFUSpaceContext context, UserDomain userDomain)
        {
            _context = context;
            _userDomain = userDomain;
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
                BuildingId = x.Id,
                GenderId = x.Id,
                GenderAR = x.GenderAR,
                //GenderEN=x.Gender.GenderEN,

            }).ToListAsync();
        }

        public async Task<int> InsertBuilding(BuildingViewModel buildings)
        {
            var user = await _userDomain.GetUserByEmail(buildings.Email);

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
                    GenderAR= buildings.GenderAR,
                    Guid = Guid.NewGuid()
                };

                _context.tblBuildings.Add(newBuilding);
                await _context.SaveChangesAsync();

                var buildingLog = new BuildingsLog();
                buildingLog.BuildingId = newBuilding.Id;
                buildingLog.OperationType = " اضافة مبنى ";
                buildingLog.OperationDate = DateTime.Now;
                buildingLog.GrantdBy = user.Email;
                buildingLog.AdditionalDetails = " ";
                _context.BuildingsLog.Add(buildingLog);
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
                GenderAR = buildingId.GenderAR,
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
            var user = await _userDomain.GetUserByEmail(buildings.Email);

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

                
                buildingsinfo.BuildingNameEn = buildings.BuildingNameEn;
                buildingsinfo.BuildingNameAr = buildings.BuildingNameAr;
                buildingsinfo.Code = buildings.Code;
                buildingsinfo.BuildingNo = buildings.BuildingNo;
                buildingsinfo.GenderAR= buildings.GenderAR;
                _context.tblBuildings.Update(buildingsinfo);
                await _context.SaveChangesAsync();

                var buildingLog = new BuildingsLog();
                buildingLog.BuildingId = buildings.BuildingId;
                buildingLog.OperationType = " تعديل مبنى ";
                buildingLog.OperationDate = DateTime.Now;
                buildingLog.GrantdBy = user.Email;
                buildingLog.AdditionalDetails = " ";
                _context.BuildingsLog.Add(buildingLog);
                await _context.SaveChangesAsync();

                return 1; // نجاح العملية
            }
            catch (Exception)
            {
                // يمكن توسيع التعامل مع الاستثناءات لاحقاً، إذا لزم الأمر
                return 0; // حدث خطأ
            }
        }

   
        public async Task DeleteBuilding(Guid id)  
        {

            tblBuildings buildinginfo = await getBuildingByGuid(id);

                buildinginfo.IsDeleted = true;
                //_context.tblBuildings.Update(buildinginfo);
               await _context.SaveChangesAsync();

            var buildingLog = new BuildingsLog();
            buildingLog.BuildingId = buildinginfo.Id;
            buildingLog.OperationType = "حذف مبنى ";
            buildingLog.OperationDate = buildingLog.OperationDate;
            buildingLog.GrantdBy = ClaimTypes.Email;
            buildingLog.AdditionalDetails = " ";
            _context.BuildingsLog.Add(buildingLog);
            await _context.SaveChangesAsync();




        }
        
       
        public async Task<tblBuildings> GetBuildingById(int buildingId)
        {
            return await _context.tblBuildings.FindAsync(buildingId);

        }

 
    }
    }

