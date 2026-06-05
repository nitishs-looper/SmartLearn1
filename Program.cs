using System;
using System.Data;
using System.Reflection.Metadata;

namespace project;

class Program
{
    static Dictionary<string, string> user = new Dictionary<string, string>();
    static Dictionary<string, string> usera = new Dictionary<string, string>();
    static void Main(String[] args)
    {
        bool enter = true;
        while (enter)
        {
            Console.WriteLine("============================");
            Console.WriteLine("Welcome to SmartLearn LMS!");
            Console.WriteLine("============================");
            Console.WriteLine("========Main Menu========");
            Console.WriteLine("1.Login");
            Console.WriteLine("2.Register");
            Console.WriteLine("3.Browse Courses");
            Console.WriteLine("4.Exit");
            Console.WriteLine("Enter your choice:");
            String choice = Console.ReadLine();
            Console.Clear();
            switch (choice)
            {
                case "1":
                    login(); break;
                case "2":
                    register(); break;
                case "3":
                    PrintCourses(); break;
                case "4":
                    exit(); enter = false; break;
                default:
                    Console.WriteLine("Invalid choice. Enter from 1-4");
                    break;
            }

        }
    }

    static readonly string[] courses = {
        "C# Programming Fundamentals",
        "Introduction to SQL Server",
        "Web Development with ASP.NET Core",
        "Advanced C# Techniques",
        "Database Design Principles",
        "RESTful API Development",
        "Entity Framework Core",
        "Front-End Development with React",
        "Cloud Computing with Azure",
        "Software Testing and Quality Assurance"
    };
    static void PrintCourses()
    {
        Console.WriteLine("=====Available Courses=====");
        for (int i = 0; i < courses.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {courses[i]}");
        }
        browseCourses();
    }

    static void login()
    {
        Console.WriteLine("=====Login=====");
        Console.WriteLine("Enter your username:");
        String username = Console.ReadLine();
        Console.WriteLine("Enter your password:");
        String password = Console.ReadLine();
        if (user.ContainsKey(username) && user[username] == password)
        {
            Console.WriteLine("Login successful! Welcome, " + username + "!");
        }
        else
        {
            Console.WriteLine("Invalid username or password. Please try again.");
        }
        Console.WriteLine("Enter your role (Student/Instructor/Admin):");
        String role = Console.ReadLine();
        Console.Clear();
        switch (role)
        {
            case "Student":
                showstudentdashboard(); break;
            case "Instructor":
                Console.WriteLine("Instructor Dashboard coming soon!"); break;
            case "Admin":
                Console.WriteLine("Admin Dashboard coming soon!"); break;
            default:
                Console.WriteLine("Invalid role. Please choose from Student, Instructor, or Admin."); break;
        }
    }
    static void register()
    {
        Console.WriteLine("=====Register=====");
        Console.WriteLine("Enter your desired username:");
        String username = Console.ReadLine();
        if (username != null)
        {
            validateusername(username);
        }
        Console.WriteLine("Enter your desired password:");
        String password = Console.ReadLine();
        if (password != null)
        {
            validatepassword(password);
        }
        Console.WriteLine("Enter your desired email:");
        String email = Console.ReadLine();
        if (email != null)
        {
            validateemail(email);
        }
        Console.WriteLine("Enter your desired role (Student/Instructor/Admin):");
        String role = Console.ReadLine();
        if (role != "Student" && role != "Instructor" && role != "Admin")
        {
            Console.WriteLine("Invalid role. Please choose from Student, Instructor, or Admin.");
            return;
        }
        Console.WriteLine("Registration successful! You can now log in with your credentials.");
        user.Add(username, password);
        usera.Add(email, role);
    }

    static void validateusername(string username)
    {
        if (user.ContainsKey(username))
        {
            Console.WriteLine("Username already exists. Please choose a different username.");
            Console.WriteLine("Enter your new username:");
            username = Console.ReadLine();
        }
    }
    static void validateemail(String email)
    {
        if (usera.ContainsKey(email))
        {
            Console.WriteLine("Email already exists. Please choose a different email.");
            Console.WriteLine("enter your new email:");
            email = Console.ReadLine();
        }
    }
    static void validatepassword(String password)
    {
        if (password.Length < 6)
        {
            Console.WriteLine("Password must be at least 6 characters long. Please choose a stronger password.");
            Console.WriteLine("Enter your password again:");
            password = Console.ReadLine();
        }
    }
    static void browseCourses()
    {
        Console.WriteLine("Enter search keyword:");
        String keyword = Console.ReadLine();
        int browsecounter = 0;
        bool found = false;
        if (keyword != null)
        {
            Console.WriteLine("Courses found:");
            foreach (var course in courses)
            {
                if (course.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                {
                    browsecounter++;
                    Console.WriteLine($"{browsecounter}.{course}");
                    found = true;
                }
            }
            if (!found)
            {
                Console.WriteLine("No courses found matching the keyword.");
            }
            Console.WriteLine("Would you try again? (y/n)");
            String tryagain = Console.ReadLine();
            switch (tryagain)
            {
                case "y":
                    browseCourses(); break;
                case "n":
                    break;
                default:
                    break;
            }
        }
    }
    static void showstudentdashboard()
    {
        Console.WriteLine("===============");
        Console.WriteLine("Student Dashboard:");
        Console.WriteLine("===============");
        Console.WriteLine("1.Browse Courses");
        Console.WriteLine("2.My Courses");
        Console.WriteLine("3.Progess");
        Console.WriteLine("4.Take Quiz");
        Console.WriteLine("5.Logout");
        Console.WriteLine("Enter your options:");
        string option = Console.ReadLine();
        Console.Clear();
        switch (option)
        {
            case "1":
                PrintCourses(); break;
            case "2":
                Console.WriteLine("My Courses coming soon!"); break;
            case "3":
                Console.WriteLine("Progress tracking coming soon!"); break;
            case "4":
                Console.WriteLine("Quiz functionality coming soon!"); break;
            case "5":
                Console.WriteLine("Logout option coming soon"); break;
            default:
                Console.WriteLine("Invalid option. Please choose from 1-5."); break;
        }
        Console.Clear();
    }
        static void exit()
        {
            Console.WriteLine("Exiting the application. Goodbye!");
        }

    
}