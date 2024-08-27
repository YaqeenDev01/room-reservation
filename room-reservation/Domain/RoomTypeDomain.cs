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
                .Select(rt => new RoomTypeViewModel
                {
                    Id = rt.Id,
                    RoomAR = rt.RoomAR,
                    guid = rt.guid
                })
                .ToListAsync();
        }
    }
}