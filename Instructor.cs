using SmartLearn1;
using System;
using System.Collections.Generic;
public class Instructor : User, INotifiable
{
    public List<int> CreatedCourseIds { get; set; }
    private List<string> notifications = new List<string>();
    public string Department { get; set; } = "Computer Science";
    public Instructor(string username, string password, string email)
        : base(username, password, email, "Instructor")
    {
        CreatedCourseIds = new List<int>();
    }
    // Implement abstract members from User
    public override void DisplayDashboard()
    {
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine($"║ Professor {Username}");
        Console.WriteLine($"║ Department: {Department}");
        Console.WriteLine($"║ Courses Teaching: {CreatedCourseIds.Count}");
        Console.WriteLine("╠════════════════════════════════════════╣");
        Console.WriteLine("║ 1. My Courses");
        Console.WriteLine("║ 2. Create New Course");
        Console.WriteLine("║ 3. View Student Roster");
        Console.WriteLine("║ 4. Grade Assignments");
        Console.WriteLine("║ 5. Logout");
        Console.WriteLine("╚════════════════════════════════════════╝");
    }

    public override string GetUserType()
    {
        return "Instructor";
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"Department: {Department}");
        Console.WriteLine($"Courses Teaching: {CreatedCourseIds.Count}");
    }

    // INotifiable implementation
    public void SendNotification(string message)
    {
        var entry = $"{DateTime.Now:g}: {message}";
        notifications.Add(entry);
        Console.WriteLine($"\uD83D\uDD14 Notification for {Username}: {message}");
    }

    public List<string> GetNotificationHistory()
    {
        return new List<string>(notifications);
    }
    public int GetStudentCount(List<Enrollment> enrollments)
    { int studentCount = 0;
        foreach (var courseId in CreatedCourseIds)
        {
            foreach (var enrollment in enrollments)
            {
                if (enrollment.CourseId == courseId)
                {
                    studentCount++;
                }
            }
        }
        return studentCount;
    }
    public Course GetCourseById(int id, List<Course> courses)
    {
        foreach (var course in courses)
        {
            if (course.CourseId == id)
            {
                return course;
            }
        }
        return null;
    }
    public void AddCourse(int courseId)
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
    public void RemoveCourse(int courseId)
    {
        if (CreatedCourseIds.Contains(courseId))
        {
            CreatedCourseIds.Remove(courseId);
            Console.WriteLine("✓ Course removed successfully!");
        }
        else
        {
            Console.WriteLine("❌ Course not found!");
        }
    }
    // Show course stats: title, number of enrolled students, average progress
    public void ShowMyCourses(List<Course> courses, List<Enrollment> enrollments)
    {
        if (CreatedCourseIds == null || CreatedCourseIds.Count == 0)
        {
            Console.WriteLine("No created courses.");
            return;
        }

        Console.WriteLine("My Courses:");
        Console.WriteLine("-----------------------------------------------");
        Console.WriteLine("Title\t| Enrolled | Avg Progress");
        Console.WriteLine("-----------------------------------------------");

        foreach (var courseId in CreatedCourseIds)
        {
            var course = GetCourseById(courseId, courses);
            string title = course != null ? course.Title : $"(ID {courseId})";

            // find enrollments for this course
            int count = 0;
            int sumProgress = 0;
            foreach (var e in enrollments)
            {
                if (e.CourseId == courseId)
                {
                    count++;
                    // use exposed Progress property
                    try { sumProgress += e.Progress; } catch { }
                }
            }

            string avgProgress = count > 0 ? (sumProgress / (double)count).ToString("0.##") + "%" : "N/A";

            Console.WriteLine($"{title}\t| {count} \t | {avgProgress}");
        }

        Console.WriteLine("-----------------------------------------------");
    }
}

