using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Abstractions.Entities;

[Table("Cards")]
public class CardEntity
{
    public static CardEntity Empty { get; }

    static CardEntity()
    {
        Empty = new CardEntity
        {
            Id = Guid.Empty,
            ClientId = -1,
            Client = ClientEntity.Empty,
        };
    }
    
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [ForeignKey(nameof(Client))]
    public int ClientId { get; set; }

    [Column, StringLength(20)]
    public string CardNumber { get; set; }

    [Column, StringLength(50)]
    public string? Name { get; set; }

    [Column, StringLength(50)]
    public string? CVV2 { get; set; }

    [Column]
    public DateTime ExpDate { get; set; }

    public virtual ClientEntity Client { get; set; }
}