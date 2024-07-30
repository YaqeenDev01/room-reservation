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
        public int AddUser(tblUsers user)
        {
            user.PhoneNumber = "567890768";
            user.FullNameEN = "MMMMM";
            user.Password = "4356789";
            user.IsDeleted = false;
            _context.tblUsers.Add(user);
            _context.SaveChanges();
            return 1;
        }

    }
}
