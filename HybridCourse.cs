using System;
using System.Collections.Generic;

namespace SmartLearn1
{
    public class HybridCourse : Course
    {
        // Use MaxStudents property from base Course (validation centralized)

        public int OnlineVideoDuration { get; set; }
        public string RoomNumber { get; set; }
        public string Building { get; set; }
        public List<DateTime> InPersonSessions { get; set; } = new List<DateTime>();

        public HybridCourse(int courseId, string title, string description, string instructorName, int maxStudents, int onlineVideoDuration, string roomNumber, string building, string category)
            : base(courseId, title, description, instructorName, maxStudents, category)
        {
            MaxStudents = maxStudents;
            OnlineVideoDuration = onlineVideoDuration;
            RoomNumber = roomNumber;
            Building = building;
        }

        public override bool CanEnroll(Student student)
        {
            return CurrentEnrollments < MaxStudents;
        }

        public override int GetAvailableSeats()
        {
            return MaxStudents - CurrentEnrollments;
        }

        public override string GetCourseType()
        {
            return "Hybrid";
        }

        public override void DisplayCourseInfo()
        {
            Console.WriteLine($"Course ID: {CourseId}");
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"Instructor: {InstructorName}");
            Console.WriteLine($"Category: {Category}");
            Console.WriteLine($"Online Component: {OnlineVideoDuration} minutes ({OnlineVideoDuration / 60}h {OnlineVideoDuration % 60}m)");
            Console.WriteLine($"In-Person Location: {Building}, Room {RoomNumber}");
            Console.WriteLine($"In-Person Sessions: {(InPersonSessions.Count == 0 ? "None scheduled" : InPersonSessions.Count.ToString())}");
            Console.WriteLine($"Capacity: {CurrentEnrollments}/{MaxStudents}");
            Console.WriteLine($"Seats remaining: {GetAvailableSeats()}");
            Console.WriteLine($"Enrollment Status: {(GetAvailableSeats() > 0 ? "Open" : "Full")}");
            Console.WriteLine($"Rating: {GetAverageRating():0.##} ({GetTotalRatings()} ratings)");
        }
    }
}
