using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Acme.BookStore.FavouriteAuthors
{
    public class AuthorIsAlreadyLikedException : BusinessException
    {
        public AuthorIsAlreadyLikedException(string name) : base(BookStoreDomainErrorCodes.AuthorAlreadyLiked)
        {
            WithData(name, "name");
        }
    }
}
