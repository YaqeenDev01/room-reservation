using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
                LectureDurations = x.LectureDurations,
                Semester = x.Semester


            }).ToListAsync();


        }
        public bool IsLectureExists(string buildingNo, string roomNo, DateTime lectureDate, TimeSpan startLectureTime, TimeSpan endLectureTime)
        {
            return _context.tblLectures.Any(l => l.BuildingNo == buildingNo
                                                && l.RoomNo == roomNo
                                                && l.LectureDate == lectureDate
                                                && l.StartLectureTime == startLectureTime
                                                && l.EndLectureTime == endLectureTime);
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
                LectureDurations = lecture.LectureDurations,
                Semester = lecture.Semester
            };
            return lecturesViewModel;
        }
        public async Task<int> Addlecture(LecturesViewModel lectures)
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
                lectureinfo.LectureDurations = lectures.LectureDurations;
                lectureinfo.Semester = lectures.Semester;

                _context.tblLectures.Add(lectureinfo);
                await _context.SaveChangesAsync();
                return 1; //successfully added
            }
            catch (Exception ex)
            {
                
                return 0;
            }
        }


        public async Task<int> EditLecture(LecturesViewModel lectures)
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
                lectureinfo.LectureDurations = lectures.LectureDurations;
                lectureinfo.Semester = lectures.Semester;


                _context.tblLectures.Update(lectureinfo);

                await _context.SaveChangesAsync();
                return 1; //successfully added
            }
            catch (Exception e)
            {
                return 0; //updated failed
            }
        }
       
        public async Task DeleteLecture(int id)
        {
            var lecture = _context.tblLectures.Where(x => x.Id == id).SingleOrDefault();
            _context.tblLectures.Remove(lecture);
            await _context.SaveChangesAsync();

        }

    }
  

}

