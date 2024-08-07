using Microsoft.EntityFrameworkCore;
using room_reservation.Models;
using room_reservation.ViewModel;

namespace room_reservation.Domain
{
    public class RoleDomain
    {
        private readonly KFUSpaceContext _context;  
        public RoleDomain(KFUSpaceContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RoleViewModel>> GetAllRoles()
        {
            return await _context.tblRoles.Select(x => new RoleViewModel {
                Id = x.Id,
                RoleName = x.RoleNameAR,
            }).ToListAsync();
        }
    }
}
