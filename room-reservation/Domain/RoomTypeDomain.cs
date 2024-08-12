using Microsoft.EntityFrameworkCore;
using room_reservation.Models;
using room_reservation.ViewModel;

namespace room_reservation.Domain
{
    public class RoomTypeDomain
    {
        private readonly KFUSpaceContext _context;

        public RoomTypeDomain(KFUSpaceContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RoomTypeViewModel>> GetAllRoomTypes()
        {
            return await _context.tblRoomType
                .Where(rt => !rt.IsDeleted)
                .Select(rt => new RoomTypeViewModel
                {
                    guid = rt.guid,
                    RoomAR = rt.RoomAR,
                    IsDeleted = rt.IsDeleted,
                })
                .ToListAsync();
        }
    }
}