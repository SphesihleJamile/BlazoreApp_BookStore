using AutoMapper;
using BookStoreApp.API.Data;
using BookStoreApp.API.ViewModels.AuthorViewModels;
using BookStoreApp.API.ViewModels.BooksViewModels;

namespace BookStoreApp.API.Configurations
{
    public class MapConfig : Profile
    {
        public MapConfig()
        {
            CreateMap<Author, AuthorCreateVM>()
                .ReverseMap();
            CreateMap<Author, AuthorVM>();

            CreateMap<Book, BookCreateVM>()
                .ReverseMap();
            CreateMap<Book, BookVM>();
        }
    }
}
