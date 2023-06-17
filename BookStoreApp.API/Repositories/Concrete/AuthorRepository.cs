using AutoMapper;
using BookStoreApp.API.Data;
using BookStoreApp.API.Repositories.Abstract;
using BookStoreApp.API.Validations.AuthorValidations;
using BookStoreApp.API.ViewModels.AuthorViewModels;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.API.Repositories.Concrete
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public AuthorRepository(BookStoreDbContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<AuthorVM>?> GetAuthorsAsync()
        {
            try
            {
                if (_dbContext.Authors == null)
                {
                    throw new Exception("Entity set 'BookStoreDbContext.Authors'  is null.");
                }

                var authors = await _dbContext.Authors.ToListAsync();

                if (authors == null || !authors.Any())
                    return null;

                var viewModel = _mapper.Map<List<AuthorVM>>(authors);
                return viewModel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AuthorVM?> GetAuthorAsync(int id)
        {
            try
            {
                if (_dbContext.Authors == null)
                {
                    throw new Exception("Entity set 'BookStoreDbContext.Authors'  is null.");
                }

                var author = await _dbContext.Authors.SingleOrDefaultAsync(x => x.Id == id);

                if (author == null)
                    return null;

                var viewModel = _mapper.Map<AuthorVM>(author);
                return viewModel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AuthorVM> CreateAuthorAsync(AuthorCreateVM authorCreate)
        {
            try
            {
                if (_dbContext.Authors == null)
                {
                    throw new Exception("Entity set 'BookStoreDbContext.Authors'  is null.");
                }

                var author = _mapper.Map<Author>(authorCreate);
                await _dbContext.Authors.AddAsync(author);
                await _dbContext.SaveChangesAsync();

                var newAuthor = _dbContext.Authors.OrderBy(x => x.Id).Last();

                var viewModel = _mapper.Map<AuthorVM>(newAuthor);
                return viewModel;

            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AuthorVM?> UpdateAuthorAsync(int id, AuthorUpdateVM authorUpdate)
        {
            try
            {
                if (_dbContext.Authors == null)
                {
                    throw new Exception("Entity set 'BookStoreDbContext.Authors'  is null.");
                }

                var author = await _dbContext.Authors.SingleOrDefaultAsync(x => x.Id == id);

                if (author == null)
                    return null;

                author.FirstName = authorUpdate.FirstName;
                author.LastName = authorUpdate.LastName;
                author.Bio = authorUpdate.Bio;

                await _dbContext.SaveChangesAsync();

                var viewModel = _mapper.Map<AuthorVM>(author);

                return viewModel;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAuthorAsync(int id)
        {
            try
            {
                if (_dbContext.Authors == null)
                {
                    throw new Exception("Entity set 'BookStoreDbContext.Authors'  is null.");
                }

                var author = await _dbContext.Authors.SingleOrDefaultAsync(x => x.Id == id);

                if (author == null)
                    return false;

                _dbContext.Authors.Remove(author);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
