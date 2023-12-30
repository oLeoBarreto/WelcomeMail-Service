using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WelcomeMail_Service.Model;

[Serializable]
public partial class User
{
    public User(string email, string name)
    {
        if (ValidateEmailFormat(email) == false)
        {
            throw new FormatException("This is not a valid email format!");
        }
        
        Email = email;
        Name = name;
    }

    [EmailAddress]
    public string Email { get; set; } 
    public string Name { get; set; }

    private bool ValidateEmailFormat(string emailAddress)
    {
        return MyRegex().IsMatch(emailAddress);
    }

    [GeneratedRegex(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")]
    private partial Regex MyRegex();
}