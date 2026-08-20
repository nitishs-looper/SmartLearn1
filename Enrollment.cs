using System;
using System.Collections.Generic;
using System.Text;

namespace SmartLearn1
{
    public class Enrollment
    {
      int EnrollmentId;
      public String StudentUsername;
      public int CourseId;
      DateTime EnrollmentDate;
      int ProgressPercentage;
      bool IsCompleted;

        public string Username { get; internal set; }

     
        public int Progress => ProgressPercentage;

        public Enrollment(int enrollmentId, String studentUsername, int courseId)
        {
            EnrollmentId = enrollmentId;
            StudentUsername = studentUsername;
            CourseId = courseId;
            EnrollmentDate = DateTime.Now;
            ProgressPercentage = 0;
            IsCompleted = false;
        }
        public void updateProgress(int progress)
        {
            ProgressPercentage = progress;
            if (ProgressPercentage >= 100)
            {
                IsCompleted = true;
                Console.WriteLine("Congratulations! You have completed the course.");
            }
            else
            {
                Console.WriteLine($"Progress updated to {ProgressPercentage}%.");
            }
        }
        public void markAsCompleted()
        {
            IsCompleted = true;
            ProgressPercentage = 100;
            Console.WriteLine("Course marked as completed.");
        }
        public void DisplayInfo()
        {
            Console.WriteLine($"Enrollment ID: {EnrollmentId}");
            Console.WriteLine($"Student Username: {StudentUsername}");
            Console.WriteLine($"Course ID: {CourseId}");
            Console.WriteLine($"Enrollment Date: {EnrollmentDate.ToString("g")}");
            Console.WriteLine($"Progress: {ProgressPercentage}%");
            Console.WriteLine("Completion Status: " + (IsCompleted ? "Completed" : "In Progress"));
        }
    }
}
