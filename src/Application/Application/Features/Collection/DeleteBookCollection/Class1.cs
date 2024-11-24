using Shared.Exceptions;

namespace Application.Features.Collection.DeleteBookCollection;
public record DeleteBookCollectionQueryResult(bool deleted);
public record DeleteBookCollectionQuery(Guid Id) : IQuery<DeleteBookCollectionQueryResult>;

internal class DeleteBookCollectionQueryByIdHandler(ApplicationDBContext db) : IQueryHandler<DeleteBookCollectionQuery, DeleteBookCollectionQueryResult>
{
    public async Task<DeleteBookCollectionQueryResult> Handle(DeleteBookCollectionQuery query, CancellationToken cancellationToken)
    {
        var collection = await db.BookCollections.FirstOrDefaultAsync(b => b.Id == query.Id);

        if (collection == null) throw new NotFoundException($"Collection with Id = {query.Id} not Found.");

        db.BookCollections.Remove(collection);
        await db.SaveChangesAsync();

        return new DeleteBookCollectionQueryResult(true);
    }
}
