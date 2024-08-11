using AnimalFriends.Registration.API.Data;
using AnimalFriends.Registration.API.Models;
using AutoMapper;
using System;

namespace AnimalFriends.Registration.API.Services
{
    public interface IRegistrationService
    {
        Task<RegistrationDto> Get(int id);
        Task<RegistrationDto> Create(RegistrationInput input);
    }
    public class RegistrationService : IRegistrationService
    {

        private ILogger<RegistrationService> _logger;
        private IRegistrationData _data;
        private IMapper _mapper;


        public RegistrationService(ILogger<RegistrationService> logger, IMapper mapper, IRegistrationData data)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _data = data ?? throw new ArgumentNullException(nameof(data));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<RegistrationDto> Get(int id)
        {
          throw new NotImplementedException();
        }

        public async Task<RegistrationDto> Create(RegistrationInput input)
        {
            throw new NotImplementedException();
        }
    }
}
