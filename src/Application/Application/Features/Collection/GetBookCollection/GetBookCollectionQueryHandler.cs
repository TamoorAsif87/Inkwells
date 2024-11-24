namespace Application.Features.Collection.GetBookCollection;
public record GetBookCollectionQueryResult(List<BookCollectionDto> CollectionDto);
public record GetBookCollectionQuery():IQuery<GetBookCollectionQueryResult>;

internal class GetBookCollectionQueryHandler(ApplicationDBContext db,IMapper mapper) : IQueryHandler<GetBookCollectionQuery, GetBookCollectionQueryResult>
{
    public async Task<GetBookCollectionQueryResult> Handle(GetBookCollectionQuery request, CancellationToken cancellationToken)
    {
        var collections = await db.BookCollections.ToListAsync(cancellationToken);
        return new GetBookCollectionQueryResult(mapper.Map<List<BookCollectionDto>>(collections));  
    }
}
