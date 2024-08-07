using Microsoft.EntityFrameworkCore;
using room_reservation.Models;
using room_reservation.ViewModel;

namespace room_reservation.Domain
{
    public class lecturesDomain
    {
        private readonly KFUSpaceContext _context;

        public lecturesDomain(KFUSpaceContext context)
        {
            _context = context;
        }
        //return list of data 
        public async Task<IEnumerable<LecturesViewModel>> getAlllectures()
        {

            return await _context.tblLectures.Select(x => new LecturesViewModel
            {
                Id = x.Id,
                BuildingNo = x.BuildingNo,
                RoomNo = x.RoomNo,
                LectureDate = x.LectureDate,
                StartLectureTime = x.StartLectureTime,
                EndLectureTime = x.EndLectureTime,
                //LectureDurations = x.LectureDurations,
                Semester = x.Semester


            }).ToListAsync();


        }

        public List<tblLectures> getlectures()
        {
            return _context.tblLectures.ToList();

        }



        public LecturesViewModel getlecturesById(int id)
        {
            var lecture = _context.tblLectures.SingleOrDefault(X => X.Id == id);
            LecturesViewModel lecturesViewModel = new LecturesViewModel
            {
                Id = lecture.Id,
                BuildingNo = lecture.BuildingNo,
                RoomNo = lecture.RoomNo,
                StartLectureTime = lecture.StartLectureTime,
                EndLectureTime = lecture.EndLectureTime,
                LectureDate = lecture.LectureDate,
                //LectureDurations = lecture.LectureDurations,
                Semester = lecture.Semester
            };
            return lecturesViewModel;
        }
        public int Addlectures(LecturesViewModel lectures)
        {
            try
            {
                tblLectures lectureinfo = new tblLectures();
                lectureinfo.Id = lectures.Id;
                lectureinfo.BuildingNo = lectures.BuildingNo;
                lectureinfo.RoomNo = lectures.RoomNo;
                lectureinfo.StartLectureTime = lectures.StartLectureTime;
                lectureinfo.EndLectureTime = lectures.EndLectureTime;
                lectureinfo.LectureDate = lectures.LectureDate;
                //lectureinfo.LectureDurations = lectures.LectureDurations;
                lectureinfo.Semester = lectures.Semester;

                _context.tblLectures.Add(lectureinfo);
                _context.SaveChanges();
                return 1; //successfully added
            }
            catch (Exception e)
            {
                return 0; //failed added
            }
        }

        public int EditLectures(LecturesViewModel lectures)
        {
            try
            {

                tblLectures lectureinfo = new tblLectures();
                lectureinfo.Id = lectures.Id;
                lectureinfo.BuildingNo = lectures.BuildingNo;
                lectureinfo.RoomNo = lectures.RoomNo;
                lectureinfo.StartLectureTime = lectures.StartLectureTime;
                lectureinfo.EndLectureTime = lectures.EndLectureTime;
                lectureinfo.LectureDate = lectures.LectureDate;
                //lectureinfo.LectureDurations = lectures.LectureDurations;
                lectureinfo.Semester = lectures.Semester;


                _context.tblLectures.Update(lectureinfo);
                _context.SaveChanges();
                return 1; //successfully added
            }
            catch (Exception e)
            {
                return 0; //updated failed
            }
        }
        public int DeleteLectures(LecturesViewModel lectures)
        {
            try
            {

                tblLectures lectureinfo = new tblLectures();
                lectureinfo.Id = lectures.Id;
                lectureinfo.BuildingNo = lectures.BuildingNo;
                lectureinfo.RoomNo = lectures.RoomNo;
                lectureinfo.StartLectureTime = lectures.StartLectureTime;
                lectureinfo.EndLectureTime = lectures.EndLectureTime;
                lectureinfo.LectureDate = lectures.LectureDate;
                //lectureinfo.LectureDurations = lectures.LectureDurations;
                lectureinfo.Semester = lectures.Semester;


                _context.tblLectures.Remove(lectureinfo);
                _context.SaveChanges();
                return 1;
            }
            catch (Exception e)
            {
                return 0;

            }
        }
    }
}

