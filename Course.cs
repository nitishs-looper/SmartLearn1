using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SmartLearn1
{
    public class Course
    {
        int CourseId;
        String Title;
        String Description;
        String InstructorName;
        int MaxStudents;
        int CurrentEnrollments;
        String Category;

        public Course(int courseId, String title, String description, String instructorName, int maxStudents , String category)
        {
            CourseId = courseId;
            Title = title;
            Description = description;
            InstructorName = instructorName;
            MaxStudents = maxStudents;
            CurrentEnrollments = 0;
            Category = category;
        }
    public bool CanEnroll()
        {
            return CurrentEnrollments < MaxStudents;
        }
        public void DisplayInfo()
        {
            Console.WriteLine($"Course ID: {CourseId}");
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"Description: {Description}");
            Console.WriteLine($"Instructor: {InstructorName}");
            Console.WriteLine($"Max Students: {MaxStudents}");
            Console.WriteLine($"Current Enrollments: {CurrentEnrollments}");
            Console.WriteLine("Category: " + Category);
        }
        public void incrementEnrollment(int multiple)
        {
            if (CurrentEnrollments < MaxStudents)
            {
                CurrentEnrollments++;
                Console.WriteLine($"Enrollment successful! Current enrollments: {CurrentEnrollments}");
            }
        }
        public void decrementEnrollment()
        {
            if (CurrentEnrollments > 0)
            {
                CurrentEnrollments--;
                Console.WriteLine($"Unenrollment successful! Current enrollments: {CurrentEnrollments}");
            }
        }

    }
}
