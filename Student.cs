using SmartLearn1;
using System;
using System.Collections.Generic;
public class Student : User
{
    public List<int> EnrolledCourseIds { get; set; }
    public Dictionary<int, int> CourseProgress { get; set; }
    public Student(string username, string password, string email)
    : base(username, password, email, "Student")
    {
        EnrolledCourseIds = new List<int>();
        CourseProgress = new Dictionary<int, int>();
    }
    public void EnrollInCourse(int courseId)
    {
        if (!EnrolledCourseIds.Contains(courseId))
        {
            EnrolledCourseIds.Add(courseId);
            CourseProgress[courseId] = 0;
            Console.WriteLine("✓ Successfully enrolled!");
        }
        else
        {
            Console.WriteLine("❌ Already enrolled in this course!");
        }
    }
    public void UpdateProgress(int courseId, int percentage)
    {
        if (CourseProgress.ContainsKey(courseId))
        {
            CourseProgress[courseId] = percentage;
            Console.WriteLine("✓ Progress updated");
        }
        else
        {
            Console.WriteLine("❌ Not enrolled in this course");
        }
    }
    public void ShowEnrolledCourses()
    {
        if (EnrolledCourseIds.Count == 0)
        {
            Console.WriteLine("No enrolled courses.");
            return;
        }
        foreach (var courseId in EnrolledCourseIds)
        {
            int progress = CourseProgress[courseId];
            Console.WriteLine($"Course {courseId} → {progress}% completed");
        }
    }
}