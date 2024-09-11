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
               // to get user info through claims  
                var user= await _userDomain.GetUserByEmail(Booking.Email);
                var room = await _roomDomain.GetRoomByGuid(Booking.RoomGuid);
         
                tblBookings Bookings = new tblBookings();
                Bookings.Id = Booking.BookingId;
                Bookings.BookingDate = DateTime.Now;
                Bookings.BookingStart = Booking.BookingStart;
                Bookings.BookingEnd = Booking.BookingEnd;
                Bookings.BookingStatues = Booking.BookingStatues;
         
                //Bookings.RejectReason = booking.RejectReason;
                // to calculate the duration first substract the end time from the start then convert it it to decimal
                var BookingDuration = Booking.BookingEnd - Booking.BookingStart;
                Bookings.Duration = Convert.ToDecimal(BookingDuration.TotalHours);
                Bookings.Email = user.Email;
                Bookings.FullName = user.FullNameAR;
                Bookings.PhoneNumber = user.PhoneNumber;
               
                Bookings.guid = Guid.NewGuid();
                Bookings.IsDeleted = false;
                Bookings.RoomId = room.Id;
                if (user.CollegeName == Booking.BuildingNameAr)
                {
                    Bookings.BookingStatuesId = 1;
                }
                else
                {
                    Bookings.BookingStatuesId = 2;
                }

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
                RoomAR = room.RoomType.RoomTypeAR,
                BookingDate = DateTime.Now
            };
        }
      

        public tblBookings getBookingByGuid(Guid id)

        {
          var bookingId= _context.tblBookings.FirstOrDefault(x => x.guid == id);
          return bookingId;
        }
        public async Task<tblBookings> getBookingByguid(Guid id)

        {
            return await _context.tblBookings.FirstOrDefaultAsync(x => x.guid == id);
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

        public async Task CancelBooking(Guid id)
        {

                tblBookings Bookings = getBookingByGuid(id);
                Bookings.BookingStatuesId = 4;
                await _context.SaveChangesAsync();
        
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

        public async Task<IEnumerable<BookingViewModel>> GetExternalBookings()
        {
            return await _context.tblBookings
                .Include(b => b.Room)   
                .ThenInclude(r => r.Floor) 
                .ThenInclude(f => f.Building) 
                .Join(_context.tblUsers, b => b.Email, u => u.Email, (b, u) => new { Booking = b, User = u }) 
                .Where(bu => bu.User.CollegeName != bu.Booking.Room.Floor.Building.BuildingNameAr) 
                .Select(bu => new BookingViewModel
                {
                    BookingId = bu.Booking.Id,
                    FullName = bu.Booking.FullName,
                    Email = bu.Booking.Email,
                    RoomNo = bu.Booking.Room.RoomNo,
                    BookingDate = bu.Booking.BookingDate,
                    guid = bu.Booking.guid,
                })
                .ToListAsync();
        }



        public async Task<bool> ApproveBooking(Guid bookingGuid)
        {
            var booking = await _context.tblBookings.FirstOrDefaultAsync(b => b.guid == bookingGuid);

            if (booking == null)
            {
                return false; 
            }

            booking.BookingStatuesId = 1; 
            _context.tblBookings.Update(booking);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RejectBooking(Guid bookingGuid,string rejectReason)
        {
            var booking = await _context.tblBookings.FirstOrDefaultAsync(b => b.guid == bookingGuid);

            if (booking == null)
            {
                return false; 
            }

            booking.BookingStatuesId = 3; 

            booking.RejectReason = rejectReason;

            _context.tblBookings.Update(booking);

            await _context.SaveChangesAsync();

            return true;
        }


    }
}






