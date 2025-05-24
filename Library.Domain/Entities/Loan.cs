namespace Library.Domain.Entities
{
    public class Loan
    {
        public int LoanId { get; set; }
        public int BookId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime? DueDate { get; set; }

        public Book Book { get; set; }
    }
}
