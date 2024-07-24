using room_reservation.Models;

namespace room_reservation.Domain
{
    public class UserDomain
    {
        private readonly KFUSpaceContext _context;
        
        public UserDomain(KFUSpaceContext context)
        {
            _context = context;
        }
        //return list of data 
        public  IEnumerable<tblUsers> getAllUsers()
        {
            return _context.tblUsers;
        }
    }
}
