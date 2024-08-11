using AnimalFriends.Registration.API.Data;
using AnimalFriends.Registration.API.Models;
using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Text.RegularExpressions;

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
        }

        public async Task<RegistrationDto> Create(RegistrationInput input)
        {
            ArgumentNullException.ThrowIfNull(input);

            ValidateInput(input);
           
            var dbo = _mapper.Map<RegistrationDbo>(input);

            var Id = await _data.Create(dbo);

            return await Get(Id);
        }

        private bool ValidateInput(RegistrationInput input)
        {
            if (string.IsNullOrWhiteSpace(input.FirstName)
                || !Regex.IsMatch(input.FirstName, RegistrationInput.FirstLastNameRegX))
            {
                throw new InvalidInputException<RegistrationInput>(input, nameof(input.FirstName));
            }
            if (string.IsNullOrWhiteSpace(input.LastName)
                || !Regex.IsMatch(input.LastName, RegistrationInput.FirstLastNameRegX))
            {
                throw new InvalidInputException<RegistrationInput>(input, nameof(input.LastName));
            }

            if (string.IsNullOrWhiteSpace(input.ReferenceNumber)
               || !Regex.IsMatch(input.ReferenceNumber, RegistrationInput.ReferenceNumberRegX))
            {
                throw new InvalidInputException<RegistrationInput>(input, nameof(input.ReferenceNumber));
            }

            if (string.IsNullOrWhiteSpace(input.Email) && !input.DateOfBirth.HasValue)
            {
                throw new InvalidInputException<RegistrationInput>(input, nameof(input.Email)+ nameof(input.DateOfBirth));
            }
            else
            {
                if (string.IsNullOrWhiteSpace(input.Email)
                     || !Regex.IsMatch(input.Email, RegistrationInput.EmailregX))
                {
                    throw new InvalidInputException<RegistrationInput>(input, nameof(input.Email));
                }

                if (input.DateOfBirth.HasValue)
                {
                    throw new InvalidInputException<RegistrationInput>(input, nameof(input.Email));
                }
            }

            return true;
        }

    }

    public class InvalidInputException<T> : InvalidOperationException
    {
        public InvalidInputException(T item, string propertyName) :
            base(@$"{typeof(T).Name} is not valid input :\r\n{JsonConvert.SerializeObject(item)}.
                  Property => {propertyName} value not Valid")
        {
        }
    }
}
