namespace Web.Api.Endpoints.Clientes;

internal sealed class Obtener : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        //app.MapGet("todos", async (
        //    Guid userId,
        //    IQueryHandler<GetTodosQuery, List<TodoResponse>> handler,
        //    CancellationToken cancellationToken) =>
        //{
        //    var query = new GetTodosQuery(userId);

        //    Result<List<TodoResponse>> result = await handler.Handle(query, cancellationToken);

        //    return result.Match(Results.Ok, CustomResults.Problem);
        //})
        //.WithTags(Tags.Todos)
        //.RequireAuthorization();
    }
}
