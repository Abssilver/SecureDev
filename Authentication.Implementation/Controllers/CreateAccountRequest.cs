namespace Authentication.Implementation.Controllers;

public class CreateAccountRequest
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public string Patronymic { get; set; }
    public string Password { get; set; }
}