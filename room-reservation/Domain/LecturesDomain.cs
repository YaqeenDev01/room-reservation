using Microsoft.EntityFrameworkCore;
using room_reservation.Models;
using room_reservation.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace room_reservation.Domain
{
    public class LecturesDomain
    {
        private readonly KFUSpaceContext _context;

        public LecturesDomain(KFUSpaceContext context)
        {
            _context = context;
        }

        // Return list of lectures
        public async Task<IEnumerable<LecturesViewModel>> GetAllLectures()
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

        // Check if a lecture exists with the same parameters
        public bool IsLectureExists(int buildingNo, int roomNo, DateTime lectureDate, TimeSpan startLectureTime, TimeSpan endLectureTime , string semester)
        {
            return _context.tblLectures.Any(l => l.BuildingNo == buildingNo
                                                && l.RoomNo == roomNo
                                                && l.LectureDate == lectureDate
                                                && l.Semester == semester
                                                && l.StartLectureTime == startLectureTime
                                                && l.EndLectureTime == endLectureTime);
        }

        // Check if a lecture overlaps with existing lectures
        public async Task<bool> IsLectureOverlapping(int buildingNo, int roomNo, DateTime lectureDate, TimeSpan startLectureTime, TimeSpan endLectureTime, string semester)
        {
            return await _context.tblLectures.AnyAsync(l =>
                l.BuildingNo == buildingNo &&
                l.RoomNo == roomNo &&
                l.LectureDate == lectureDate &&
                l.Semester == semester &&
                (l.StartLectureTime < endLectureTime && l.EndLectureTime > startLectureTime));
        }

        // Get a list of all lectures
        public List<tblLectures> GetLectures()
        {
            return _context.tblLectures.ToList();
        }

        // Get lecture by ID
        public async Task<LecturesViewModel> GetLectureById(int id)
        {
            var lecture = await _context.tblLectures.FindAsync(id);
            if (lecture == null)
            {
                return null;
            }

            return new LecturesViewModel
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
        }

        // Add a new lecture
        public async Task<int> AddLecture(LecturesViewModel lectures)
        {
            try
            {
                var lectureInfo = new tblLectures
                {
                    BuildingNo = lectures.BuildingNo,
                    RoomNo = lectures.RoomNo,
                    StartLectureTime = lectures.StartLectureTime,
                    EndLectureTime = lectures.EndLectureTime,
                    LectureDate = lectures.LectureDate,
                    LectureDurations = lectures.LectureDurations,
                    Semester = lectures.Semester
                };

                _context.tblLectures.Add(lectureInfo);
                await _context.SaveChangesAsync();
                return 1; // Successfully added
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if necessary
                return 0; // Failed to add
            }
        }

        // Edit an existing lecture
        public async Task<int> EditLecture(LecturesViewModel lectures)
        {
            try
            {
                var lectureInfo = await _context.tblLectures.FindAsync(lectures.Id);
                if (lectureInfo == null)
                {
                    return 0; // Lecture not found
                }

                lectureInfo.BuildingNo = lectures.BuildingNo;
                lectureInfo.RoomNo = lectures.RoomNo;
                lectureInfo.StartLectureTime = lectures.StartLectureTime;
                lectureInfo.EndLectureTime = lectures.EndLectureTime;
                lectureInfo.LectureDate = lectures.LectureDate;
                lectureInfo.LectureDurations = lectures.LectureDurations;
                lectureInfo.Semester = lectures.Semester;

                _context.tblLectures.Update(lectureInfo);
                await _context.SaveChangesAsync();
                return 1; // Successfully updated
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if necessary
                return 0; // Failed to update
            }
        }

        // Delete a lecture


                    // Delete a lecture
      public async Task<int> DeleteLecture(int id)
        {
            try
            {
                var lecture = await _context.tblLectures.FindAsync(id);
                if (lecture == null)
                {
                    return 0; // Lecture not found
                }

                _context.tblLectures.Remove(lecture);
                await _context.SaveChangesAsync();
                return 1; // Successfully deleted
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if necessary
                return 0; // Failed to delete
            }
        }
    }
}
