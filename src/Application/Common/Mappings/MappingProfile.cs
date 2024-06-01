using Application.UseCases.Transactions.Commands.CreateTransaction;
using Application.UseCases.Transactions.Commands.DeleteTransaction;
using Application.UseCases.Transactions.Commands.UpdateTransaction;
using Application.UseCases.Transactions.Queries.GetTransaction;

namespace Application.Common.Mappings;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        this.CreateMap<GetTransactionQueryModel, GetTransactionQuery>();
        this.CreateMap<CreateTransactionCommandModel, CreateTransactionCommand>();
        this.CreateMap<UpdateTransactionCommandModel, UpdateTransactionCommand>();
        this.CreateMap<RemoveTransactionCommandModel, RemoveTransactionCommand>();
    }
}
