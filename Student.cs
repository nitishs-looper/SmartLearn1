using SmartLearn1;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
public class Student : User, ISearchable, INotifiable
{
    private int _progressPercentage;
    public int ProgressPercentage
    {
        get => _progressPercentage;
        set
        {
            if (value < 0 || value > 100)
            {
                Console.WriteLine("✗ Progress must be 0-100");
                return;
            }
            _progressPercentage = value;
        }
    }
    public List<int> EnrolledCourseIds { get; set; }
    public Dictionary<int, int> CourseProgress { get; set; }
    private List<string> notifications = new List<string>();
    public List<int> GetCompletedCourses()
    { List<int> completedCourses = new List<int>();
        foreach (var courseId in EnrolledCourseIds)
        {
            if (CourseProgress[courseId] >= 100)
            {
                completedCourses.Add(courseId);
            }
        }
        return completedCourses;
    }
    public double GetAverageProgress()
    {     if (EnrolledCourseIds.Count == 0) return 0;
        double totalProgress = 0;
        foreach (var courseId in EnrolledCourseIds)
        {
            totalProgress += CourseProgress[courseId];
        }
        return totalProgress / EnrolledCourseIds.Count;
    }
    public void DropCourse(int courseId)
    {
        if (EnrolledCourseIds.Contains(courseId))
        {
            EnrolledCourseIds.Remove(courseId);
            CourseProgress.Remove(courseId);
            Console.WriteLine("✓ Successfully dropped the course!");
        }
        else
        {
           Console.WriteLine("❌ Not enrolled in this course!");
        }
    }
    public Student(string username, string password, string email)
    : base(username, password, email, "Student")
    {
        EnrolledCourseIds = new List<int>();
        CourseProgress = new Dictionary<int, int>();
    }

    // Implement abstract members from User
    public override void DisplayDashboard()
    {
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine($"║ Welcome, {Username}");
        Console.WriteLine($"║ Enrolled Courses: {EnrolledCourseIds.Count}");
        Console.WriteLine("╠════════════════════════════════════════╣");
        Console.WriteLine("║ 1. Browse Courses");
        Console.WriteLine("║ 2. My Enrolled Courses");
        Console.WriteLine("║ 3. Update Progress");
        Console.WriteLine("║ 4. My Statistics");
        Console.WriteLine("║ 5. Logout");
        Console.WriteLine("╚════════════════════════════════════════╝");
    }

    public override string GetUserType()
    {
        return "Student";
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"Enrolled Courses: {EnrolledCourseIds.Count}");
        Console.WriteLine($"Average Progress: {GetAverageProgress():0.##}%");
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
            int prog = CourseProgress[courseId];
            Console.WriteLine($"Course {courseId} → {prog}% completed");
        }
        foreach(var completedCourseId in GetCompletedCourses())
        {
            Console.WriteLine($"✓ Completed Course: {completedCourseId}");
        }
        foreach(var courseId in EnrolledCourseIds)
        {
            if (CourseProgress[courseId] < 100)
            {
                ProgressPercentage = CourseProgress[courseId];
                Console.WriteLine($"❌ Incomplete Course: {courseId}");
            }
        }
        int dayCount = (DateTimeOffset.UtcNow - DateRegistered).Days;
        if(dayCount>30 && ProgressPercentage<50)
        {
            Console.WriteLine("⚠️ Warning: Your progress is below 50% after 30 days. Consider seeking help or adjusting your study plan.");
        }
    }

    // ISearchable implementation
    public bool MatchesSearch(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword)) return false;
        var k = keyword.Trim();
        return (Username?.IndexOf(k, StringComparison.OrdinalIgnoreCase) >= 0)
            || (Email?.IndexOf(k, StringComparison.OrdinalIgnoreCase) >= 0);
    }

    public string GetSearchSummary()
    {
        return $"[Student] {Username} ({Email}) - {EnrolledCourseIds.Count} courses";
    }

    // INotifiable implementation
    public void SendNotification(string message)
    {
        var entry = $"{DateTime.Now:g}: {message}";
        notifications.Add(entry);
        Console.WriteLine($"🔔 Notification for {Username}: {message}");
    }

    public List<string> GetNotificationHistory()
    {
        return new List<string>(notifications);
    }
}