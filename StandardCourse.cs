using System;
using SmartLearn1;

namespace SmartLearn1
{
    public class StandardCourse : Course
    {
        public StandardCourse(int courseId, string title, string description, string instructorName, int maxStudents, string category)
            : base(courseId, title, description, instructorName, maxStudents, category)
        {
        }

        public override bool CanEnroll(Student student)
        {
            return CurrentEnrollments < MaxStudents;
        }

        public override void DisplayCourseInfo()
        {
            Console.WriteLine($"Course ID: {CourseId}");
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"Description: {Description}");
            Console.WriteLine($"Instructor: {InstructorName}");
            Console.WriteLine($"Seats: {CurrentEnrollments}/{MaxStudents}");
            Console.WriteLine($"Category: {Category}");
        }

        public override string GetCourseType()
        {
            return "Standard";
        }

        public override int GetAvailableSeats()
        {
            return MaxStudents - CurrentEnrollments;
        }
    }
}
