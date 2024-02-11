using AutoMapper;
using imdb.Dto;
using imdb.Models;

public class MappingProfile : Profile {
    public MappingProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<Movie, MovieDto>();
    }
}