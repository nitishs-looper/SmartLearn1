using System;
using System.Linq;

namespace SmartLearn1;
public abstract class User
{
    public string Username { get; set; }
    private string _password;
    public string Password
    {
        get => _password;
        set
        {
            if (value == null || value.Length < 8)
            {
                Console.WriteLine("✗ Password must be at least 8 characters");
                return;
            }
            if (!value.Any(char.IsDigit))
            {
                Console.WriteLine("✗ Password must contain at least 1 number");
                return;
            }
            _password = value;
        }
    }
    private string _email;
    public string Email
    {
        get => _email;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                Console.WriteLine("✗ Invalid email - must contain @");
                return;
            }
            if (!value.Contains("@"))
            {
                Console.WriteLine("✗ Invalid email - must contain @");
                return;
            }
            _email = value;
        }
    }
    public string Role { get; set; }
    protected User(string username, string password, string email, string role)
    {
        Username = username;
        Password = password;
        Email = email;
        Role = role;
        isactive = true; 
        DateRegistered = DateTime.Now;
    }
    public virtual void DisplayInfo()
    {
        Console.WriteLine($"Username: {Username}");
        Console.WriteLine($"Email: {Email}");
        Console.WriteLine("Account Status: " + (isactive ? "Active" : "Inactive"));
        Console.WriteLine("Date Registered: " + DateRegistered.ToString("g"));
    }
    public virtual void DisplayInfoWithRole()
    {
        Console.WriteLine($"Username: {Username}");
        Console.WriteLine($"Email: {Email}");
        Console.WriteLine($"Role: {Role}");
        Console.WriteLine("Account Status: " + (isactive ? "Active" : "Inactive"));
        Console.WriteLine("Date Registered: " + DateRegistered.ToString("g"));
    }
    public abstract void DisplayDashboard();
    public abstract string GetUserType();
    public bool ValidatePassword(string inputPassword)
    {
        return Password == inputPassword;
    }
    public void changePassword(string newPassword)
    {
        Password = newPassword;
        Console.WriteLine("Password changed successfully.");
    }
    bool isactive = true;
    public DateTimeOffset DateRegistered { get; set; } = DateTimeOffset.UtcNow;
   public void DeactivateAccount()
    {
        isactive = false;
        Console.WriteLine("Account deactivated.");
    }
    public void ActivateAccount()
    {
        isactive = true;
        Console.WriteLine("Account activated.");
    }
    public bool IsActive => isactive;
}