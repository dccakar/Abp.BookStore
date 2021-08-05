using Acme.BookStore.Books;
using Acme.BookStore.FavouriteAuthors;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.FAvouriteAuthors
{
    public interface IFavouriteAuthorAppService : IApplicationService
    {
        Task<FavouriteAuthorDto> GetAsync(Guid id);
        Task<PagedResultDto<FavouriteAuthorDto>> GetListAsync(GetFavouriteAuthorListDto input);
        Task<FavouriteAuthorDto> CreateAsync(string name, Guid authorId, Guid? userId);
        Task DeleteAsync(Guid id);
        Task<ListResultDto<AuthorLookupDto>> GetAuthorLookupAsync();
    }
}