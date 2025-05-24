namespace Library.Domain.DTOs.Books
{
    public class BookWithLoanCountDto
    {
        public int BookId { get; set; }
        public string Title { get; set; } = null!;
        public int TotalLoans { get; set; }
    }
}
