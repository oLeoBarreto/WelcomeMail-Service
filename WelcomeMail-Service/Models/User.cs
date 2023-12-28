namespace WelcomeMail_Service.Model;

[Serializable]
public class User(string email, string name)
{
    public string email { get; set; } = email;
    public string name { get; set; } = name;
}