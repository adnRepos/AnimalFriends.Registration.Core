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
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var dbo = await _data.Get(id);
            return _mapper.Map<RegistrationDto>(dbo);

            throw new NotImplementedException();
        }

        public async Task<RegistrationDto> Create(RegistrationInput input)
        {
            ArgumentNullException.ThrowIfNull(input);

            var dbo = _mapper.Map<RegistrationDbo>(input);

            var Id = await _data.Create(dbo);

            return await Get(Id);
        }

        private bool ValidateInput(RegistrationInput input)
        {
            //List<string> errors = new();
            //if(string.IsNullOrWhiteSpace(input.FirstName) || input.FirstName.Length < 3)
            //{
            //    errors.a
            //}


            return true;
        }




    }
}
