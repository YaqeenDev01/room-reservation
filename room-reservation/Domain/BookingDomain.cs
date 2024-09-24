 using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using room_reservation.Models;
using room_reservation.ViewModel;
using System.Security;
using System.Security.Claims;
using System.Linq;

namespace room_reservation.Domain
{
    public class BookingDomain
    {
        private readonly KFUSpaceContext _context;
        private readonly UserDomain _userDomain;
        private readonly RoomDomain _roomDomain;
        private readonly PermissionDomain _permissionDomain;
        private readonly BuildingDomain _buildingDomain;

        public BookingDomain(KFUSpaceContext context, UserDomain userDomain, RoomDomain roomDomain, PermissionDomain permissionDomain,BuildingDomain buildingDomain)
        {
            _context = context;
            _userDomain = userDomain;
            _roomDomain = roomDomain;
            _permissionDomain = permissionDomain;
            _buildingDomain = buildingDomain;
        }
        [Authorize]
        public async Task<IEnumerable<BookingViewModel>> GetAllBooking()
        {
            return await _context.tblBookings.Select(x => new BookingViewModel
            {
                BookingId = x.Id,
                BookingDate = x.BookingDate,
                BookingStart = x.BookingStart,
                BookingEnd = x.BookingEnd,
                guid = x.guid,
                RoomId = x.RoomId,
                RoomNo = x.Room.RoomNo,
                BookingStatues = x.BookingStatues,
                PhoneNumber = x.PhoneNumber,
                FullName = x.FullName,
                Email = x.Email,
                Duration = x.Duration,
                RejectReason = x.RejectReason,
                FloorNo = x.Room.Floor.FloorNo,
                BuildingNameAr = x.Room.Floor.Building.BuildingNameAr,
                SeatCapacity = x.Room.SeatCapacity,
                UserBuildingAR = x.UserBuildingAR,
                IsDeleted =x.IsDeleted,


               

            }).ToListAsync();
        }

        public async Task<IEnumerable<tblBookings>> getAllBooking()
        {
            return await _context.tblBookings.ToListAsync();
        }
        public async Task<int> AddBooking(BookingViewModel Booking)
        {
            try
            {
                // to get user info through claims  
                var user = await _userDomain.GetUserByEmail(Booking.Email);
                var room = await _roomDomain.GetRoomByGuid(Booking.RoomGuid);
                var building = await _buildingDomain.getBuildingByguid(Booking.BuildingGuid);

                tblBookings Bookings = new tblBookings();
                Bookings.Id = Booking.BookingId;
                Bookings.BookingDate =Booking.BookingDate; 
                Bookings.BookingStart = Booking.BookingStart;
                Bookings.BookingEnd = Booking.BookingEnd;
                Bookings.BookingStatues = Booking.BookingStatues;

                //Bookings.RejectReason = booking.RejectReason;
                // to calculate the duration first substract the end time from the start then convert  it to decimal
                var BookingDuration = Booking.BookingEnd - Booking.BookingStart;
                Bookings.Duration = Convert.ToDecimal(BookingDuration.TotalHours);
                Bookings.Email = user.Email;
                Bookings.FullName = user.FullNameAR;
                Bookings.PhoneNumber = user.PhoneNumber;
                Bookings.UserBuildingAR = user.DepartmentName;
                Bookings.guid = Guid.NewGuid();
                Bookings.IsDeleted = false;
                Bookings.RoomId = room.Id;
                //if (user.CollegeCode == Booking.BuildingNameAr) this solution works but it is better to use numbers
                
                // if the user is from the same uni or  building or from the  department of Information Techonolgy they can book directly 
                if((user.CollegeCode == building.Code)||(user.DepartmentCode==building.Code)||(user.DepartmentCode=="01"))
                {
                    Bookings.BookingStatuesId = 1;
                }
                else // else their booking statues is processing / waiting  
                {
                    Bookings.BookingStatuesId = 2;
                }
              _context.tblBookings.Add(Bookings);
              await _context.SaveChangesAsync();
               var bookingLog = new BookingsLog();
               bookingLog.BookingId = Bookings.Id;
               bookingLog.BookedBy = user.Email;
               bookingLog.GrantedBy =user.Email;
               bookingLog.BookingStatus = Bookings.BookingStatuesId;
               bookingLog.OperationDate=DateTime.Now;
               bookingLog.AdditionalDetails = "حجز قاعة";
              _context.BookingsLog.Add(bookingLog);
              await _context.SaveChangesAsync();
               
               
                return 1;
            }
            catch (Exception ex)
            {
                // تسجيل الخطأ أو معالجته هنا
                return 0;
            }
        }
        public async Task<BookingViewModel> getAllBookingByRoomGuid(Guid id)
        {
            var room = await _context.tblRooms.Where(room=>!room.IsDeleted && !room.IsActive).Include(x => x.RoomType).Include(x => x.Floor).ThenInclude(x => x.Building).FirstOrDefaultAsync(x => x.guid == id);
        //    var booking = await _context.tblBookings.Include(b=>b).FirstOrDefaultAsync(x => x.guid == id);
            return new BookingViewModel
            {
              //  BookingId = booking.Id,
              
                FloorNo = room.Floor.FloorNo,
                BuildingNameAr = room.Floor.Building.BuildingNameAr,
                SeatCapacity = room.SeatCapacity,
                RoomNo = room.RoomNo,
                RoomGuid = room.guid,
                RoomAR = room.RoomType.RoomTypeAR,
                BookingDate = DateTime.Now,
                BuildingGuid = room.Floor.Building.Guid
            };
        }


        public tblBookings getBookingByGuid(Guid id)

        {
            var bookingId = _context.tblBookings.FirstOrDefault(x => x.guid == id);
            return bookingId;
        }
        public async Task<tblBookings> getBookingByguid(Guid id)

        {
            return await _context.tblBookings.FirstOrDefaultAsync(x => x.guid == id);
        }


        public BookingViewModel getBookingByid(Guid id)
        {
            var BookingId = _context.tblBookings.Include(bs => bs.BookingStatues).Include(r => r.Room).Include(b => b.Room.Floor.Building).Include(f => f.Room.Floor).Include(rt => rt.Room.RoomType).FirstOrDefault(x => x.guid == id);
            BookingViewModel models = new BookingViewModel
            {

                BookingId = BookingId.Id,
                BookingDate = BookingId.BookingDate,
                BookingStart = BookingId.BookingStart,
                BookingEnd = BookingId.BookingEnd,

                UserBuildingAR = BookingId.UserBuildingAR,
                BookingStatuesId = BookingId.BookingStatues.Id,
                BookingStatusAR = BookingId.BookingStatues.StatuesAR,
                RoomTypeId = BookingId.Room.RoomTypeId,
                RoomAR = BookingId.Room.RoomType.RoomTypeAR,
                BuildingNameAr = BookingId.Room.Floor.Building.BuildingNameAr,
                FloorNo = BookingId.Room.Floor.FloorNo,
                SeatCapacity = BookingId.Room.SeatCapacity,
                RoomNo = BookingId.Room.RoomNo,
                IsDeleted = BookingId.IsDeleted,
                RoomId = BookingId.RoomId,
                guid = Guid.NewGuid(),
            };
            return models;
        }



        public async Task<tblBookings> CancelBooking(Guid guid,String userEmail)
        {

            var Bookings = _context.tblBookings.FirstOrDefault(x => x.guid == guid);
            Bookings.BookingStatuesId = 4;
            await _context.SaveChangesAsync();
            var bookingLog = new BookingsLog();
            bookingLog.BookingId = Bookings.Id;
            bookingLog.BookedBy = userEmail;
            bookingLog.GrantedBy =userEmail;
            bookingLog.BookingStatus = Bookings.BookingStatuesId;
            bookingLog.OperationDate=DateTime.Now;
            bookingLog.AdditionalDetails = "تم إلغاء الحجز";
            _context.BookingsLog.Add(bookingLog);
            await _context.SaveChangesAsync();
            return Bookings;
        }
        

        public BookingViewModel DetailsBooking(BookingViewModel booking)
        {
            try
            {
                tblBookings bookings =  getBookingByGuid(booking.guid);

                if (bookings == null)
                {
                    return null;
                }

                BookingViewModel bookingViewModel = new BookingViewModel
                {
                    BookingId = bookings.Id,
                    BookingDate = bookings.BookingDate,
                    BookingStart = bookings.BookingStart,
                    BookingEnd = bookings.BookingEnd,
                    BookingStatues = booking.BookingStatues,
                    guid = booking.guid,
                    RoomAR = bookings.Room.RoomType.RoomTypeAR,
                    BuildingNameAr = bookings.Room.Floor.Building.BuildingNameAr,
                    FloorNo = bookings.Room.Floor.FloorNo,
                    SeatCapacity = bookings.Room.SeatCapacity,
                    RoomNo = bookings.Room.RoomNo,

                };

                return bookingViewModel;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving the booking details.", ex);
            }

        }

        public async Task<bool> ApproveBooking(Guid id,string userEmail)
        {
            var booking = await _context.tblBookings.FirstOrDefaultAsync(b => b.guid == id);

            if (booking == null)
            {
                return false;
            }

            booking.BookingStatuesId = 1;
            _context.tblBookings.Update(booking);
            await _context.SaveChangesAsync();
       
            var bookingLog = new BookingsLog();
            bookingLog.BookingId = booking.Id;
            bookingLog.BookedBy = booking.Email;
            bookingLog.GrantedBy =userEmail;
            bookingLog.BookingStatus = booking.BookingStatuesId;
            bookingLog.OperationDate = booking.BookingDate;
            bookingLog.AdditionalDetails = "قبول الطلب";
            _context.BookingsLog.Add(bookingLog);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RejectBooking(Guid id, string rejectReason,string userEmail)
        {
            var booking = await _context.tblBookings.FirstOrDefaultAsync(b => b.guid == id);

            if (booking == null)
            {
                return false;
            }

            booking.BookingStatuesId = 3;

            booking.RejectReason = rejectReason;

            _context.tblBookings.Update(booking);

            await _context.SaveChangesAsync();

            var bookingLog = new BookingsLog();
            bookingLog.BookingId = booking.Id;
            bookingLog.BookedBy = booking.Email;
            bookingLog.GrantedBy = userEmail;
            bookingLog.BookingStatus = booking.BookingStatuesId;
            bookingLog.OperationDate = booking.BookingDate;
            bookingLog.AdditionalDetails = "رفض الطلب";
            _context.BookingsLog.Add(bookingLog);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> IsBookingExist(string buildingNameAr, int roomNo, DateTime bookingDate, TimeSpan bookingStart, TimeSpan bookingEnd)
        {
            return await _context.tblBookings
                .Include(b => b.Room)                      // Include Room
                    .ThenInclude(r => r.Floor)             // Include Floor from Room
                        .ThenInclude(f => f.Building)      // Include Building from Floor
                .AnyAsync(b => b.Room.Floor.Building.BuildingNameAr == buildingNameAr
                              && b.Room.RoomNo == roomNo
                              && b.BookingDate == bookingDate
                              && b.BookingStart == bookingStart
                              && b.BookingEnd == bookingEnd);
        }

        public async Task<bool> IsBookingOverlapping(string buildingNameAr, int roomNo, DateTime bookingDate, TimeSpan bookingStart, TimeSpan bookingEnd)
        {
            return await _context.tblBookings
                .Include(b => b.Room)                     // Include Room
                    .ThenInclude(r => r.Floor)            // Include Floor from Room
                        .ThenInclude(f => f.Building)     // Include Building from Floor
                .AnyAsync(b => b.Room.Floor.Building.BuildingNameAr == buildingNameAr
                              && b.Room.RoomNo == roomNo
                              && b.BookingDate == bookingDate
                              && (b.BookingStart < bookingEnd && b.BookingEnd > bookingStart));
        }

        public async Task<bool> IsLectureTimeAvailable(string buildingNameAr, int roomNo, DateTime bookingDate, TimeSpan bookingStart, TimeSpan bookingEnd)
        {
            return !await _context.tblLectures.AnyAsync(l =>
                l.BuildingNameAR == buildingNameAr &&
                l.RoomNo == roomNo &&
                l.LectureDate == bookingDate &&
                (l.StartLectureTime < bookingEnd && l.EndLectureTime > bookingStart));
        }



        //        // جلب المستخدم بناءً على البريد الإلكتروني
        //        var user = await _userDomain.GetUserByEmail(Booking.Email);
        //        if (user == null)
        //        {
        //            throw new Exception("User not found");
        //        }

        //        // جلب الغرفة بناءً على RoomGuid
        //        var room = await _roomDomain.GetRoomByGuid(Booking.RoomGuid);
        //        if (room == null)
        //        {
        //            throw new Exception("Room not found");
        //        }

        //        // إعداد ViewModel بناءً على البيانات المستردة
        //        
        //            RoomId = room.Id,
        //            BookingStatuesId = Booking.BookingStatuesId,
        //            RoomAR = room.RoomAR,
        //            BuildingNameAr = room.BuildingNameAr,
        //            FloorNo = room.FloorNo,
        //            RoomNo = room.RoomNo,
        //            SeatCapacity = room.SeatCapacity,
        //        };

        //        return 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        // تسجيل الخطأ أو معالجته هنا
        //        return 0;
        //    }
        //}

        public async Task<IEnumerable<BookingViewModel>> GetUserBookings(string userEmail)
        {
            var user = await _userDomain.GetUserByEmail(userEmail);

            return await _context.tblBookings.Where(booking => booking.Email ==user.Email ).Select(x => new BookingViewModel
            {
                BookingId = x.Id,
                BookingDate = x.BookingDate,
                BookingStart = x.BookingStart,
                BookingEnd = x.BookingEnd,
                guid = x.guid,
                RoomId = x.RoomId,
                RoomNo = x.Room.RoomNo,
                BookingStatues = x.BookingStatues,
                PhoneNumber = x.PhoneNumber,
                FullName = x.FullName,
                Email = x.Email,
                Duration = x.Duration,
                RejectReason = x.RejectReason,
                IsDeleted =x.IsDeleted,
            }).ToListAsync();
        }
   
   
   
        public async Task<IEnumerable<BookingViewModel>> GetBuildingBookings(string userEmail)
        {
            var permission = await _permissionDomain.GetPermissionByEmail(userEmail);
         

            return await _context.tblBookings.Where(booking => booking.Room.Floor.BuildingId==permission.BuildingId ).Select(x => new BookingViewModel
            {
                BookingId = x.Id,
                BookingDate = x.BookingDate,
                BookingStart = x.BookingStart,
                BookingEnd = x.BookingEnd,
                guid = x.guid,
                RoomId = x.RoomId,
                RoomNo = x.Room.RoomNo,
                BookingStatues = x.BookingStatues,
                PhoneNumber = x.PhoneNumber,
                FullName = x.FullName,
                Email = x.Email,
                Duration = x.Duration,
                RejectReason = x.RejectReason,
                UserBuildingAR = x.UserBuildingAR,
                IsDeleted =x.IsDeleted,
            }).ToListAsync();
        }
        public async Task<IEnumerable<BookingViewModel>> GetExtrnalBooking(string userEmail)
        {
            var permission = await _permissionDomain.GetPermissionByEmail(userEmail);

            return await _context.tblBookings.Where(booking => !booking.IsDeleted && booking.Room.Floor.BuildingId == permission.BuildingId && booking.BookingStatuesId == 2).Select(x => new BookingViewModel
            {
                BookingId = x.Id,
                BookingDate = x.BookingDate,
                BookingStart = x.BookingStart,
                BookingEnd = x.BookingEnd,
                guid = x.guid,
                RoomId = x.RoomId,
                RoomNo = x.Room.RoomNo,
                BookingStatues = x.BookingStatues,
                PhoneNumber = x.PhoneNumber,
                FullName = x.FullName,
                Email = x.Email,
                Duration = x.Duration,
                RejectReason = x.RejectReason,
            }).ToListAsync();
        }
        public async Task<BookingViewModel> GetBookingByGuid(Guid id)
        {
          
            var booking = await _context.tblBookings.Include(bs => bs.BookingStatues).Include(r => r.Room).Include(rt => rt.Room.RoomType).FirstOrDefaultAsync(b => b.guid == id && b.IsDeleted == false);
            BookingViewModel model = new BookingViewModel
            {
                FullName = booking.FullName,
                Email = booking.Email,
                PhoneNumber = booking.PhoneNumber,
                guid = booking.guid,
                BookingId = booking.Id,
                BookingDate = booking.BookingDate,
                BookingStart = booking.BookingStart,
                BookingEnd = booking.BookingEnd,
                
                BookingStatuesId = booking.BookingStatues.Id,
                BookingStatusAR = booking.BookingStatues.StatuesAR,
                RoomTypeId = booking.Room.RoomTypeId,
                RoomAR = booking.Room.RoomType.RoomTypeAR,
           
                SeatCapacity = booking.Room.SeatCapacity,
                RoomNo = booking.Room.RoomNo,
                
                RoomId = booking.RoomId,
            };
            return model;
        }
        public async Task<tblBookings> getBookingOrderByGuid(Guid id)

        {
            return await _context.tblBookings.FirstOrDefaultAsync(x => x.guid == id);
        }
        public async Task<int> Orderinfo(BookingViewModel booking)
        {
            try
            {
                tblBookings bookinfo = await getBookingOrderByGuid(booking.guid);
                BookingViewModel Bookings = new BookingViewModel
                {
                    BookingId = bookinfo.Id,
                    BookingDate = bookinfo.BookingDate,
                    BookingStart = bookinfo.BookingStart,
                    BookingEnd = bookinfo.BookingEnd,
                    BookingStatues = bookinfo.BookingStatues,
                    Email = bookinfo.Email,
                    FullName = bookinfo.FullName,
                    PhoneNumber = bookinfo.PhoneNumber,
                    guid = bookinfo.guid,
                    IsDeleted = false,
                    BookingStatuesId = bookinfo.BookingStatuesId,
                    RoomAR = bookinfo.Room.RoomType.RoomTypeAR,
                    RoomNo = bookinfo.Room.RoomNo,
                    SeatCapacity = bookinfo.Room.SeatCapacity,
                    RoomId=bookinfo.RoomId,

                };
                return 1;
            }
            catch (Exception ex)
            {
                // تسجيل الخطأ أو معالجته هنا
                return 0;
            }

        }
        public async Task<IEnumerable<BookingViewModel>> GetExportableBookings()
        {
            return await _context.tblBookings
                .Where(b => !b.IsDeleted).Include(x=>x.BookingStatues).Include(x=>x.Room).ThenInclude(x=>x.Floor).ThenInclude(x=>x.Building)
                .Select(b => new BookingViewModel
                {
                    FullName =b.FullName,
                    Email = b.Email ,
                    PhoneNumber = b.PhoneNumber,
                    BuildingNameAr = b.Room.Floor.Building.BuildingNameAr,
                    FloorNo = b.Room.Floor.FloorNo,
                    RoomNo = b.Room.RoomNo,
                    RoomAR = b.Room.RoomType.RoomTypeAR,
                    BookingDate = b.BookingDate,
                    BookingStart = b.BookingStart,
                    BookingEnd = b.BookingEnd,
                    Duration = b.Duration,
                    RejectReason = b.RejectReason,
                    BookingStatusAR = b.BookingStatues.StatuesAR,
                    UserBuildingAR = b.UserBuildingAR,
                 
                })
                .ToListAsync();
        }   
    }
}






