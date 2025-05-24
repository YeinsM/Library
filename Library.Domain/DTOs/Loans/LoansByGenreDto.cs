namespace Library.Domain.DTOs.Stats;

public class LoansByAuthorDto
{
    public int AuthorId { get; set; }
    public string AuthorName { get; set; } = null!;
    public int TotalLoans { get; set; }
}
