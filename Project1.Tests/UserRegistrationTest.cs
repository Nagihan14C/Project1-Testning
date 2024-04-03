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
    public void CheckIf_InputIsCorrect_Success() // Den h�r metoden testar att anv�ndaren har matat in r�tt uppgifter
    {
        // Arrange
        var user = new User("nagicifoglu", "password@123", "Nagi@gmail.com"); // H�r skapar vi en ny anv�ndare med en username, password och en mail adress

        // Act
        var result = _userService.RegisterUser(user);

        // Assert
        Assert.IsTrue(result.Success); // Kontrollerar att registreringen lyckades
        Assert.AreEqual("nagicifoglu", result.Username); // kontrollerar att anv�ndarnamnet matchade anv�ndarnamnet i "new user"
        Assert.AreEqual("Registration successful.", result.Message); // skriver ett meddelande om att registreringen lyckades
    }

    [TestMethod]
    public void CheckIfThereIsSameUsername_Failure() 
        // Den h�r metoden testar om det finns samma anv�ndarnamn tv� g�nger
    {
        // Arrange
        var user = new User("nagicifoglu", "password@123", "Nagi@gmail.com"); // skapar anv�ndare 1
        var user2 = new User("nagicifoglu", "password@123", "Nagi@gmail.com"); // skapar anv�ndare 2 med samma anv�ndarnamn

        // Act
        var result = _userService.RegisterUser(user); // h�r registerar vi anv�ndare 1
        var result2 = _userService.RegisterUser(user2); // h�r registerar vi anv�ndare 2

        // Assert
        Assert.IsTrue(result.Success); // Anv�ndare 1 ska lyckas
        Assert.IsFalse(result2.Success); // anv�ndare 2 ska misslyckas eftersom anv�ndarnamnet redan anv�nds
        Assert.AreEqual(result2.Message, "This username is already in use."); // Skickar meddelande f�r det andra av�ndaren som misslyckades

    }

    [TestMethod]
    public void CheckIfUsernameIsLong_MoreThan_20_Characters_Failureg() 
        // Den h�r metoden testar om Username �r �ver 20 tecken l�ng
    {
        // Arrange
        var user = new User("Nagihancifoglulonglong", "password@123", "Nagi@gmail.com"); // skapa new user med anv�ndarnamnet �ver 20 tecken l�ng

        // Act
        var result = _userService.RegisterUser(user);

        // Assert
        Assert.IsFalse(result.Success); // registrering ska misslyckas f�r att anv�ndarnamnet �r f�r l�ngt
        Assert.IsNull(result.Username);
        Assert.AreEqual("Oops! Invalid username.", result.Message); // ska skicka korrekt message f�r det ogiltiga anv�ndarnamnet
    }

    [TestMethod]
    public void CheckIfUsernameIsShort_Under_5_Characters_Failure()
    // Den h�r metoden testar om Username �r under 5 tecken kort

    {
        // Arrange
        var user = new User("Nagi", "password@123", "Nagi@gmail.com"); // skapa new user med anv�ndarnamnet under 5 tecken kort

        // Act
        var result = _userService.RegisterUser(user);

        // Assert
        Assert.IsFalse(result.Success); // registrering ska misslyckas f�r att anv�ndarnamnet �r f�r kort
        Assert.IsNull(result.Username);
        Assert.AreEqual("Oops! Invalid username.", result.Message); // ska skicka korrekt message f�r det ogiltiga anv�ndarnamnet
    }

    [TestMethod]
    public void CheckIfPasswordIsInvalid_Failure() 
        // Den h�r metoden testar om anv�ndaren har registrerat sig med ogiltig l�senord (l�senordet m�ste inneh�lla ett specialtecken)
    {
        // Arrange
        var user = new User("nagicifoglu", "password", "Nagi@gmail.com"); // skapa new user med ogiltig l�senord (m�ste innah�lla specialtecken)

        // Act
        var result = _userService.RegisterUser(user);

        // Assert
        Assert.IsFalse(result.Success); // kontrollera att det inte g�r att registrera pga ogiltig l�senord
        Assert.IsNull(result.Username);
        Assert.AreEqual("Invalid password.", result.Message); // se tidigare svar
        Assert.IsFalse(_userService.CheckIsValidPassword("password")); // kontrollera att l�senordet "password" �r ogiltig enligt en separat metod 
    }

    [TestMethod]
    public void CheckIfEmailFormatIsInvalid_Failure()
        //Metoden testar att anv�ndaren registrerar sig med en ogiltig e-mail format 
    {
        // Arrange
        var user = new User("nagicifoglu", "password@123", "Nagi@Cifoglu.com"); // skapar en new user med ogiltig email. Email m�ste sluta med @gmail.com

        // Act
        var result = _userService.RegisterUser(user); // registerar new user med en ogiltig email

        // Assert
        Assert.IsFalse(result.Success); // kontrollera att registrering misslyckas pga felaktig format p� email
        Assert.IsNull(result.Username);
        Assert.AreEqual("Invalid email format.", result.Message); // se tidigare svar
    }

   
}
