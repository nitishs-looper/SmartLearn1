public class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public User(string username, string password, string email, string role)
    {
        Username = username;
        Password = password;
        Email = email;
        Role = role;
        isactive = true; // New accounts are active by default
        DateRegistered = DateTime.Now; // Set to current date/time
    }
    public void DisplayInfo()
    {
        Console.WriteLine($"Username: {Username}");
        Console.WriteLine($"Email: {Email}");
        Console.WriteLine($"Role: {Role}");
        Console.WriteLine("Account Status: " + (isactive ? "Active" : "Inactive"));
        Console.WriteLine("Date Registered: " + DateRegistered.ToString("g"));
    }
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
}