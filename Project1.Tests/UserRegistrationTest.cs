using Project1;

[TestClass]
public class UserRegistrationServiceTests
{
    private UserRegistrationService _userService;

    [TestInitialize]
    public void Initialize()
    {
        _userService = new UserRegistrationService();
    }

    [TestMethod]
    public void CheckIf_InputIsCorrect_Success() // Den här metoden testar att användaren har matat in rätt uppgifter
    {
        // Arrange
        var user = new User("nagicifoglu", "password@123", "Nagi@gmail.com"); // Här skapar vi en ny användare med en username, password och en mail adress

        // Act
        var result = _userService.RegisterUser(user);

        // Assert
        Assert.IsTrue(result.Success); // Kontrollerar att registreringen lyckades
        Assert.AreEqual("nagicifoglu", result.Username); // kontrollerar att användarnamnet matchade användarnamnet i "new user"
        Assert.AreEqual("Registration successful.", result.Message); // skriver ett meddelande om att registreringen lyckades
    }

    [TestMethod]
    public void CheckIfThereIsSameUsername_Failure() 
        // Den här metoden testar om det finns samma användarnamn två gånger
    {
        // Arrange
        var user = new User("nagicifoglu", "password@123", "Nagi@gmail.com"); // skapar användare 1
        var user2 = new User("nagicifoglu", "password@123", "Nagi@gmail.com"); // skapar användare 2 med samma användarnamn

        // Act
        var result = _userService.RegisterUser(user); // här registerar vi användare 1
        var result2 = _userService.RegisterUser(user2); // här registerar vi användare 2

        // Assert
        Assert.IsTrue(result.Success); // Användare 1 ska lyckas
        Assert.IsFalse(result2.Success); // användare 2 ska misslyckas eftersom användarnamnet redan används
        Assert.AreEqual(result2.Message, "This username is already in use."); // Skickar meddelande för det andra avändaren som misslyckades

    }

    [TestMethod]
    public void CheckIfUsernameIsLong_MoreThan_20_Characters_Failureg() 
        // Den här metoden testar om Username är över 20 tecken lång
    {
        // Arrange
        var user = new User("Nagihancifoglulonglong", "password@123", "Nagi@gmail.com"); // skapa new user med användarnamnet över 20 tecken lång

        // Act
        var result = _userService.RegisterUser(user);

        // Assert
        Assert.IsFalse(result.Success); // registrering ska misslyckas för att användarnamnet är för långt
        Assert.IsNull(result.Username);
        Assert.AreEqual("Oops! Invalid username.", result.Message); // ska skicka korrekt message för det ogiltiga användarnamnet
    }

    [TestMethod]
    public void CheckIfUsernameIsShort_Under_5_Characters_Failure()
    // Den här metoden testar om Username är under 5 tecken kort

    {
        // Arrange
        var user = new User("Nagi", "password@123", "Nagi@gmail.com"); // skapa new user med användarnamnet under 5 tecken kort

        // Act
        var result = _userService.RegisterUser(user);

        // Assert
        Assert.IsFalse(result.Success); // registrering ska misslyckas för att användarnamnet är för kort
        Assert.IsNull(result.Username);
        Assert.AreEqual("Oops! Invalid username.", result.Message); // ska skicka korrekt message för det ogiltiga användarnamnet
    }

    [TestMethod]
    public void CheckIfPasswordIsInvalid_Failure() 
        // Den här metoden testar om användaren har registrerat sig med ogiltig lösenord (lösenordet måste innehålla ett specialtecken)
    {
        // Arrange
        var user = new User("nagicifoglu", "password", "Nagi@gmail.com"); // skapa new user med ogiltig lösenord (måste innahålla specialtecken)

        // Act
        var result = _userService.RegisterUser(user);

        // Assert
        Assert.IsFalse(result.Success); // kontrollera att det inte går att registrera pga ogiltig lösenord
        Assert.IsNull(result.Username);
        Assert.AreEqual("Invalid password.", result.Message); // se tidigare svar
        Assert.IsFalse(_userService.CheckIsValidPassword("password")); // kontrollera att lösenordet "password" är ogiltig enligt en separat metod 
    }

    [TestMethod]
    public void CheckIfEmailFormatIsInvalid_Failure()
        //Metoden testar att användaren registrerar sig med en ogiltig e-mail format 
    {
        // Arrange
        var user = new User("nagicifoglu", "password@123", "Nagi@Cifoglu.com"); // skapar en new user med ogiltig email. Email måste sluta med @gmail.com

        // Act
        var result = _userService.RegisterUser(user); // registerar new user med en ogiltig email

        // Assert
        Assert.IsFalse(result.Success); // kontrollera att registrering misslyckas pga felaktig format på email
        Assert.IsNull(result.Username);
        Assert.AreEqual("Invalid email format.", result.Message); // se tidigare svar
    }

   
}
