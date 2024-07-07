using Bank.Application.Common.Mappings;
using Bank.Core.Models;

namespace Bank.Application.ViewModels;

public record UserViewModel : IMapWith<User>
{
    public required int UserId { get; set; }
    public required string Login { get; set; }
}