using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Acme.BookStore.FavouriteBookss;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;

namespace Acme.BookStore.FavouriteBooks
{
    public class FavouriteBookAppService : CrudAppService<
            FavouriteBook, 
            FavouriteBookDto, 
            Guid, 
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            FavouriteBookDto>, 
        IFavouriteBookAppService 
    {
        private readonly ICurrentUser _currentUser;
        public FavouriteBookAppService(IRepository<FavouriteBook, Guid> repository, ICurrentUser currentUser)
            : base(repository)
        {
            _currentUser = currentUser;
        }
        public async Task<bool> FindAsync(Guid id, string name)
        {
            var queryable = await Repository.GetQueryableAsync();

            var query = from favouriteBook in queryable
                        where favouriteBook.BookId == id && favouriteBook.UserId == _currentUser.Id
                        select new { favouriteBook };

            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                return false;
            }
            else
            {
                return true;
            }
           

        }
        public override async Task<PagedResultDto<FavouriteBookDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            //Get the IQueryable<Book> from the repository
            var queryable = await Repository.GetQueryableAsync();
            var userId = _currentUser.Id;
            //Prepare a query to join books and authors
            var query = from book in queryable
                        where book.UserId == userId
                        select new { book };

            //Paging
            query = query
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            //Execute the query and get a list
            var queryResult = await AsyncExecuter.ToListAsync(query);

            //Convert the query result to a list of BookDto objects
            var bookDtos = queryResult.Select(x =>
            {
                var favouriteBookDto = ObjectMapper.Map<FavouriteBook, FavouriteBookDto>(x.book);
                favouriteBookDto.UserId = userId;
                return favouriteBookDto;
            }).ToList();

            //Get the total count with another query
            var totalCount = await Repository.GetCountAsync();

            return new PagedResultDto<FavouriteBookDto>(
                totalCount,
                bookDtos
            );
        }
    }
}
