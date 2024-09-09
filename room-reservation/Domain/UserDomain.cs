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
                    UserId = u.Id,
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
        public UserViewModel getUserById(int id) {
            //return _context.tblUsers.SingleOrDefault(x=> x.Id == id);
            try
            {
                var user = _context.tblUsers
                    .Where(u => u.Id == id)
                    .Select(u => new UserViewModel
                    { 
                        UserId = u.Id,
                        FullNameEN = u.FullNameEN,
                        FullNameAR = u.FullNameAR,
                        PhoneNumber = u.PhoneNumber,
                        Email = u.Email,
                        Password = u.Password,
                        UserType = u.UserType,
                    }).FirstOrDefault();
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving floor: {ex.Message}");
            }
        }

        public tblUsers GetUserByID(int id)
        {
            var userId = _context.tblUsers.FirstOrDefault(i => i.Id == id);
            return userId;
        }

        public async Task<int> EditUser(UserViewModel user)
        {
            try
            {
                var userInfo = GetUserByID(user.UserId);
                userInfo.FullNameAR = user.FullNameAR;
                userInfo.Email = user.Email;
                userInfo.Password = user.Password;
                userInfo.FullNameEN = user.FullNameEN;
                userInfo.UserType = user.UserType;
                userInfo.IsDeleted = false;
                _context.tblUsers.Update(userInfo);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch (Exception exception)
            {
                return 0;
            }
            


        }
        public async Task DeleteUser(int id)
        {
            var user = _context.tblUsers.Where(x => x.Id == id).SingleOrDefault();
            user.IsDeleted = true;
            await _context.SaveChangesAsync();


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
                UserId = userData.Id,
                FullNameEN = userData.FullNameEN
            };
        }

        public async Task<tblUsers> GetUserByEmail(string email)
        {
            return await _context.tblUsers.FirstOrDefaultAsync(u => u.Email == email);

        }

    }
}