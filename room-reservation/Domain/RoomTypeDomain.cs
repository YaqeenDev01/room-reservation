using Microsoft.EntityFrameworkCore;
using room_reservation.Models;

namespace room_reservation.Domain
{
    public class RoomTypeDomain
    {
        private readonly KFUSpaceContext _context;

        public RoomTypeDomain(KFUSpaceContext context)
        {
            _context = context;
        }



        public async Task<tblRoomType> GetRoomTypeByGuid(Guid guid)
        {
            return await _context.tblRoomType
                .FirstOrDefaultAsync(rt => rt.guid == guid && !rt.IsDeleted);
        }

    }
}