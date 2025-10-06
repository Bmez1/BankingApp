using Application.Abstractions.Messaging;

using SharedKernel;

using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Clientes;

internal sealed class ObtenerById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        //app.MapGet("todos/{id:guid}", async (
        //    Guid id,
        //    IQueryHandler<GetTodoByIdQuery, TodoResponse> handler,
        //    CancellationToken cancellationToken) =>
        //{
        //    var command = new GetTodoByIdQuery(id);

        //    Result<TodoResponse> result = await handler.Handle(command, cancellationToken);

        //    return result.Match(Results.Ok, CustomResults.Problem);
        //})
        //.WithTags(Tags.Todos)
        //.RequireAuthorization();
    }
}
