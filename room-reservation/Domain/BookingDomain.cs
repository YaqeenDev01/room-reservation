using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using room_reservation.Models;
using room_reservation.ViewModel;

namespace room_reservation.Domain {
    public class BookingDomain
    {
        private readonly KFUSpaceContext _context;
        private readonly UserDomain _userDomain;
        private readonly RoomDomain _roomDomain;
        public BookingDomain(KFUSpaceContext context,UserDomain userDomain,RoomDomain roomDomain)
        {
            _context = context;
            _userDomain = userDomain;
            _roomDomain = roomDomain;
        }
        [Authorize]
        public async Task<IEnumerable<BookingViewModel>> GetAllBooking()
        {
            return await _context.tblBookings.Where(booking => !booking.IsDeleted).Select(x=> new BookingViewModel
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
                
                
                // FloorNo = x.Rooms.Floor.FloorNo,
                // BuildingNameAr = x.Rooms.Floor.Building.BuildingNameAr,
                // SeatCapacity=x.Rooms.SeatCapacity,
                
                
                //Bookings.BookingStatues = booking.BookingStatues
                //Bookings.RejectReason = booking.RejectReason;
                //Bookings.Duration = booking.Duration;
                //Bookings.Email = booking.Email;
                //Bookings.FullName = booking.FullName;
                //Bookings.PhoneNumber = booking.PhoneNumber;


            }).ToListAsync();
        }
        
        // public async Task<IEnumerable<LecturesViewModel>> getAlllectures()
        // {
        //
        //     return await _context.tblLectures.Select(x => new LecturesViewModel
        //     {
        //         Id = x.Id,
        //         BuildingNo = x.BuildingNo,
        //         RoomNo = x.RoomNo,
        //         LectureDate = x.LectureDate,
        //         StartLectureTime = x.StartLectureTime,
        //         EndLectureTime = x.EndLectureTime,
        //         //LectureDurations = x.LectureDurations,
        //         Semester = x.Semester
        //
        //
        //     }).ToListAsync();
        //
        //
        // }
        public async Task<IEnumerable<tblBookings>> getAllBooking()
        {
            return await _context.tblBookings.ToListAsync();
        }
        public  async Task<int> AddBooking(BookingViewModel Booking)
        {
            try
            {
                
                var user= await _userDomain.GetUserByEmail(Booking.Email);
                var room = await _roomDomain.GetRoomByGuid(Booking.RoomGuid);
                
                tblBookings Bookings = new tblBookings();
                Bookings.Id = Booking.BookingId;
                Bookings.BookingDate = DateTime.Now;
                Bookings.BookingStart = Booking.BookingStart;
                Bookings.BookingEnd = Booking.BookingEnd;
                Bookings.BookingStatues = Booking.BookingStatues;
         
                //Bookings.RejectReason = booking.RejectReason;
               // Bookings.Duration = Booking.BookingEnd - Booking.BookingStart;
               //since duration is int this wont work 
               /**/
               
               Bookings.Duration = Booking.Duration;
                Bookings.Email = user.Email;
                Bookings.FullName = user.FullNameAR;
                Bookings.PhoneNumber = user.PhoneNumber;
               
                Bookings.guid = Guid.NewGuid();
                Bookings.IsDeleted = false;
                Bookings.RoomId = room.Id;
                Bookings.BookingStatuesId = 2;
                _context.tblBookings.Add(Bookings);
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
            var room =  await _context.tblRooms.Include(x=>x.RoomType).Include(x=> x.Floor).ThenInclude(x=>x.Building).FirstOrDefaultAsync(x=>x.guid==id);

            return new BookingViewModel
            {
                FloorNo = room.Floor.FloorNo,
                BuildingNameAr = room.Floor.Building.BuildingNameAr,
                SeatCapacity = room.SeatCapacity,
                RoomNo = room.RoomNo,
                RoomGuid = room.guid,
                RoomAR = room.RoomType.RoomAR,
                BookingDate = DateTime.Now
            };
        }
      

        public tblBookings getBookingByGuid(Guid id)

        {
            return _context.tblBookings.FirstOrDefault(x => x.guid == id);
        }


        public BookingViewModel getBookingByid(Guid id)
        {
            var BookingId = _context.tblBookings.FirstOrDefault(x => x.guid == id);
            BookingViewModel models = new BookingViewModel
            {
             
                BookingId  = BookingId.Id,
                BookingDate = BookingId.BookingDate,
                BookingStart = BookingId.BookingStart,
                BookingEnd = BookingId.BookingEnd,
                //Bookings.BookingStatues = booking.BookingStatues;
                //Bookings.RejectReason = booking.RejectReason;
                //Bookings.Duration = booking.Duration;
                //Bookings.Email = booking.Email;
                //Bookings.FullName = booking.FullName;
                //Bookings.PhoneNumber = booking.PhoneNumber;
                IsDeleted = BookingId.IsDeleted,
                RoomId = BookingId.RoomId,
                guid = Guid.NewGuid(),
             };
            return models;
        } 

        public string DeleteBooking(BookingViewModel booking)
        {
            try
            {
                tblBookings Bookings = getBookingByGuid(booking.guid);
                Bookings.Id = booking.BookingId;
                Bookings.BookingDate = booking.BookingDate;
                Bookings.BookingStart = booking.BookingStart;
                Bookings.BookingEnd = booking.BookingEnd;
                //Bookings.BookingStatues = booking.BookingStatues;
                //Bookings.RejectReason = booking.RejectReason;
                //Bookings.Duration = booking.Duration;
                //Bookings.Email = booking.Email;
                //Bookings.FullName = booking.FullName;
                //Bookings.PhoneNumber = booking.PhoneNumber;
                Bookings.guid = Guid.NewGuid();
                Bookings.IsDeleted = false;
                Bookings.RoomId = booking.RoomId;

                _context.tblBookings.Remove(Bookings);
                _context.SaveChanges();
                return "successful";
            }
            catch (Exception ex)
            {
                // تسجيل الخطأ أو معالجته هنا
                return $"Error: {ex.Message}";
            }
        }
        //public string DetailsBooking(BookingViewModel booking)
        //{
        //    try
        //    {
        //        tblBookings Bookings = getBookingByGuid(booking.guid);
        //        Bookings.Id = booking.Id;
        //        Bookings.BookingDate = booking.BookingDate;
        //        Bookings.BookingStart = booking.BookingStart;
        //        Bookings.BookingEnd = booking.BookingEnd;
        //        //Bookings.BookingStatues = booking.BookingStatues;
        //        //Bookings.RejectReason = booking.RejectReason;
        //        //Bookings.Duration = booking.Duration;
        //        //Bookings.Email = booking.Email;
        //        //Bookings.FullName = booking.FullName;
        //        //Bookings.PhoneNumber = booking.PhoneNumber;


        //        return "successful";
        //    }
        //    catch (Exception ex)
        //    {
        //        // تسجيل الخطأ أو معالجته هنا
        //        return $"Error: {ex.Message}";
        //    }
        //}

        public BookingViewModel DetailsBooking(BookingViewModel booking)
        {
            try
            {
                // Retrieve the booking record from the database using the GUID
                tblBookings bookings = getBookingByGuid(booking.guid);

                if (bookings == null)
                {
                    // Return null or handle the case where the booking is not found
                    return null;
                }

                // Map the tblBookings entity to BookingViewModel
                BookingViewModel bookingViewModel = new BookingViewModel
                {
                    BookingId= bookings.Id,
                    BookingDate = bookings.BookingDate,
                    BookingStart = bookings.BookingStart,
                    BookingEnd = bookings.BookingEnd,
                    //BookingStatues = booking.BookingStatues,
                    //RejectReason = booking.RejectReason,
                    //Duration = booking.Duration,
                    //Email = booking.Email,
                    //FullName = booking.FullName,
                    //PhoneNumber = booking.PhoneNumber
                };

                // Return the booking details as a BookingViewModel
                return bookingViewModel;
            }
            catch (Exception ex)
            {
                // Log or handle the error as needed
                // For simplicity, just rethrow or return null
                throw new ApplicationException("An error occurred while retrieving the booking details.", ex);
            }
        }

 
    }
}






