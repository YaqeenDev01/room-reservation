using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using room_reservation.Models;
using room_reservation.ViewModel;
using System.Security;
using System.Security.Claims;

namespace room_reservation.Domain {
    public class BookingDomain
    {
        private readonly KFUSpaceContext _context;
        private readonly UserDomain _userDomain;
        private readonly RoomDomain _roomDomain;
        private readonly PermissionDomain _permissionDomain;

        public BookingDomain(KFUSpaceContext context, UserDomain userDomain, RoomDomain roomDomain, PermissionDomain permissionDomain)
        {
            _context = context;
            _userDomain = userDomain;
            _roomDomain = roomDomain;
            _permissionDomain = permissionDomain;
        }
        [Authorize]
        public async Task<IEnumerable<BookingViewModel>> GetAllBooking()
        {
            return await _context.tblBookings.Where(booking => !booking.IsDeleted).Select(x => new BookingViewModel
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
        public async Task<int> AddBooking(BookingViewModel Booking)
        {
            try
            {

                var user = await _userDomain.GetUserByEmail(Booking.Email);
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
            var room = await _context.tblRooms.Include(x => x.RoomType).Include(x => x.Floor).ThenInclude(x => x.Building).FirstOrDefaultAsync(x => x.guid == id);

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
            return _context.tblBookings.FirstOrDefault(x => x.guid == id);
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

                BookingId = BookingId.Id,
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
                tblBookings bookings = getBookingByGuid(booking.guid);

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
                    //BookingStatues = booking.BookingStatues,
                    //RejectReason = booking.RejectReason,
                    //Duration = booking.Duration,
                    //Email = booking.Email,
                    //FullName = booking.FullName,
                    //PhoneNumber = booking.PhoneNumber
                };

                return bookingViewModel;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving the booking details.", ex);
            }

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

        public async Task<bool> RejectBooking(Guid bookingGuid, string rejectReason)
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
        public async Task<IEnumerable<BookingViewModel>> GetExtrnalBooking(string userEmail)
        {
            var permission = await _permissionDomain.GetPermissionByEmail(userEmail);

            return await _context.tblBookings.Where(booking => !booking.IsDeleted && /*booking.Room.Floor.BuildingId == permission.BuildingId &&*/ booking.BookingStatuesId == 2).Select(x => new BookingViewModel
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
        //public async Task<BookingViewModel> GetBookingOrder( BookingViewModel Booking )
        //{
        //    try
        //    {
        //        tblBookings tbl = await getBookingByguid(Booking.guid);
        //        var user = await _userDomain.GetUserByEmail(Booking.Email);
        //        var room = await _roomDomain.GetRoomByGuid(Booking.RoomGuid);

        //        BookingViewModel Bookings = new BookingViewModel
        //        {
        //            BookingId = Booking.BookingId,
        //            BookingDate = DateTime.Now,
        //            BookingStart = Booking.BookingStart,
        //            BookingEnd = Booking.BookingEnd,
        //            BookingStatues = Booking.BookingStatues,
        //            Duration = Booking.Duration,
        //            Email = user.Email,
        //            FullName = user.FullNameAR,
        //            PhoneNumber = user.PhoneNumber,
        //            guid = Booking.guid,
        //            IsDeleted = false,
        //            RoomId = room.Id,
        //            BookingStatuesId = Booking.BookingStatuesId,
        //            RoomAR = room.RoomAR,
        //            BuildingNameAr = room.BuildingNameAr,
        //            FloorNo = room.FloorNo,
        //            RoomNo = room.RoomNo,
        //            SeatCapacity = room.SeatCapacity,
        //        };
        //        return Bookings;
        //    }
        //    catch (Exception ex) {
        //        throw new Exception("ex");
        //    }
        //}


        public async Task <BookingViewModel> GetBookingByGuid(Guid bookingGuid)
        {
            var booking = await _context.tblBookings.FirstOrDefaultAsync(b => b.guid == bookingGuid && b.IsDeleted == false);
            BookingViewModel model = new BookingViewModel
            {
                FullName=booking.FullName,
                Email=booking.Email,        
                PhoneNumber=booking.PhoneNumber,
                guid = booking.guid,
                BookingId = booking.Id,
                BookingDate = booking.BookingDate,
                BookingStart= booking.BookingStart,
                BookingEnd= booking.BookingEnd,
                BookingStatuesId = booking.BookingStatuesId,
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
                    BookingDate = DateTime.Now,
                    BookingStart = bookinfo.BookingStart,
                    BookingEnd = bookinfo.BookingEnd,
                    BookingStatues = bookinfo.BookingStatues,
                    Email = bookinfo.Email,
                    FullName = bookinfo.FullName,
                    PhoneNumber = bookinfo.PhoneNumber,
                    guid = bookinfo.guid,
                    IsDeleted = false,
                    BookingStatuesId= bookinfo.BookingStatuesId,    
                };
                return 1;
            }
            catch (Exception ex)
            {
                // تسجيل الخطأ أو معالجته هنا
                return 0;
            }
        }


        //public async Task<int> getBookingOrder(BookingViewModel Booking)
        //{
        //    try
        //    {
        //        // جلب الحجز بناءً على guid
        //        tblBookings tbl = await getBookingByguid(Booking.guid);
        //        if (tbl == null)
        //        {
        //            throw new Exception("Booking not found");
        //        }

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

    }


}






