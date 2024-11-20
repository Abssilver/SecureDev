namespace BusinessLogic.Abstractions.Dto;

public class ClientDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public string? Patronymic { get; set; }
}