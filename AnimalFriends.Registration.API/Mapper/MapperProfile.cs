using AnimalFriends.Registration.API.Models;
using AutoMapper;


namespace AnimalFriends.Registration.API.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // Task mappings
            CreateMap<RegistrationDbo, RegistrationDto>(); // maps dbo(database entity) to dto (data transfer)
            CreateMap<RegistrationInput, RegistrationDbo>();

        }
    }
}
