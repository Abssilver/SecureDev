using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Abstractions.Entities;

[Table("Accounts")]
public class AccountEntity
{
    public static AccountEntity Empty { get; }

    static AccountEntity()
    {
        Empty = new AccountEntity
        {
            Id = -1,
            Sessions = ArraySegment<AccountSessionEntity>.Empty,
        };
    }
    
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [StringLength(255)]
    public string Email { get; set; }

    [StringLength(100)]
    public string PasswordSalt { get; set; }

    [StringLength(100)]
    public string PasswordHash { get; set; }

    public bool IsBlocked { get; set; }

    [StringLength(255)]
    public string FirstName { get; set; }

    [StringLength(255)]
    public string Surname { get; set; }

    [StringLength(255)]
    public string Patronymic { get; set; }

    [InverseProperty(nameof(AccountSessionEntity.Account))]
    public virtual ICollection<AccountSessionEntity> Sessions { get; set; } = new HashSet<AccountSessionEntity>();

}