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
        new StandardCourse(1, "Learn the basics of C# programming.", "Enjoy coding", "Nitish", 10, "programming"),
        new StandardCourse(2, "Mastering Python: From Beginner to Pro", "Unlock the power of Python programming with our comprehensive course designed for all skill levels.", "John Doe", 15, "programming"),
        new StandardCourse(3, "Web Development Bootcamp: HTML, CSS, JavaScript", "Become a full-stack web developer with our intensive bootcamp covering HTML, CSS, and JavaScript.", "Jane Smith", 20, "web development"),
        new StandardCourse(4, "Data Science with R: From Data Analysis to Machine Learning", "Learn data science techniques using R programming language and apply them to real-world datasets.", "Emily Davis", 25, "data science"),
        new StandardCourse(5, "Mobile App Development with Flutter: Build Cross-Platform Apps", "Create stunning mobile applications for both Android and iOS using Flutter framework.", "Michael Brown", 30, "mobile development")
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
                    exit(); enter = false; break;
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
        User proj;
        if (roles == "Student") proj = new Student(username, password, email);
        else if (roles == "Instructor") proj = new Instructor(username, password, email);
        else proj = new Admin(username, password, email);
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
        currentUser.DisplayDashboard();
        // After displaying dashboard, delegate interactive handling to role-specific handlers
        if (currentUser is Student s)
        {
            HandleStudentDashboard(s);
        }
        else if (currentUser is Instructor ins)
        {
            HandleInstructorDashboard(ins);
        }
        else if (currentUser is Admin adm)
        {
            HandleAdminDashboard(adm);
        }
    }
    static void Logout()
    {
        isLoggedIn = false;
        currentUser = null;
        Console.WriteLine("You have been logged out successfully.");
    }

    // Role-specific interactive handlers
    public static void HandleStudentDashboard(Student studentObj)
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Enter choice (1-5):");
            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    BrowseAndEnrollCourses();
                    break;
                case "2":
                    ShowMyEnrolledCourses();
                    break;
                case "3":
                    Console.WriteLine("Enter Course ID to update progress:");
                    if (!int.TryParse(Console.ReadLine(), out int cid)) { Console.WriteLine("Invalid ID"); break; }
                    Console.WriteLine("Enter new progress percentage (0-100):");
                    if (!int.TryParse(Console.ReadLine(), out int pct) || pct < 0 || pct > 100) { Console.WriteLine("Invalid percentage"); break; }
                    var enr = enrollments.Find(e => e.Username == studentObj.Username && e.CourseId == cid);
                    if (enr != null)
                    {
                        enr.updateProgress(pct);
                        if (studentObj.CourseProgress == null) studentObj.CourseProgress = new Dictionary<int, int>();
                        studentObj.CourseProgress[cid] = pct;
                        Console.WriteLine("✓ Progress updated");
                    }
                    else
                    {
                        Console.WriteLine("Not enrolled in this course.");
                    }
                    break;
                case "4":
                    studentObj.DisplayInfo();
                    break;
                case "5":
                    Logout();
                    return;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }
    }

    public static void HandleInstructorDashboard(Instructor instructorObj)
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Enter choice (1-5):");
            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    instructorObj.ShowMyCourses(courses, enrollments);
                    break;
                case "2":
                    // Create minimal course entry
                    int newId = courses.Count > 0 ? courses[^1].CourseId + 1 : 1;
                    Console.WriteLine("Enter course title:");
                    var title = Console.ReadLine();
                    Console.WriteLine("Enter course description:");
                    var desc = Console.ReadLine();
                    var newCourse = new StandardCourse(newId, title ?? "Untitled", desc ?? "", instructorObj.Username, 10, "General");
                    courses.Add(newCourse);
                    instructorObj.AddCourse(newId);
                    Console.WriteLine($"✓ Course '{title}' created with ID {newId}");
                    break;
                case "3":
                    Console.WriteLine("Enter Course ID to view roster:");
                    if (!int.TryParse(Console.ReadLine(), out int cid)) { Console.WriteLine("Invalid ID"); break; }
                    Console.WriteLine($"Students enrolled in course {cid}:");
                    foreach (var e in enrollments)
                    {
                        if (e.CourseId == cid) Console.WriteLine($"- {e.Username} (Progress: {e.Progress}%)");
                    }
                    break;
                case "4":
                    Console.WriteLine("Enter Course ID to grade:");
                    if (!int.TryParse(Console.ReadLine(), out int gcid)) { Console.WriteLine("Invalid ID"); break; }
                    Console.WriteLine("Enter student username:");
                    var uname = Console.ReadLine();
                    var enr = enrollments.Find(e => e.CourseId == gcid && e.Username == uname);
                    if (enr == null) { Console.WriteLine("Enrollment not found"); break; }
                    Console.WriteLine("Enter grade/progress percent (0-100):");
                    if (!int.TryParse(Console.ReadLine(), out int grade) || grade < 0 || grade > 100) { Console.WriteLine("Invalid grade"); break; }
                    enr.updateProgress(grade);
                    Console.WriteLine("✓ Graded/Progress updated");
                    break;
                case "5":
                    Logout();
                    return;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }
    }

    public static void HandleAdminDashboard(Admin adminObj)
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Enter choice (1-5):");
            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    adminObj.ViewAllUsers(users);
                    Console.WriteLine("Enter username to deactivate (or blank to cancel):");
                    var toDeact = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(toDeact))
                    {
                        var u = users.Find(x => x.Username == toDeact);
                        if (u != null) adminObj.DeactivateUser(u);
                        else Console.WriteLine("User not found");
                    }
                    break;
                case "2":
                    Console.WriteLine("Courses:");
                    foreach (var c in courses) Console.WriteLine($"{c.CourseId}: {c.Title}");
                    Console.WriteLine("Enter Course ID to remove (or blank to cancel):");
                    var input = Console.ReadLine();
                    if (int.TryParse(input, out int rid))
                    {
                        var course = courses.Find(c => c.CourseId == rid);
                        if (course != null) { courses.Remove(course); Console.WriteLine("✓ Course removed"); }
                        else Console.WriteLine("Course not found");
                    }
                    break;
                case "3":
                    adminObj.GetSystemStats(users, courses, enrollments);
                    break;
                case "4":
                    adminObj.DisplayPermissions();
                    break;
                case "5":
                    Logout();
                    return;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }
    }
    static void LoadSampleCourses()
    {
        // Initialize the more extensive sample set
        InitializeCourses();
        Console.WriteLine("✓ Sample courses loaded successfully!");
    }
    static void InitializeCourses()
    {
        courses.Clear();
        courses.Add(new OnlineCourse(101, "C# Fundamentals", "Learn C# fundamentals from variables to OOP.", "Prof. Smith", 450, "Programming"));
        courses.Add(new OnlineCourse(102, "Python for Beginners", "A gentle introduction to Python programming.", "Prof. Johnson", 360, "Programming"));
        courses.Add(new OnlineCourse(103, "Web Development Basics", "HTML, CSS and JavaScript for beginners.", "Prof. Williams", 540, "Web Development"));
        courses.Add(new OnlineCourse(104, "Data Structures", "Core data structures and algorithms in practice.", "Prof. Brown", 600, "Programming"));
        courses.Add(new OnlineCourse(105, "Machine Learning Intro", "Introductory machine learning concepts and workflows.", "Prof. Davis", 720, "Data Science"));
        courses.Add(new InPersonCourse(201, "Database Design Workshop", "Design relational databases and ER modelling.", "Prof. Taylor", 25, "B-101", "Engineering Building", "Database"));
        courses.Add(new InPersonCourse(202, "Network Security Lab", "Hands-on network security lab sessions.", "Prof. Anderson", 20, "C-205", "CS Building", "Security"));
        courses.Add(new InPersonCourse(203, "Mobile App Development", "In-person mobile app labs and pair programming.", "Prof. Martinez", 30, "A-301", "Tech Center", "Mobile Development"));
        courses.Add(new HybridCourse(301, "Full-Stack Development", "Combination of online content and in-person workshops.", "Prof. Clark", 30, 720, "C-201", "CS Building", "Web Development"));
        courses.Add(new HybridCourse(302, "Cloud Computing", "Cloud fundamentals with online labs and on-campus sessions.", "Prof. Lewis", 25, 600, "D-101", "Engineering Building", "Cloud"));
    }
    static void UniversalSearch(List<Course> courseList, List<User> userList)
    {
        var combined = new List<ISearchable>();

        if (courseList != null)
        {
            foreach (var c in courseList)
            {
                if (c != null) combined.Add(c);
            }
        }

        if (userList != null)
        {
            foreach (var u in userList)
            {
                if (u is ISearchable s)
                {
                    combined.Add(s);
                }
            }
        }

        Console.WriteLine("Enter search keyword:");
        string keyword = Console.ReadLine();

        var results = SearchEngine.Search(combined, keyword);
        SearchEngine.DisplayResults(results);
    }
    static void EnrollStudentInCourse(Student student)
    {
        Console.WriteLine("\n=== AVAILABLE COURSES ===");
        foreach (Course course in courses)
        {
            Console.WriteLine($"\n[{course.CourseId}] {course.Title}");
            Console.WriteLine($"Category: {course.Category}");
            Console.WriteLine($"Instructor: {course.InstructorName}");
            Console.WriteLine($"Enrollment: {course.CurrentEnrollments}/{course.MaxStudents}");
            Console.WriteLine($"Available: {(course.CanEnroll(student) ? "✓ Yes" : "✗ Full")}");
            Console.WriteLine("---");
        }
        Console.Write("\nEnter Course ID to enroll: ");
        if (!int.TryParse(Console.ReadLine(), out int courseId))
        {
            Console.WriteLine(" Invalid course ID.");
            return;
        }
        Course selectedCourse = courses.Find(c => c.CourseId == courseId);
        if (selectedCourse == null)
        {
            Console.WriteLine(" Course not found.");
            return;
        }
        if (!selectedCourse.CanEnroll(student))
        {
            Console.WriteLine(" Course is full!");
            return;
        }
        if (student.EnrolledCourseIds.Contains(courseId))
        { 
            Console.WriteLine(" Already enrolled in this course!");
            return;
        }
        // Enroll via course implementation
        ((IEnrollable)selectedCourse).Enroll(student);
        int enrollmentId = enrollments.Count + 1;
        Enrollment newEnrollment = new Enrollment(enrollmentId, student.Username, courseId);
        enrollments.Add(newEnrollment);
        // Notify instructor if possible
        var instructorNotify = users.Find(u => u.Username == selectedCourse.InstructorName) as INotifiable;
        if (instructorNotify != null)
        {
            instructorNotify.SendNotification($"Student {student.Username} has enrolled in your course '{selectedCourse.Title}' (ID: {courseId}).");
        }
        Console.WriteLine($"✓ Successfully enrolled in '{selectedCourse.Title}'!");
    }
    static void UpdateStudentProgress(Student student)
    {
        student.ShowEnrolledCourses();
        Console.Write("\nEnter Course ID to update: ");
        if (!int.TryParse(Console.ReadLine(), out int courseId))
        {
            Console.WriteLine(" Invalid course ID.");
            return;
        }
        Console.Write("Enter progress percentage (0-100): ");
        if (!int.TryParse(Console.ReadLine(), out int progress) || progress < 0 || progress > 100)
        {
            Console.WriteLine(" Invalid progress value.");
            return;
        }
        student.UpdateProgress(courseId, progress);
        Enrollment enrollment = enrollments.Find(e => e.StudentUsername == student.Username && e.CourseId == courseId);
        if (enrollment != null) 
        { 
            enrollment.updateProgress(progress);
            Console.WriteLine("✓ Progress updated!");
        }
    }
    static void DropStudentCourse(Student student)
    {
        student.ShowEnrolledCourses();
        Console.Write("\nEnter Course ID to drop: ");
        if (!int.TryParse(Console.ReadLine(), out int courseId))
        { 
            Console.WriteLine(" Invalid course ID.");
            return; 
        }
        Course course = courses.Find(c => c.CourseId == courseId);
        if (course != null)
        {
            // Delegate drop to course implementation which updates student and counts
            ((IEnrollable)course).Drop(student);
        }
        Enrollment enrollment = enrollments.Find(e => e.StudentUsername == student.Username && e.CourseId == courseId);
        if (enrollment != null)
        { 
            enrollments.Remove(enrollment);
        }
    }
    static void ShowStudentStats(Student student) 
    {
        Console.WriteLine("\n=== YOUR STATISTICS ===");
        Console.WriteLine($"Username: {student.Username}");
        Console.WriteLine($"Total Courses Enrolled: {student.EnrolledCourseIds.Count}"); 
        Console.WriteLine($"Completed Courses: {student.GetCompletedCourses().Count}");
        Console.WriteLine($"Average Progress: {student.GetAverageProgress():F2}%");
        var completed = student.GetCompletedCourses();
        if (completed.Count > 0)
        { 
            Console.WriteLine("\nCompleted Courses:");
            foreach (int courseId in completed) 
            {
                Course course = courses.Find(c => c.CourseId == courseId); 
                if (course != null)
                { 
                    Console.WriteLine($" ✓ {course.Title}");
                } 
            } 
        }
    }
    static void AddInstructorCourse(Instructor instructor) 
    {
        DisplayAllCourses(); 
        Console.Write("\nEnter Course ID to add: "); 
        if (!int.TryParse(Console.ReadLine(), out int courseId))
        { 
            Console.WriteLine(" Invalid course ID."); 
            return; 
        }
        Course course = courses.Find(c => c.CourseId == courseId);
        if (course == null)
        {
            Console.WriteLine(" Course not found.");
            return;
        } 
        instructor.AddCourse(courseId);
    }
    static void ShowInstructorStudentCount(Instructor instructor) 
    { 
        int count = instructor.GetStudentCount(enrollments); 
        Console.WriteLine($"\nTotal students in your courses: {count}");
    }
    static void DisplayAllCourses() 
    { 
        Console.WriteLine("\n=== ALL COURSES ===");
        foreach (Course course in courses)
        { 
            Console.WriteLine($"\n[{course.CourseId}] {course.Title}");
            Console.WriteLine($"Category: {course.Category}");
            Console.WriteLine("---");
        } 
    }
    static void DeactivateUserAsAdmin(Admin admin)
    {
        admin.ViewAllUsers(users);
        Console.Write("\nEnter username to deactivate: ");
        string username = Console.ReadLine();
        User userToDeactivate = users.Find(u => u.Username == username);
        if (userToDeactivate == null)
        { 
            Console.WriteLine(" User not found."); 
            return; 
        }
        if (userToDeactivate == currentUser) 
        { 
            Console.WriteLine(" You cannot deactivate yourself!");
            return; 
        }
        admin.DeactivateUser(userToDeactivate);
    }
    static void BrowseAndEnrollCourses()
    {
        // Ensure sample data is loaded
        LoadSampleCourses();

        // Header with box-drawing characters
        Console.WriteLine("╔════════════════════════════════════════════════╗");
        Console.WriteLine("║                AVAILABLE COURSES               ║");
        Console.WriteLine("╚════════════════════════════════════════════════╝");

        // Determine current student (may be null if not logged in)
        var studentObj = users.Find(u => u.Username == currentUsername && u.Role == "Student") as Student;

        // Loop through courses and let each course display its own info
        foreach (var course in courses)
        {
            Console.WriteLine("==========================================");
            // Polymorphic display - each concrete course prints its format
            course.DisplayCourseInfo();

            // Show availability using polymorphic CanEnroll
            bool available = course.CanEnroll(studentObj);
            Console.WriteLine($"Status: {(available ? "✓ Available" : "✗ Full")}");
        }

        Console.WriteLine("==========================================");
        Console.Write("Enter the Course ID to enroll (or 0 to cancel): ");
        if (!int.TryParse(Console.ReadLine(), out int selectedId) || selectedId == 0)
        {
            Console.WriteLine("Cancelled or invalid input.");
            return;
        }

        var selectedCourse = courses.Find(c => c.CourseId == selectedId);
        if (selectedCourse == null)
        {
            Console.WriteLine("Course not found. Please enter a valid Course ID from the list.");
            return;
        }

        if (studentObj == null)
        {
            Console.WriteLine("You must be logged in as a student to enroll in courses. Please log in or register as a student to continue.");
            return;
        }

        // Check enrollment eligibility using polymorphic CanEnroll
        if (!selectedCourse.CanEnroll(studentObj))
        {
            Console.WriteLine("✗ Full or not eligible to enroll in this course.");
            return;
        }

        // Enroll via the IEnrollable implementation (polymorphic behavior)
        ((IEnrollable)selectedCourse).Enroll(studentObj);
        int enrollmentId = enrollments.Count + 1;
        Enrollment newEnrollment = new Enrollment(enrollmentId, studentObj.Username, selectedId);
        enrollments.Add(newEnrollment);

        // Notify instructor if they implement INotifiable
        var instructorNotify = users.Find(u => u.Username == selectedCourse.InstructorName) as INotifiable;
        if (instructorNotify != null)
        {
            instructorNotify.SendNotification($"Student {studentObj.Username} has enrolled in your course '{selectedCourse.Title}' (ID: {selectedId}).");
        }

        Console.WriteLine($"✓ Successfully enrolled in '{selectedCourse.Title}'!");
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

    public static void exit()
    {
        Console.WriteLine("Thx for using our application!!!");
        Environment.Exit(0);
    }
}