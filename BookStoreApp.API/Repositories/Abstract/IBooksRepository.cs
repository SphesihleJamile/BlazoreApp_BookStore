using BookStoreApp.API.ViewModels.BooksViewModels;

namespace BookStoreApp.API.Repositories.Abstract
{
    public interface IBooksRepository
    {
        Task<IEnumerable<BookVM>?> GetBooksAsync();
        Task<BookVM?> GetBookAsync(int id);
        Task<BookVM?> CreateBookAsync(BookCreateVM bookCreate);
        Task<BookVM?> UpdateBookAsync(int id, BookUpdateVM bookUpdate);
        Task<bool> DeleteBookAsync(int id);
    }
}
