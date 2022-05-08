using System.ComponentModel.DataAnnotations;

namespace Domain.ModelClasses;

public class User
{
    [Key]
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }

    public override string ToString()
    {
        return $"Username: {UserName}, Password: {Password}, Id: {Id}";
    }

    public User() {}

    public User(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }
}