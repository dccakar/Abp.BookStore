using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.FavouriteAuthors
{
    public class GetFavouriteAuthorListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
