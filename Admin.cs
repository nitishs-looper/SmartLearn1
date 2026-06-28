using SmartLearn1;
using System;
using System.Collections.Generic;
public class Admin : User
{
    bool CanManageUsers;
    bool CanManageCourses;
    public Admin(string username, string password, string email)
        : base(username, password, email, "Admin")
    {
        CanManageUsers = true;
        CanManageCourses = true;
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
    public void GetSystemStats(List<User> users, List<Course> courses)
    { 
        int totalUsers = users.Count;
        int totalCourses = courses.Count;
        Console.WriteLine($"Total Users: {totalUsers}");
        Console.WriteLine($"Total Courses: {totalCourses}");
    }
   public void DisplayPermissions()
    {
        Console.WriteLine($"Can Manage Users: {CanManageUsers}");
        Console.WriteLine($"Can Manage Courses: {CanManageCourses}");
    }
}