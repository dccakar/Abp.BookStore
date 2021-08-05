using Acme.BookStore.Books;
using System;
using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.FavouriteBookss
{
    public class FavouriteBookDto : AuditedEntityDto<Guid>
    {
        public string BookName { get; set; }

        public BookType Type { get; set; }
        public Guid BookId { get; set; }
        public Guid? UserId { get; set; }
    }
}
