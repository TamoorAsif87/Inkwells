namespace Application.Features.Collection.CreateBookCollection;

public record CreateBookCollectionResult(Guid Id);
public record CreateBookCollectionCommand(BookCollectionDto BookCollection):ICommand<CreateBookCollectionResult>;

public class CreateBookCollectionCommandValidators : AbstractValidator<CreateBookCollectionCommand>
{
    public CreateBookCollectionCommandValidators()
    {
        RuleFor(x => x.BookCollection.CollectionName)
            .NotEmpty()
            .WithMessage("Name of Collection can not be empty")
            .MaximumLength(100)
            .WithMessage("Name of Collection can not exceed than 100 characters");
    }
}



internal class CreateBookCollectionCommandHandler(ApplicationDBContext db,IMapper mapper) : ICommandHandler<CreateBookCollectionCommand, CreateBookCollectionResult>
{
    public async Task<CreateBookCollectionResult> Handle(CreateBookCollectionCommand command, CancellationToken cancellationToken)
    {
        command.BookCollection.Id = Guid.NewGuid();
        var collection = mapper.Map<BookCollection>(command.BookCollection);

        await db.BookCollections.AddAsync(collection);
        await db.SaveChangesAsync();

        return new CreateBookCollectionResult(collection.Id);
    }
}
