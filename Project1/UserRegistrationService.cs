using Project1;
using UserRegistration;

public class UserRegistrationService
{
    private readonly List<User> _registeredUsers;

    public UserRegistrationService()
    {
        _registeredUsers = new List<User>();
    }

    public RegistrationResult RegisterUser(User user) 
    {
        if (!CheckIsValidUsername(user.Username))
            return new RegistrationResult(false, null, "Oops! Invalid username.");

        if (!CheckIsValidPassword(user.Password))
            return new RegistrationResult(false, null, "Invalid password.");

        if (!CheckIsValidEmail(user.Email))
            return new RegistrationResult(false, null, "Invalid email format.");

        if (CheckIsUsernameTaken(user.Username))
            return new RegistrationResult(false, null, "This username is already in use.");

        _registeredUsers.Add(user);
        return new RegistrationResult(true, user.Username, "Registration successful.");
    }

    private bool CheckIsValidUsername(string username)
    {
        return username.Length >= 5 && username.Length <= 20 && username.All(char.IsLetterOrDigit);
    }

    public bool CheckIsValidPassword(string password)
    {
        return password.Length >= 8 && CheckIsCharacterSpecial(password);
    }

    public bool CheckIsValidEmail(string email)
    {
        if (email.EndsWith("@gmail.com"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CheckIsUsernameTaken(string username)
    {
        return _registeredUsers.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
    }

    private bool CheckIsCharacterSpecial(string password)
    {
        foreach (char c in password) // Checkar varje char i passwordargument som skickas in
        {
            if (!char.IsLetterOrDigit(c)) // Betyder om någon av char är en bokstav eller siffra.
            {
                return true; // Triggar en bool som returnerar true
            }
        }
        return false;
    }

}




