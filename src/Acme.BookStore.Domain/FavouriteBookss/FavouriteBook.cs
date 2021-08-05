using Acme.BookStore.Books;
using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.BookStore.FavouriteBookss
{
    public class FavouriteBook : AuditedAggregateRoot<Guid>
    {
        public string BookName { get; set; }
        public BookType Type { get; set; }
        public Guid BookId { get; set; }
        public Guid? UserId { get; set; }
    }
}
