namespace Library.Domain.DTOs.Stats;

public class LoansByGenreDto
{
    public string Genre { get; set; } = null!;
    public int TotalLoans { get; set; }
}
