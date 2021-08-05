using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.FavouriteAuthors
{
    public interface IFavouriteAuthorRepository : IRepository<FavouriteAuthor, Guid>
    {
        /// <summary>
        /// gets author from FavouriteAuthor table with given inputs
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="authorName"></param>
        /// <returns>if exists returns FavouriteAuthor</returns>
        Task<FavouriteAuthor> FindByIdAsync(Guid? userId, string authorName);

        Task<List<FavouriteAuthor>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );
    }
}
