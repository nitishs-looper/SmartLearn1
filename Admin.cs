using SmartLearn1;
using System;
using System.Collections.Generic;
public class Admin : User
{
    public List<string> ManagedUsernames { get; set; }
    public Admin(string username, string password, string email)
        : base(username, password, email, "Admin")
    {
        ManagedUsernames = new List<string>();
    }
    public void AddUser(string username)
    {
        if (!ManagedUsernames.Contains(username))
        {
            ManagedUsernames.Add(username);
            Console.WriteLine("✓ User added successfully!");
        }
        else
        {
            Console.WriteLine("❌ User already exists!");
        }
    }
    public void RemoveUser(string username)
    {
        if (ManagedUsernames.Contains(username))
        {
            ManagedUsernames.Remove(username);
            Console.WriteLine("✓ User removed successfully!");
        }
        else
        {
            Console.WriteLine("❌ User not found!");
        }
    }
    public void ShowManagedUsers()
    {
        if (ManagedUsernames.Count == 0)
        {
            Console.WriteLine("No managed users.");
            return;
        }
        foreach (var username in ManagedUsernames)
        {
            Console.WriteLine($"Managed Username: {username}");
        }
    }
}
