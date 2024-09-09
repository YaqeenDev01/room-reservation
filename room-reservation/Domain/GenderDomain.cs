using Microsoft.EntityFrameworkCore;
using room_reservation.Models;
using room_reservation.ViewModel;

namespace room_reservation.Domain
{
    public class GenderDomain
    {
        private readonly KFUSpaceContext _context;
        public GenderDomain(KFUSpaceContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GenderViewModel>> GetAllGenders()
        {
            return await _context.tblGender.Select(x => new GenderViewModel
            {
                Id = x.Id,
                GenderAR = x.GenderAR,
                GenderEN = x.GenderEN

            }).ToListAsync();
        }

    }
}
