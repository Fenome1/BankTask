namespace Bank.Core.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<PersonalAccount> PersonalAccounts { get; set; } = new List<PersonalAccount>();

    public virtual RefreshToken? RefreshToken { get; set; }
}
