using AutoMapper;
using BookStoreApp.API.Data;
using BookStoreApp.API.ViewModels.AuthorViewModels;
using BookStoreApp.API.ViewModels.BooksViewModels;
using BookStoreApp.API.ViewModels.UserViewModels;

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

            CreateMap<ApiUser, UserCreateVM>()
                .ForMember(x => x.FirstName, y => y.MapFrom(z => z.FirstName))
                .ForMember(x => x.LastName, y => y.MapFrom(x => x.LastName))
                .ForMember(x => x.Email, y => y.MapFrom(x => x.Email))
                .ReverseMap();
        }
    }
}
