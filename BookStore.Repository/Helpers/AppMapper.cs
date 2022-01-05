using System;
using AutoMapper;
using BookStore.Data.Models;
using BookStore.Repository.Dtos;

namespace BookStore.Repository.Helpers
{
    public class AppMapper : Profile 
    {
        public AppMapper()
        {
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<Book, AddBookDto>().ReverseMap();
            CreateMap<User, SignUpDto>().ReverseMap()
                .ForMember(d => d.FName, source => source.MapFrom(s => s.FirstName))
                .ForMember(d => d.LName, source => source.MapFrom(s => s.LastName))
                .ForMember(d => d.UserName, source => source.MapFrom(s => s.Email));


        }
    }
}
