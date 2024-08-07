using room_reservation.Models;
using room_reservation.ViewModel;

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
        public IEnumerable<tblUsers> getAllUsers()
        {
            return _context.tblUsers;
        }
 
        public List<tblUsers> getUsers()
        {
            return _context.tblUsers.ToList();
        }
        public int InsertUser(UserViewModel user)
        {
            // user.Guid=Guid.NewGuid();
            tblUsers userInfo = new tblUsers();
            userInfo.FullNameEN = user.FullNameEN;
            userInfo.FullNameAR = user.FullNameAR;
            userInfo.Email = user.Email;
            userInfo.PhoneNumber = user.PhoneNumber;
            userInfo.Password = user.Password;
            userInfo.UserType = user.UserType;
           
            _context.tblUsers.Add(userInfo);
            _context.SaveChanges();
            return 1;
             

        }
        public tblUsers getUserById(int id) {
            return _context.tblUsers.SingleOrDefault(x=> x.Id == id);
        }


        public int EditUser(tblUsers user)
        {

            // user.Guid=Guid.NewGuid();
            user.IsDeleted = false;
            _context.tblUsers.Update(user);
            _context.SaveChanges();
            return 1;


        }
        public int DeleteUser(tblUsers user)
        {

            user.IsDeleted = false;
            _context.tblUsers.Remove(user);
            _context.SaveChanges();
            return 1;


        }

    }
}