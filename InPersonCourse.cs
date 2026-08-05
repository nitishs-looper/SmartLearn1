using System;

namespace SmartLearn1
{
    public class InPersonCourse : Course
    {
        // MaxStudents with validation
        public int MaxStudents
        {
            get => base.MaxStudents;
            set
            {
                if (value <= 0)
                {
                    Console.WriteLine("✗ Max students must be greater than 0");
                    return;
                }
                base.MaxStudents = value;
            }
        }

        public string RoomNumber { get; set; }
        public string Building { get; set; }

        public InPersonCourse(int courseId, string title, string description, string instructorName, int maxStudents, string roomNumber, string building, string category)
            : base(courseId, title, description, instructorName, maxStudents, category)
        {
            MaxStudents = maxStudents;
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
            return "In-Person";
        }

        public override void DisplayCourseInfo()
        {
            Console.WriteLine($"Course ID: {CourseId}");
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"Instructor: {InstructorName}");
            Console.WriteLine($"Category: {Category}");
            Console.WriteLine($"Location: {Building}, Room {RoomNumber}");
            Console.WriteLine($"Capacity: {CurrentEnrollments}/{MaxStudents}");
            Console.WriteLine($"Seats remaining: {GetAvailableSeats()}");
            Console.WriteLine($"Enrollment Status: {(CanEnroll(null) ? "Open" : "Full")}");
            Console.WriteLine($"Rating: {GetAverageRating():0.##} ({GetTotalRatings()} ratings)");
        }
    }
}
