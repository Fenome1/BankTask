using AutoMapper;
using Bank.Application.Common.Mappings;
using Bank.Core.Models;
using MediatR;

namespace Bank.Application.Features.Accounts.Commands.Create;

public sealed record CreateAccountCommand : IRequest<int>, IMapWith<Account>
{
    public required int UserId { get; set; }
    public required int CurrencyId { get; set; }
    public string? Name { set; get; }

    public void Map(Profile profile)
    {
        profile.CreateMap<CreateAccountCommand, Account>
            ().ForMember(dest => dest.OwnerId, opt =>
            opt.MapFrom(src => src.UserId));
    }
}