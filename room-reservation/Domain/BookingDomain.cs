using Microsoft.EntityFrameworkCore;
using room_reservation.Models;
using room_reservation.ViewModel;

namespace room_reservation.Domain {
    public class BookingDomain
    {
        private readonly KFUSpaceContext _context;

        public BookingDomain(KFUSpaceContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BuildingViewModel>> GetAllBooking()
        {
            return (IEnumerable<BuildingViewModel>)await _context.tblBookings.Select(x => new BookingViewModel
            {
                Id = x.Id,
                BookingDate = x.BookingDate,
                BookingStart = x.BookingStart,
                BookingEnd = x.BookingEnd,
                guid = x.guid,

                //Bookings.BookingStatues = booking.BookingStatues;
                //Bookings.RejectReason = booking.RejectReason;
                //Bookings.Duration = booking.Duration;
                //Bookings.Email = booking.Email;
                //Bookings.FullName = booking.FullName;
                //Bookings.PhoneNumber = booking.PhoneNumber;


            }).ToListAsync();
        }

        public async Task<IEnumerable<tblBookings>> getAllBooking()
        {
            return await _context.tblBookings.ToListAsync();
        }
        public string AddBooking(BookingViewModel Booking)
        {
            try
            {
                tblBookings Bookings = new tblBookings();
                Bookings.Id = Booking.Id;
                Bookings.BookingDate = Booking.BookingDate;
                Bookings.BookingStart = Booking.BookingStart;
                Bookings.BookingEnd = Booking.BookingEnd;
                //Bookings.BookingStatues = booking.BookingStatues;
                //Bookings.RejectReason = booking.RejectReason;
                //Bookings.Duration = booking.Duration;
                //Bookings.Email = booking.Email;
                //Bookings.FullName = booking.FullName;
                //Bookings.PhoneNumber = booking.PhoneNumber;
               
                Bookings.guid = Guid.NewGuid();
                Bookings.IsDeleted = false;
                Bookings.RoomId = Booking.RoomId;

                _context.tblBookings.Add(Bookings);
                _context.SaveChanges();
                return "successful";
            }
            catch (Exception ex)
            {
                // تسجيل الخطأ أو معالجته هنا
                return $"Error: {ex.Message}";
            }
        }

        public tblBookings getBookingByGuid(Guid id)

        {
            return _context.tblBookings.FirstOrDefault(x => x.guid == id);
        }


        public BookingViewModel getBookinggByid(Guid id)
        {
            var BookingId = _context.tblBookings.FirstOrDefault(x => x.guid == id);
            BookingViewModel models = new BookingViewModel
            {
             
                Id = BookingId.Id,
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
                Bookings.Id = booking.Id;
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
                    Id = bookings.Id,
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






