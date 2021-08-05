using System;
using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.FavouriteAuthors
{
    public class FavouriteAuthorDto : EntityDto<Guid>
    {
        public string AuthorName { get; set; }
        public Guid AuthorId { get; set; }
        public Guid? UserId { get; set; }

    }
}
