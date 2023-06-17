using BookStoreApp.API.ViewModels.AuthorViewModels;

namespace BookStoreApp.API.Repositories.Abstract
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<AuthorVM>?> GetAuthorsAsync();
        Task<AuthorVM?> GetAuthorAsync(int id);
        Task<AuthorVM> CreateAuthorAsync(AuthorCreateVM authorCreate);
        Task<AuthorVM?> UpdateAuthorAsync(int id, AuthorUpdateVM authorUpdate);
        Task<bool> DeleteAuthorAsync(int id);
    }
}
