using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Abstractions.Entities;

[Table("AccountSessions")]
public class AccountSessionEntity
{
    public static AccountSessionEntity Empty { get; }

    static AccountSessionEntity()
    {
        Empty = new AccountSessionEntity
        {
            Id = -1,
            Account = AccountEntity.Empty,
        };
    }
    
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(384)]
    public string SessionToken { get; set; }

    [ForeignKey(nameof(Account))]
    public int AccountId { get; set; }

    [Column(TypeName = "datetime2")]
    public DateTime TimeCreated { get; set; }

    [Column(TypeName = "datetime2")]
    public DateTime TimeLastRequest { get; set; }

    public bool IsClosed { get; set; }

    [Column(TypeName = "datetime2")]
    public DateTime? TimeClosed { get; set; }
    public virtual AccountEntity Account { get; set; }
}