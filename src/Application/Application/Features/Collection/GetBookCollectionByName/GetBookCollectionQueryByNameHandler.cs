
namespace Application.Features.Collection.GetBookCollectionByName;

public record GetBookCollectionQueryByNameResult(BookCollectionDto CollectionDto);
public record GetBookCollectionQueryByName(string name) : IQuery<GetBookCollectionQueryByNameResult>;

internal class GetBookCollectionQueryByNameHandler(ApplicationDBContext db, IMapper mapper) : IQueryHandler<GetBookCollectionQueryByName, GetBookCollectionQueryByNameResult>
{
    public async Task<GetBookCollectionQueryByNameResult> Handle(GetBookCollectionQueryByName query, CancellationToken cancellationToken)
    {
        var collection = await db.BookCollections.AsNoTracking().FirstOrDefaultAsync(b => b.CollectionName == query.name);

        if (collection == null) throw new NotFoundException($"Collection Not Found with name = {query.name}");

        return new GetBookCollectionQueryByNameResult(mapper.Map<BookCollectionDto>(collection));
    }
}
