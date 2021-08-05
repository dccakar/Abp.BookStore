using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.BookStore.FavouriteAuthors
{
    public class FavouriteAuthor : FullAuditedAggregateRoot<Guid>
    {
        public string AuthorName { get; set; }
        public Guid AuthorId { get; set; }
        public Guid? UserId { get; set; }
        public FavouriteAuthor()
        {

        }
        public FavouriteAuthor(
            Guid id,
            [NotNull] string authorName,
            Guid authorId,
            Guid? userId
            ) : base(id)
        {
            SetName(authorName);
            AuthorId = authorId;
            UserId = userId;
        }
        private void SetName([NotNull] string name)
        {
            AuthorName = name;
        }
    }
}
