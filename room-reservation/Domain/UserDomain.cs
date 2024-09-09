using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using room_reservation.Models;
using room_reservation.ViewModel;
using System.Security;
using System.Security.Cryptography.X509Certificates;

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
        public async Task <IEnumerable<UserViewModel>> GetAllUsers()
        {
            return await _context.tblUsers.Where(user=>!user.IsDeleted).Select(u=>new UserViewModel
                {
                    UserType = u.UserType,
                    FullNameEN = u.FullNameEN,
                    FullNameAR = u.FullNameAR,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber
                }).ToListAsync();
        }
 
        public List<tblUsers> getUsers()
        {
            return _context.tblUsers.ToList();
        }
        
        public async Task<int> AddUser(UserViewModel user)
        {
            try
            {
                var userInfo = new tblUsers
                {
                    Email = user.Email,
                    FullNameEN = user.FullNameEN,
                    FullNameAR = user.FullNameAR,
                    Password = user.Password,
                    PhoneNumber = user.PhoneNumber,
                    UserType = user.UserType,
                    IsDeleted = false


                };

                _context.Add(userInfo);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return 0;
            }


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

            user.IsDeleted = true;
            _context.SaveChanges();
            return 1;


        }

       
        public async Task<UserViewModel> GetAccountsForLogin(UserViewModel userInfo)
        {
            var userData = await _context.tblUsers.FirstOrDefaultAsync(
                u => u.Email == userInfo.Email && u.Password == userInfo.Password && u.IsDeleted == false);
            
                
            return new UserViewModel
            {
                UserType = userData.UserType,
                Email = userData.Email,
                FullNameAR = userData.FullNameAR,
                PhoneNumber = userData.PhoneNumber,
                Id = userData.Id,
                FullNameEN = userData.FullNameEN
            };
        }

        public async Task<tblUsers> GetUserByEmail(string email)
        {
            return await _context.tblUsers.FirstOrDefaultAsync(u => u.Email == email);

        }

    }
}