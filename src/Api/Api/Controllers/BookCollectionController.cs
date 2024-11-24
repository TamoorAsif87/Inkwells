
using Application.Dtos;
using Application.Features.Collection.CreateBookCollection;
using Application.Features.Collection.DeleteBookCollection;
using Application.Features.Collection.GetBookCollection;
using Application.Features.Collection.GetBookCollectionById;
using Application.Features.Collection.GetBookCollectionByName;
using Application.Features.Collection.UpdateBookCollection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookCollectionController(ISender sender) : ControllerBase
    {
        // GET: api/<BookCollectionController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var result = await sender.Send(new GetBookCollectionQuery());
            return Ok(result);
        }

        // GET api/<BookCollectionController>/5
        [HttpGet("{id:guid}")]
        public async Task<ActionResult> Get(Guid id)
        {
            var result = await sender.Send(new GetBookCollectionQueryById(id));
            return Ok(result);
        }

        // GET api/<BookCollectionController>/5
        [HttpGet("{name}")]
        public async Task<ActionResult> Get(string name)
        {
            var result = await sender.Send(new GetBookCollectionQueryByName(name));
            return Ok(result);
        }



        // POST api/<BookCollectionController>
        [HttpPost]
        public async Task<ActionResult> Post(BookCollectionDto command)
        {
            var result = await sender.Send(new CreateBookCollectionCommand(command));
            return Created();
        }

        // PUT api/<BookCollectionController>/5
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Put(Guid id,BookCollectionDto command)
        {
            var result = await sender.Send(new UpdateBookCollectionCommand(BookCollection:command,Id:id));
            return Ok(result);
        }

        // DELETE api/<BookCollectionController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await sender.Send(new DeleteBookCollectionQuery(id));
            return Ok(result);
        }
    }
}
