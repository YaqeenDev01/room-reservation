using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using room_reservation.Domain;
using room_reservation.Models;
using room_reservation.ViewModel;

//Each table has its own domain except for those that don't have pages. For examples, users or roles that will be added manually.
namespace room_reservation.Domain
{
    public class PermissionDomain
    {
        //lines 12 - 16 must be in every domain to connect to db.
        private readonly KFUSpaceContext _context; //context is a standard word for MS. There is no problem in changing it. 
        public PermissionDomain(KFUSpaceContext context)
        {
           _context = context;
        }

        
        public async Task<IEnumerable<PermissionViewModel>> getAllPermissions()
        {
            return await _context.tblPermissions.Include(x => x.Role).Include(y => y.Building).Select(z => new PermissionViewModel
            {
                Id = z.Id,
                Email = z.Email,
                RoleId = z.RoleId,
                RoleName = z.Role.RoleNameAR,
                BuildingNum = z.Building.BuildingNo,
                BuildingName = z.Building.BuildingNameAr
            }).ToListAsync();


            
        }

        public async Task<PermissionViewModel> GetPermissionByGuid(Guid guid)
        {
            return _context.tblPermissions.Where(x => x.guid == guid).Select(
                x => new PermissionViewModel
                {
                    guid = x.guid,
                    Email = x.Email,
                    RoleId = x.RoleId,
                    BuildingId = x.BuildingId,
                    

                }
                ).FirstOrDefault();    
                
        }

        public async Task<string> AddPermission(PermissionViewModel permission)
        {
            
            try
            {
                var permissionInfo = new tblPermissions
                {
                    Email = permission.Email,
                    RoleId = permission.RoleId,
                    BuildingId = permission.BuildingId,
                    guid = new Guid(),
                    IsDeleted = false
                    
                   
                };
                
                _context.Add(permissionInfo);
                await _context.SaveChangesAsync();
                return"added successfully";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return ex.Message;
            }
           
        }


    }
}

