using SmartLearn1;
using System;
using System.Collections.Generic;
public class Admin : User
{
    public bool CanManageUsers { get; set; }
    public bool CanManageCourses { get; set; }
    public string AdminLevel { get; set; }

    public Admin(string username, string password, string email)
        : base(username, password, email, "Admin")
    {
        CanManageUsers = true;
        CanManageCourses = true;
        AdminLevel = "Super";
    }

    public override void DisplayDashboard()
    {
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine($"║ Admin: {Username}");
        Console.WriteLine($"║ Level: {AdminLevel}");
        Console.WriteLine("╠════════════════════════════════════════╣");
        Console.WriteLine("║ 1. Manage Users");
        Console.WriteLine("║ 2. Manage Courses");
        Console.WriteLine("║ 3. View System Reports");
        Console.WriteLine("║ 4. System Settings");
        Console.WriteLine("║ 5. Logout");
        Console.WriteLine("╚════════════════════════════════════════╝");
    }

    public override string GetUserType()
    {
        return "Admin";
    }
    public void ViewAllUsers(List<User> users)
    { Console.WriteLine("All Users:");
        foreach (var user in users)
        {
            user.DisplayInfo();
            Console.WriteLine("--------------------");
        }
    }
    public void DeactivateUser(User user)
    {
        user.DeactivateAccount();
        Console.WriteLine($"User {user.Username} has been deactivated.");
    }
    // Deletes a user from the users list and removes their enrollments
    public void DeleteUser(List<User> users, List<SmartLearn1.Enrollment> enrollments, User user)
    {
        if (user == null)
        {
            Console.WriteLine("No user specified.");
            return;
        }
        // Attempt to remove user from the list
        bool removed = users.Remove(user);
        if (removed)
        {
            // Remove any enrollments for this user
            if (enrollments != null)
            {
                enrollments.RemoveAll(e => e.Username == user.Username || e.StudentUsername == user.Username);
            }
            Console.WriteLine($"User {user.Username} has been deleted and their enrollments were removed.");
        }
        else
        {
            Console.WriteLine($"Failed to delete user {user.Username}. User may not exist in the list.");
        }
    }
    public void GetSystemStats(List<User> users, List<Course> courses, List<Enrollment> enrollments)
    {
        int totalUsers = users.Count;
        int totalCourses = courses.Count;
        Console.WriteLine($"Total Users: {totalUsers}");
        Console.WriteLine($"Total Courses: {totalCourses}");
        int studentsWithFullCompletion = 0;
        foreach (var user in users)
        {
            if (user.Role != "Student") continue;
            bool hasEnrollment = false;
            bool allCompleted = true;
            foreach (var e in enrollments)
            {
                if (e.StudentUsername == user.Username)
                {
                    hasEnrollment = true;
                    if (e.Progress < 100)
                    {
                        allCompleted = false;
                        break;
                    }
                }
            }
            if (hasEnrollment && allCompleted) studentsWithFullCompletion++;
        }
        Console.WriteLine($"Students with 100% completion (for all enrolled courses): {studentsWithFullCompletion}");
        var courseCounts = new Dictionary<int, int>();
        foreach (var c in courses)
            courseCounts[c.CourseId] = 0;
        foreach (var e in enrollments)
        {
            if (courseCounts.ContainsKey(e.CourseId))
                courseCounts[e.CourseId]++;
        }
        int mostPopularId = -1, leastPopularId = -1;
        int mostCount = -1, leastCount = int.MaxValue;
        foreach (var kv in courseCounts)
        {
            if (kv.Value > mostCount)
            {
                mostCount = kv.Value;
                mostPopularId = kv.Key;
            }
            if (kv.Value < leastCount)
            {
                leastCount = kv.Value;
                leastPopularId = kv.Key;
            }
        }

        string mostTitle = "(none)";
        string leastTitle = "(none)";
        foreach (var c in courses)
        {
            if (c.CourseId == mostPopularId) mostTitle = c.Title;
            if (c.CourseId == leastPopularId) leastTitle = c.Title;
        }
        Console.WriteLine($"Most popular course: {mostTitle} ({mostCount} enrollments)");
        Console.WriteLine($"Least popular course: {leastTitle} ({leastCount} enrollments)");
        int inactiveUsers = 0;
        foreach (var u in users)
        {
            if (!u.IsActive) inactiveUsers++;
        }
        Console.WriteLine($"Inactive users: {inactiveUsers}");
    }
   public void DisplayPermissions()
    {
        Console.WriteLine($"Can Manage Users: {CanManageUsers}");
        Console.WriteLine($"Can Manage Courses: {CanManageCourses}");
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"Admin Level: {AdminLevel}");
        Console.WriteLine($"Permissions: ManageUsers={CanManageUsers}, ManageCourses={CanManageCourses}");
    }
}