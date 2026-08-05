using System;
using System.Collections.Generic;
using System.Text;

namespace SmartLearn1
{
    public abstract class Course : IEnrollable, ISearchable, IRatable
    {
        public int CourseId;
        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length > 100)
                {
                    Console.WriteLine("✗ Title must be 1-100 characters");
                    return;
                }
                _title = value;
            }
        }
        public string Description;
        public string InstructorName;
        public int MaxStudents;
        public int CurrentEnrollments;
        public string Category;

        private List<int> ratings = new List<int>();
        private List<string> reviews = new List<string>();

        protected Course(int courseId, string title, string description, string instructorName, int maxStudents, string category)
        {
            CourseId = courseId;
            Title = title;
            Description = description;
            InstructorName = instructorName;
            MaxStudents = maxStudents;
            CurrentEnrollments = 0;
            Category = category;
        }

        // Abstract members that derived classes must implement
        public abstract bool CanEnroll(Student student);
        public abstract void DisplayCourseInfo();
        public abstract string GetCourseType();
        public abstract int GetAvailableSeats();

        // IEnrollable implementation
        void IEnrollable.Enroll(Student student)
        {
            if (!CanEnroll(student))
            {
                Console.WriteLine("Cannot enroll: course is full or student not eligible.");
                return;
            }
            CurrentEnrollments++;
            student.EnrollInCourse(CourseId);
            Console.WriteLine($"Enrollment successful! Current enrollments: {CurrentEnrollments}");
            if (student is INotifiable notifier)
            {
                notifier.SendNotification($"You have been enrolled in '{Title}' (Course ID: {CourseId}).");
            }
        }

        void IEnrollable.Drop(Student student)
        {
            if (CurrentEnrollments > 0)
            {
                CurrentEnrollments--;
                student.DropCourse(CourseId);
                Console.WriteLine($"Unenrollment successful! Current enrollments: {CurrentEnrollments}");
                if (student is INotifiable notifier)
                {
                    notifier.SendNotification($"You have been unenrolled from '{Title}' (Course ID: {CourseId}).");
                }
            }
        }

        bool IEnrollable.CanEnroll(Student student)
        {
            // Defer to derived implementation
            return CanEnroll(student);
        }

        int IEnrollable.GetAvailableSeats()
        {
            return GetAvailableSeats();
        }

        // ISearchable implementation
        public bool MatchesSearch(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword)) return false;
            var k = keyword.Trim();
            return (Title?.IndexOf(k, StringComparison.OrdinalIgnoreCase) >= 0)
                || (Description?.IndexOf(k, StringComparison.OrdinalIgnoreCase) >= 0)
                || (Category?.IndexOf(k, StringComparison.OrdinalIgnoreCase) >= 0)
                || (InstructorName?.IndexOf(k, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        public string GetSearchSummary()
        {
            return $"[{GetCourseType()}] {Title} - {Category} (Instructor: {InstructorName})";
        }

        // IRatable implementation
        public void AddRating(int stars, string review)
        {
            if (stars < 1 || stars > 5)
            {
                Console.WriteLine("Rating must be between 1 and 5 stars.");
                return;
            }
            ratings.Add(stars);
            reviews.Add(review ?? string.Empty);
        }

        public double GetAverageRating()
        {
            if (ratings.Count == 0) return 0;
            double sum = 0;
            foreach (var r in ratings) sum += r;
            return sum / ratings.Count;
        }

        public int GetTotalRatings()
        {
            return ratings.Count;
        }
    }
}

