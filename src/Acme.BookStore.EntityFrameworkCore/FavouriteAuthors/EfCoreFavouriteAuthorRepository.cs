using Acme.BookStore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Acme.BookStore.FavouriteAuthors
{
    public class EfCoreFavouriteAuthorRepository
       : EfCoreRepository<BookStoreDbContext, FavouriteAuthor, Guid>,
           IFavouriteAuthorRepository
    {
        public EfCoreFavouriteAuthorRepository(
            IDbContextProvider<BookStoreDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<FavouriteAuthor> FindByIdAsync(Guid? userId, string authorName)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(favouriteAuthor => favouriteAuthor.UserId == userId && favouriteAuthor.AuthorName == authorName);
        }

        public async Task<List<FavouriteAuthor>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    favouriteAuthor => favouriteAuthor.AuthorName.Contains(filter)
                 )
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}
