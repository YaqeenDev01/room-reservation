using room_reservation.Models;
using room_reservation.ViewModel;

namespace room_reservation.Domain
{
    public class FloorDomain
    {
        private readonly KFUSpaceContext _context;

        public FloorDomain(KFUSpaceContext context)
        {
            _context = context;
        }
        public IEnumerable<tblFloors> getAllFloors()
        {
            return _context.tblFloors;//Select * from tblFloors
        }

        /*
                public int AddFloor(FloorViewModel floor)
                {


                    tblFloors floorInfo = new tblFloors();
                    floorInfo.FloorNo = floor.FloorNo;
                   // floor

                    _context.tblUsers.Add(userInfo);
                    _context.SaveChanges();
                    return 1;



                }
        */
    }
}