namespace BusinessLogic.Abstractions.Dto;

public class CardDto
{
    public Guid Id { get; set; }
    public string CardNumber { get; set; }
    public string? Name { get; set; }
    public string? CVV2 { get; set; }
    public string ExpDate { get; set; }
}