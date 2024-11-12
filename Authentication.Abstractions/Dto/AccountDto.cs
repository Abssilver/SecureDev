namespace Authentication.Abstractions.Dto;

public class AccountDto
{
    public int Id { get; set; }
    public string Email { get; set; }
    public bool IsBlocked { get; set; }
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public string Patronymic { get; set; }
}