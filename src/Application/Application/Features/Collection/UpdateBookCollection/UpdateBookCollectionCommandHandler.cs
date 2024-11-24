namespace Application.Features.Collection.UpdateBookCollection;

public record UpdateBookCollectionResult(Guid Id);
public record UpdateBookCollectionCommand(BookCollectionDto BookCollection,Guid Id) : ICommand<UpdateBookCollectionResult>;

public class UpdateBookCollectionCommandValidators : AbstractValidator<UpdateBookCollectionCommand>
{
    public UpdateBookCollectionCommandValidators()
    {
        RuleFor(x => x.BookCollection.CollectionName)
            .NotEmpty()
            .NotNull()
            .WithMessage("Name of Collection can not be empty")
            .MaximumLength(100)
            .WithMessage("Name of Collection can not exceed than 100 characters");
    }
}



internal class UpdateBookCollectionCommandHandler(ApplicationDBContext db) : ICommandHandler<UpdateBookCollectionCommand, UpdateBookCollectionResult>
{
    public async Task<UpdateBookCollectionResult> Handle(UpdateBookCollectionCommand command, CancellationToken cancellationToken)
    {
        var collection = await db.BookCollections.AsNoTracking().FirstOrDefaultAsync(b => b.Id == command.Id);


        if (collection == null) throw new NotFoundException($"Collection Not Found with Id = {command.Id}");

        collection.CollectionName = command.BookCollection.CollectionName;
        db.BookCollections.Update(collection);
        await db.SaveChangesAsync();

        return new UpdateBookCollectionResult(collection.Id);
    }
}
