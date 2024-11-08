using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Abstractions.Entities;

[Table("Clients")]
public class ClientEntity
{
    public static ClientEntity Empty { get; }

    static ClientEntity()
    {
        Empty = new ClientEntity
        {
            Id = -1,
            Cards = ArraySegment<CardEntity>.Empty,
        };
    }

    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Column, StringLength(255)]
    public string? FirstName { get; set; }

    [Column, StringLength(255)]
    public string? Surname { get; set; }

    [Column, StringLength(255)]
    public string? Patronymic { get; set; }

    [InverseProperty(nameof(CardEntity.Client))]
    public virtual ICollection<CardEntity> Cards { get; set; } = new HashSet<CardEntity>();
}