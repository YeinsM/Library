﻿namespace Library.Domain.Entities
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string Nationality { get; set; }

        public ICollection<Book> Books { get; set; } = [];
    }
}
