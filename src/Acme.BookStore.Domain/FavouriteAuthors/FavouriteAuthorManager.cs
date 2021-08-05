using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.Users;

namespace Acme.BookStore.FavouriteAuthors
{
    public class FavouriteAuthorManager : DomainService
    {
        private readonly IFavouriteAuthorRepository _favouriteAuthorRepository;
        private readonly ICurrentUser _currentUser;

        public FavouriteAuthorManager(IFavouriteAuthorRepository favouriteAuthorRepository, ICurrentUser currentUser)
        {
            _favouriteAuthorRepository = favouriteAuthorRepository;
            _currentUser = currentUser;
        }

        public async Task<FavouriteAuthor> CreateAsync(
            [NotNull] string authorName, 
            Guid authorId,
            Guid? userId
            )
        {
            Check.NotNullOrWhiteSpace(authorName, nameof(authorName));

            var existingAuthor = await _favouriteAuthorRepository.FindByIdAsync(userId, authorName);
            if (existingAuthor != null && existingAuthor.AuthorName == authorName && existingAuthor.UserId == userId)
            {
                throw new AuthorIsAlreadyLikedException(authorName);
            }

            return new FavouriteAuthor(
                GuidGenerator.Create(),
                authorName,
                authorId,
                userId
            );
        }
    }
}
