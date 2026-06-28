using ICSharpCode.Decompiler.CSharp.Syntax;
using SmartLearn1;
using System;
using System.Data;
using System.Reflection.Metadata;
using System.Security.Principal;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
namespace project;

class Program
{
    static List<User> users = new List<User>();
    static List<Course> courses = new List<Course>()
    {
        new Course(1, "Learn the basics of C# programming.", "Enjoy coding", "Nitish", 10, "programming"),
        new Course(2, "Mastering Python: From Beginner to Pro", "Unlock the power of Python programming with our comprehensive course designed for all skill levels.", "John Doe", 15, "programming"),
        new Course(3, "Web Development Bootcamp: HTML, CSS, JavaScript", "Become a full-stack web developer with our intensive bootcamp covering HTML, CSS, and JavaScript.", "Jane Smith", 20, "web development"),
        new Course(4, "Data Science with R: From Data Analysis to Machine Learning", "Learn data science techniques using R programming language and apply them to real-world datasets.", "Emily Davis", 25, "data science"),
        new Course(5, "Mobile App Development with Flutter: Build Cross-Platform Apps", "Create stunning mobile applications for both Android and iOS using Flutter framework.", "Michael Brown", 30, "mobile development")
    };
    static List<Enrollment> enrollments = new List<Enrollment>();
    static bool isLoggedIn = false;
    static User currentUser = null;
    public static string currentUsername;
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
                    BrowseAndEnrollCourses(); break;
                case "4":
                    Logout(); enter = false; break;
                default:
                    Console.WriteLine("Invalid choice. Enter from 1-4");
                    break;
            }
        }
    }
    static void register()
    {
        Console.WriteLine("=====Register=====");
        Console.WriteLine("Enter your desired username:");
        String username = Console.ReadLine();
        if (username != null)
        {
            if (users.Exists(u => u.Username == username))
            {
                Console.WriteLine("Username already exists. Please choose a different username.");
                Console.WriteLine("Enter your new username:");
                username = Console.ReadLine();
            }
        }
        Console.WriteLine("Enter your desired password:");
        string password = Console.ReadLine();
        if (password != null)
        {
            if (password.Length < 6)
            {
                Console.WriteLine("Password must be at least 6 characters long. Please choose a stronger password.");
                Console.WriteLine("Enter your password again:");
                password = Console.ReadLine();
            }
        }
        Console.WriteLine("Enter your desired email:");
        String email = Console.ReadLine();
        if (email != null)
        {
            if (users.Exists(u => u.Email == email))
            {
                Console.WriteLine("Email already exists. Please choose a different email.");
                Console.WriteLine("enter your new email:");
                email = Console.ReadLine();
            }
        }
        Console.WriteLine("Enter your desired role (Student/Instructor/Admin):");
        String roles = Console.ReadLine();
        if (roles != "Student" && roles != "Instructor" && roles != "Admin")
        {
            Console.WriteLine("Invalid role. Please choose from Student, Instructor, or Admin.");
            return;
        }
        User proj = new User(username, password, email, roles);
        users.Add(proj);
        Console.WriteLine("Registration successful! You can now log in with your credentials.");
    }
    static void login()
    {
        Console.WriteLine("=====Login=====");
        Console.WriteLine("Enter your username:");
        string username = Console.ReadLine();

        if (string.IsNullOrEmpty(username))
        {
            Console.WriteLine("Username cannot be empty. Please try again.");
            return;
        }

        if (!users.Exists(u => u.Username == username))
        {
            Console.WriteLine("Username does not exist. Please register first.");
            return;
        }

        Console.WriteLine("Enter your password:");
        string password = Console.ReadLine();

        if (password == null)
        {
            Console.WriteLine("Password cannot be empty. Please try again.");
            return;
        }

        User checkpass = users.Find(u => u.Username == username);
        if (checkpass == null)
        {
            // defensive, though we checked existence above
            Console.WriteLine("Username does not exist. Please register first.");
            return;
        }

        bool get = checkpass.ValidatePassword(password);
        if (get)
        {
            isLoggedIn = true;
            currentUser = checkpass;
            Console.WriteLine("Welcome, " + currentUser.Username + "! You have successfully logged in as a " + currentUser.Role + ".");
        }
        else
        {
            Console.WriteLine("Incorrect password. Please try again.");
        }
        currentUsername = username;
        string[] roles = { "Student", "Instructor", "Admin" };
        for (int i = 0; i < roles.Length; i++)
        {
            if (users.Exists(u => u.Username == username && u.Password == password && u.Role == roles[i]))
            {
                switch (roles[i])
                {
                    case "Student":
                        showstudentdashboard(); break;
                    case "Instructor":
                        showinstructordashboard(); break;
                    case "Admin":
                        showadmindashboard(); break;
                    default:
                        Console.WriteLine("Invalid role. Please choose from Student, Instructor, or Admin."); break;
                }
            }
        }
    }
    static void Logout()
    {
        isLoggedIn = false;
        currentUser = null;
        Console.WriteLine("You have been logged out successfully.");
    }
    static void LoadSampleCourses()
    {
        courses.Clear();
        courses.Add(new Course(1, "C# Programming Fundamentals", "Learn the basics of C#", "Prof. Smith", 30, "Programming"));
        courses.Add(new Course(2, "Introduction to SQL Server", "Database fundamentals", "Prof. Johnson", 25, "Database"));
        courses.Add(new Course(3, "Python Interface Development", "Create a User interface", "Prof. Nitish", 10, "Programming"));
        courses.Add(new Course(4, "Mobile App Development with Flutter", "Build cross-platform mobile apps", "Prof. Brown", 20, "Mobile Development"));
        courses.Add(new Course(5, "Web Development with React", "Learn to build web applications using React", "Prof. Davis", 15, "Web Development"));
    }
    static void BrowseAndEnrollCourses()
    {
        LoadSampleCourses();
        Console.WriteLine("===========");
        Console.WriteLine("OUR COURSES:");
        Console.WriteLine("===========");
        foreach (var course in courses)
        {
            Console.WriteLine("==========================================");
            Console.Write(course.CourseId + ". ");
            Console.WriteLine(course.Title + " ");
            Console.WriteLine("=>" + course.Description);
            Console.WriteLine("Instructor Name :" + course.InstructorName + " ");
            Console.WriteLine("Number of total seats :" + course.MaxStudents + " ");
            Console.WriteLine("Belongs To :" + course.Category + " ");
        }
        Console.WriteLine("======================================");
        Console.WriteLine("Enter the number of the course you want to enroll in:");
        int courseId = Convert.ToInt32(Console.ReadLine());
        if (courseId > 5)
        {
            Console.WriteLine("incorrect course number. Please enter a valid course number from the list.");
        }
        Course obj = courses.Find(c => c.CourseId == courseId);
        bool c1 = obj.CanEnroll();
        if (users.Exists(e => e.Username == currentUsername && e.Role == "Student"))
        {
            if (c1 == false)
            {
                Console.WriteLine("Sorry, There are no available seats in this course. Please choose a different course.");
            }
            else
            {
                int enrollmentid = enrollments.Count + 1;
                obj.incrementEnrollment(1);
                Enrollment enrollment = new Enrollment(enrollmentid, currentUsername, courseId);
                enrollments.Add(enrollment);
                Console.WriteLine("You have successfully enrolled in the course: " + obj.Title);
            }
        }
        else
        {
            Console.WriteLine("You must be logged in as a student to enroll in courses. Please log in or register as a student to continue.");
        }
    }
    static void ShowMyEnrolledCourses()
    {
        Console.WriteLine("===========");
        Console.WriteLine("My Courses:");
        Console.WriteLine("===========");
        var myEnrollments = enrollments.FindAll(e => e.Username == currentUsername);
        if (myEnrollments.Count == 0)
        {
            Console.WriteLine("You have not enrolled in any courses yet.");
            return;
        }
        foreach (var enrollment in myEnrollments)
        {
            Course course = courses.Find(c => c.CourseId == enrollment.CourseId);
            if (course != null)
            {
                Console.WriteLine("==========================================");
                Console.Write(course.CourseId + ". ");
                Console.WriteLine(course.Title + " ");
                Console.WriteLine("=>" + course.Description);
                Console.WriteLine("Instructor Name :" + course.InstructorName + " ");
                Console.WriteLine("Number of total seats :" + course.MaxStudents + " ");
                Console.WriteLine("Belongs To :" + course.Category + " ");
            }
        }
    }
    //static void browseCourses()
    //{
    //    Console.WriteLine("=====================");
    //    Console.WriteLine("Enter search keyword:");
    //    String keyword = Console.ReadLine();
    //    int browsecounter = 0;
    //    bool found = false;
    //    if (keyword != null)
    //    {
    //        Console.WriteLine("Courses found:");
    //        foreach (var courses in courses)
    //        {
    //            if (courses.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase))
    //            {
    //                browsecounter++;
    //                Console.WriteLine($"{browsecounter}.{courses.Title}");
    //                found = true;
    //            }
    //        }
    //        if (!found)
    //        {
    //            Console.WriteLine("No courses found matching the keyword.");
    //        }
    //        Console.WriteLine("Would you try again? (y/n)");
    //        String tryagain = Console.ReadLine();
    //        switch (tryagain)
    //        {
    //            case "y":
    //                browseCourses(); break;
    //            case "n":
    //                break;
    //            default:
    //                break;
    //        }
    //    }
    //}
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
                BrowseAndEnrollCourses(); break;
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
    }
    static void showinstructordashboard()
    {
        Console.WriteLine("===============");
        Console.WriteLine("Instructor Dashboard:");
        Console.WriteLine("===============");
        Console.WriteLine("1.Create Course");
        Console.WriteLine("2.Manage Courses");
        Console.WriteLine("3.View Enrollments");
        Console.WriteLine("4.Grade Assignments");
        Console.WriteLine("5.Logout");
        Console.WriteLine("Enter your options:");
        string option = Console.ReadLine();
        switch (option)
        {
            case "1":
                Console.WriteLine("Course creation coming soon!"); break;
            case "2":
                Console.WriteLine("Course management coming soon!"); break;
            case "3":
                Console.WriteLine("Enrollment viewing coming soon!"); break;
            case "4":
                Console.WriteLine("Assignment grading coming soon!"); break;
            case "5":
                Console.WriteLine("Logout option coming soon"); break;
            default:
                Console.WriteLine("Invalid option. Please choose from 1-5."); break;
        }
    }
    static void showadmindashboard()
    {
        Console.WriteLine("===============");
        Console.WriteLine("Admin Dashboard:");
        Console.WriteLine("===============");
        Console.WriteLine("1.User Management");
        Console.WriteLine("2.Course Management");
        Console.WriteLine("3.System Settings");
        Console.WriteLine("4.Logout");
        Console.WriteLine("Enter your options:");
        string option = Console.ReadLine();
        switch (option)
        {
            case "1":
                Console.WriteLine("User management coming soon!"); break;
            case "2":
                Console.WriteLine("Course management coming soon!"); break;
            case "3":
                Console.WriteLine("System settings coming soon!"); break;
            case "4":
                Console.WriteLine("Logout option coming soon"); break;
            default:
                Console.WriteLine("Invalid option. Please choose from 1-4."); break;
        }
    }
}