using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Acme.BookStore.FavouriteBookss
{
    public class FavouriteBookManager : DomainService
    {
        private readonly IRepository<FavouriteBook, Guid> _favouriteBookRepository;
        public FavouriteBookManager(IRepository<FavouriteBook, Guid> favouriteBookRepository)
        {
            _favouriteBookRepository = favouriteBookRepository;
        }
    }
}
