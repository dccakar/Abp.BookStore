using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acme.BookStore.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.Authors
{
    [Authorize(BookStorePermissions.Authors.Default)]
    public class AuthorAppService : BookStoreAppService, IAuthorAppService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly AuthorManager _authorManager;

        public AuthorAppService(
            IAuthorRepository authorRepository,
            AuthorManager authorManager)
        {
            _authorRepository = authorRepository;
            _authorManager = authorManager;
        }

        //...SERVICE METHODS WILL COME HERE...
        public async Task<AuthorDto> GetAsync(Guid id)
        {
            var author = await _authorRepository.GetAsync(id);
            return ObjectMapper.Map<Author, AuthorDto>(author);
        }

        public async Task<PagedResultDto<AuthorDto>> GetListAsync(GetAuthorListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Author.Name);
            }

            var authors = await _authorRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );
            var totalCount = input.Filter == null
                ? await _authorRepository.CountAsync()
                : await _authorRepository.CountAsync(
                    author => author.Name.Contains(input.Filter));

            return new PagedResultDto<AuthorDto>(
                totalCount,
                ObjectMapper.Map<List<Author>, List<AuthorDto>>(authors)
            );
        }

        [Authorize(BookStorePermissions.Authors.Create)]
        public async Task<AuthorDto> CreateAsync(CreateAuthorDto input)
        {
            var author = await _authorManager.CreateAsync(
                input.Name,
                input.BirthDate,
                input.ShortBio
            );

            await _authorRepository.InsertAsync(author);

            return ObjectMapper.Map<Author, AuthorDto>(author);
        }

        [Authorize(BookStorePermissions.Authors.Edit)]
        public async Task UpdateAsync(Guid id, UpdateAuthorDto input)
        {
            var author = await _authorRepository.GetAsync(id);

            if (author.Name != input.Name)
            {
                await _authorManager.ChangeNameAsync(author, input.Name);
            }

            author.BirthDate = input.BirthDate;
            author.ShortBio = input.ShortBio;
            await _authorRepository.UpdateAsync(author);
        }

        [Authorize(BookStorePermissions.Authors.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _authorRepository.DeleteAsync(id);
        }
        public async Task<PagedResultDto<AuthorDto>> GetFavouriteListAsync(PagedAndSortedResultRequestDto input)
        {
            var queryable = await _authorRepository.GetQueryableAsync();

            //Prepare a query to select liked books     
            var query = from author in queryable
                        where author.IsFavourite
                        select new { author };

            //Paging
            query = query
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            //Execute the query and get a list
            var queryResult = await AsyncExecuter.ToListAsync(query);

            //Convert the query result to a list of BookDto objects
            var authorDtos = queryResult.Select(x =>
            {
                var authorDto = ObjectMapper.Map<Author, AuthorDto>(x.author);
                return authorDto;
            }).ToList();

            //Get the total count with another query
            var totalCount = await _authorRepository.GetCountAsync();

            return new PagedResultDto<AuthorDto>(
                totalCount,
                authorDtos
            );
        }

        [Authorize(BookStorePermissions.Authors.Liked)]
        public async Task LikeAsync(Guid id)
        {
            var author = await _authorRepository.GetAsync(id);
            author.IsFavourite = true;
            await _authorRepository.UpdateAsync(author);
        }

        [Authorize(BookStorePermissions.Authors.Liked)]
        public async Task DislikeAsync(Guid id)
        {
            var author = await _authorRepository.GetAsync(id);
            author.IsFavourite = false;
            await _authorRepository.UpdateAsync(author);
        }
    }
}
