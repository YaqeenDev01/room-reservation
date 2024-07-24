using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using room_reservation.Domain;
using room_reservation.Models;

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
        public IEnumerable<tblPermissions> getAllPermissions()
        {
            return _context.tblPermissions;//=select * from tblPermissions;
        }
        /*public List<tblRoles> getAllPermissionRoles()
        {
            return _Context.tblRoles;
        }*/

    }
}

