using Acme.BookStore.Authors;
using Acme.BookStore.Books;
using Acme.BookStore.FAvouriteAuthors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;

namespace Acme.BookStore.FavouriteAuthors
{
    public class FavouriteAuthorAppService: BookStoreAppService, IFavouriteAuthorAppService
    {
        private readonly IFavouriteAuthorRepository _favouriteAuthorRepository;
        private readonly FavouriteAuthorManager _favouriteAuthorManager;
        private readonly IAuthorRepository _authorRepository;
        private readonly ICurrentUser _currentUser;

        public FavouriteAuthorAppService(
            IFavouriteAuthorRepository favouriteAuthorRepository,
            FavouriteAuthorManager favouriteAuthorManager,
            IAuthorRepository authorRepository,
            ICurrentUser currentUser)
        {
            _favouriteAuthorRepository = favouriteAuthorRepository;
            _favouriteAuthorManager = favouriteAuthorManager;
            _authorRepository = authorRepository;
            _currentUser = currentUser;
        }
        public async Task<FavouriteAuthorDto> GetAsync(Guid id)
        {
            var favouriteAuthor = await _favouriteAuthorRepository.GetAsync(id);
            return ObjectMapper.Map<FavouriteAuthor, FavouriteAuthorDto>(favouriteAuthor);
        }
        public async Task<PagedResultDto<FavouriteAuthorDto>> GetListAsync(GetFavouriteAuthorListDto input)
        {
            var queryable = await _favouriteAuthorRepository.GetQueryableAsync();
            
            var query = from favouriteAuthor in queryable
                        where favouriteAuthor.CreatorId == _currentUser.Id
                        select new { favouriteAuthor };

            query = query
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            var queryResult = await AsyncExecuter.ToListAsync(query);

            var authorDtos = queryResult.Select(x =>
            {
                var authorDto = ObjectMapper.Map<FavouriteAuthor, FavouriteAuthorDto>(x.favouriteAuthor);
                return authorDto;
            }).ToList();

            var totalCount = await _favouriteAuthorRepository.GetCountAsync();

            return new PagedResultDto<FavouriteAuthorDto>(
                totalCount,
                authorDtos
            );
        }
        public async Task<FavouriteAuthorDto> CreateAsync(string authorName, Guid authorId, Guid? userId)
        {
            var favouriteAuthor = await _favouriteAuthorManager.CreateAsync(
                authorName,
                authorId,
                userId
            );

            await _favouriteAuthorRepository.InsertAsync(favouriteAuthor);

            return ObjectMapper.Map<FavouriteAuthor, FavouriteAuthorDto>(favouriteAuthor);
        }
        public async Task DeleteAsync(Guid id)
        {
            await _favouriteAuthorRepository.DeleteAsync(id);
        }

        public async Task<ListResultDto<AuthorLookupDto>> GetAuthorLookupAsync()
        {
            var authors = await _authorRepository.GetListAsync();

            return new ListResultDto<AuthorLookupDto>(
                ObjectMapper.Map<List<Author>, List<AuthorLookupDto>>(authors)
            );
        }
    }
}
