using AutoMapper;
using BookStoreApp.API.Data;
using BookStoreApp.API.Repositories.Abstract;
using BookStoreApp.API.ViewModels.BooksViewModels;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.API.Repositories.Concrete
{
    public class BooksRepository : IBooksRepository
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public BooksRepository(BookStoreDbContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        public async Task<BookVM?> CreateBookAsync(BookCreateVM bookCreate)
        {
            try
            {
                if (_dbContext.Books == null)
                    throw new Exception("Entity set 'BookStoreDbContext.Books' is null.");

                if (!DoesAuthorExist(bookCreate.AuthorId))
                    return null;

                if (IsISBNUnique(bookCreate.Isbn))
                    return null;

                var model = _mapper.Map<Book>(bookCreate);
                await _dbContext.Books.AddAsync(model);
                await _dbContext.SaveChangesAsync();

                var newBook = _dbContext.Books.OrderBy(x => x.Id).Last();
                var viewModel = _mapper.Map<BookVM>(newBook);
                return viewModel;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            try
            {
                if (_dbContext.Books == null)
                    throw new Exception("Entity set 'BookStoreDbContext.Books' is null.");

                var book = await _dbContext.Books.SingleOrDefaultAsync(x => x.Id == id);

                if (book == null)
                    return false;

                _dbContext.Books.Remove(book);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<BookVM?> GetBookAsync(int id)
        {
            try
            {
                if (_dbContext.Books == null)
                    throw new Exception("Entity set 'BookStoreDbContext.Books' is null.");

                var book = await _dbContext.Books.SingleOrDefaultAsync(x => x.Id == id);

                if (book == null)
                    return null;

                var viewModel = _mapper.Map<BookVM>(book);
                return viewModel;

            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<BookVM>?> GetBooksAsync()
        {
            try
            {
                if (_dbContext.Books == null)
                    throw new Exception("Entity set 'BookStoreDbContext.Books' is null.");

                var books = await _dbContext.Books.ToListAsync();

                if (books == null || !books.Any())
                    return null;

                var viewModel = _mapper.Map<List<BookVM>>(books);
                return viewModel;

            }catch
            {
                throw;
            }
        }

        public async Task<BookVM?> UpdateBookAsync(int id, BookUpdateVM bookUpdate)
        {
            try
            {
                if (_dbContext.Books == null)
                    throw new Exception("Entity set 'BookStoreDbContext.Books' is null.");

                var book = await _dbContext.Books.SingleOrDefaultAsync(x => x.Id == id);

                if (book == null)
                    return null;

                book.Title = bookUpdate.Title;
                book.Year = bookUpdate.Year;
                book.Summary = bookUpdate.Summary;
                book.Image = bookUpdate.Image;
                book.Price = bookUpdate.Price;

                await _dbContext.SaveChangesAsync();

                var viewModel = _mapper.Map<BookVM>(book);
                return viewModel;

            }
            catch
            {
                throw;
            }
        }

        private bool IsISBNUnique(string ISBN)
        {
            if (!_dbContext.Books.Any())
                return false;
            var result = _dbContext.Books.Any(x => x.Isbn != null && x.Isbn == ISBN);
            return result;
        }

        private bool DoesAuthorExist(int id)
        {
            var result = _dbContext.Authors.Any(x => x.Id == id);
            return result;
        }
    }
}
