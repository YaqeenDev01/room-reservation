using Microsoft.AspNetCore.Mvc;
using room_reservation.Models;

namespace room_reservation.Domain
{
    public class BookingStatusDomain
    {
        private readonly KFUSpaceContext _context;

        public BookingStatusDomain(KFUSpaceContext context)
        {
            _context = context;
        }

            public tblBookingStatues getBookingStatuesByGuid(int id)

            {
                return _context.tblBookingStatues.FirstOrDefault(x => x.Id == id);
            }
        
    } 
}
