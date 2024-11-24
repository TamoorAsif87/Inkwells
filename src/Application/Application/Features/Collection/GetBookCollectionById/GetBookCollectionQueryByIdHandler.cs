namespace Application.Features.Collection.GetBookCollectionById;
public record GetBookCollectionQueryByIdResult(BookCollectionDto CollectionDto);
public record GetBookCollectionQueryById(Guid Id) : IQuery<GetBookCollectionQueryByIdResult>;

internal class GetBookCollectionQueryByIdHandler(ApplicationDBContext db, IMapper mapper): IQueryHandler<GetBookCollectionQueryById, GetBookCollectionQueryByIdResult>
{
    public async Task<GetBookCollectionQueryByIdResult> Handle(GetBookCollectionQueryById query, CancellationToken cancellationToken)
    {
        var collection = await db.BookCollections.AsNoTracking().FirstOrDefaultAsync(b => b.Id == query.Id);
        if (collection == null) throw new NotFoundException($"Collection Not Found with Id = {query.Id}");

        return new GetBookCollectionQueryByIdResult(mapper.Map<BookCollectionDto>(collection));
    }
}
