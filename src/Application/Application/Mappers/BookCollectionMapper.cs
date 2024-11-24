namespace Application.Mappers;

public class BookCollectionMapper:Profile
{
    public BookCollectionMapper()
    {
        CollectionMappers();
    }

    private void CollectionMappers()
    {
       CreateMap<BookCollection, BookCollectionDto>().ReverseMap();
    }
}
