using AutoMapper;
using Common.Models;
using Library.DataLayer.Entities;

namespace Library.BusinessLayer.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<BorrowEntity, BorrowModel>()
                .ForMember(dest => dest.BorrowPublicID, opt => opt.MapFrom(src => src.BorrowPublicID))
                .ForMember(dest => dest.UserPublicID, opt => opt.MapFrom(src => src.User.UserPublicID))
                .ForMember(dest => dest.BookPublicID, opt => opt.MapFrom(src => src.Book.BookPublicID))
                .ForMember(dest => dest.Book, opt => opt.MapFrom(src => src.Book))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));

            CreateMap<BorrowModel, BorrowEntity>()
                .ForMember(dest => dest.BorrowID, opt => opt.Ignore())
                .ForMember(dest => dest.BookID, opt => opt.Ignore())
                .ForMember(dest => dest.UserID, opt => opt.Ignore())
                .ForMember(dest => dest.BorrowPublicID, opt => opt.MapFrom(src => src.BorrowPublicID));

            CreateMap<BookEntity, BookModel>();
            CreateMap<BookModel, BookEntity>();
            CreateMap<UserEntity, UserModel>();
            CreateMap<UserModel, UserEntity>();
        }
    }
}
