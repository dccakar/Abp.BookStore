namespace Acme.BookStore
{
    public static class BookStoreDomainErrorCodes
    {
        /* You can add your business exception error codes here, as constants */
        public const string AuthorAlreadyExists = "BookStore:00001";
        public const string BookIsAlreadyLiked = "BookStore:00002";
        public const string AuthorAlreadyLiked = "BookStore:00003";
    }
}
