using SmartLearn1;
using System;
using System.Collections.Generic;
public class Instructor : User
{
    public List<int> CreatedCourseIds { get; set; }
    public Instructor(string username, string password, string email)
        : base(username, password, email, "Instructor")
    {
        CreatedCourseIds = new List<int>();
    }
    public void CreateCourse(int courseId)
    {
        if (!CreatedCourseIds.Contains(courseId))
        {
            CreatedCourseIds.Add(courseId);
            Console.WriteLine("✓ Course created successfully!");
        }
        else
        {
            Console.WriteLine("❌ Course already exists!");
        }
    }
    public void ShowCreatedCourses()
    {
        if (CreatedCourseIds.Count == 0)
        {
            Console.WriteLine("No created courses.");
            return;
        }
        foreach (var courseId in CreatedCourseIds)
        {
            Console.WriteLine($"Created Course ID: {courseId}");
        }
    }
}

