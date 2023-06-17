using AutoMapper;
using BookStoreApp.API.Data;
using BookStoreApp.API.ViewModels.AuthorViewModels;

namespace BookStoreApp.API.Configurations
{
    public class MapConfig : Profile
    {
        public MapConfig()
        {
            CreateMap<Author, AuthorCreateVM>()
                .ReverseMap();
            CreateMap<Author, AuthorVM>();
        }
    }
}
