using System;

namespace SmartLearn1
{
    public class OnlineCourse : Course
    {
        public int VideoDurationMinutes { get; set; }
        public string StreamingUrl { get; private set; }

        public OnlineCourse(int courseId, string title, string description, string instructorName, int videoDurationMinutes, string category)
            : base(courseId, title, description, instructorName, int.MaxValue, category)
        {
            VideoDurationMinutes = videoDurationMinutes;
            StreamingUrl = "https://smartlearn.com/stream/" + courseId;
        }

        public override bool CanEnroll(Student student)
        {
            // Online courses are assumed to have unlimited capacity
            return true;
        }

        public override int GetAvailableSeats()
        {
            return int.MaxValue;
        }

        public override string GetCourseType()
        {
            return "Online";
        }

        public override void DisplayCourseInfo()
        {
            Console.WriteLine($"Course ID: {CourseId}");
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"Instructor: {InstructorName}");
            Console.WriteLine($"Category: {Category}");
            Console.WriteLine($"Duration: {VideoDurationMinutes} minutes ({VideoDurationMinutes / 60}h {VideoDurationMinutes % 60}m)");
            Console.WriteLine($"Streaming URL: {StreamingUrl}");
            Console.WriteLine($"Capacity: Unlimited");
            Console.WriteLine($"Current Enrollments: {CurrentEnrollments}");
            Console.WriteLine($"Rating: {GetAverageRating():0.##} ({GetTotalRatings()} ratings)");
            Console.WriteLine($"Enrollment Status: {(CanEnroll(null) ? "Open" : "Closed")}");
        }
    }
}
