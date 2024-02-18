using AutoMapper;
using imdb.Dto;
using imdb.Models;

public class MappingProfile : Profile {
    public MappingProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();
        CreateMap<Movie, MovieDto>();
        CreateMap<MovieDto, Movie>();
    }
}