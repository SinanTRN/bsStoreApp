namespace Entities.Exceptions
{
    public class BookNotFound : NotFound
    {
        public BookNotFound(int id) : base($"The book with id:{id} could not found")
        {
        }
    }
}
