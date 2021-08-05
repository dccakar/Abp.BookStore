using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.FavouriteBookss
{
    public interface IFavouriteBookAppService : ICrudAppService<
        FavouriteBookDto,
        Guid,
        PagedAndSortedResultRequestDto,
        FavouriteBookDto>
    {
        Task<bool> FindAsync(Guid id, string name);
    }
}
