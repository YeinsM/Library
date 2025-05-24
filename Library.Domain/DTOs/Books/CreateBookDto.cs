namespace Library.Domain.DTOs.Books
{
    public record CreateBookDto
    {
        public string Title { get; set; }
        public int YearPublished { get; set; }
        public int AuthorId { get; set; }
        public string Genre { get; set; }

    }
}
