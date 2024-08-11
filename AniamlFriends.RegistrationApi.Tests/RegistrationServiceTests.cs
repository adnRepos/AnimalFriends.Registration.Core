using AnimalFriends.Registration.API.Services;
using Moq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using AnimalFriends.Registration.API.Data;
using AnimalFriends.Registration.API.Mapper;
using Xunit;
using AnimalFriends.Registration.API.Models;

namespace AniamlFriends.RegistrationApi.Tests
{
    public class RegistrationServiceTests
    {
        private static readonly Mock<ILogger<RegistrationService>> LoggerDefault = new(MockBehavior.Strict);
        private static readonly Mock<IRegistrationData> RegistrationDataDefault = new(MockBehavior.Strict);
        private static readonly Mock<IMapper> MapperDefault = new(MockBehavior.Strict);

        public IMapper Mapper { get; }

        public RegistrationServiceTests()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MapperProfile>()); // loads all the mapings
            Mapper = config.CreateMapper();
        }

        [Fact]
        public void TasksService_Exists()
        {
            var registrationService = new RegistrationService(LoggerDefault.Object, MapperDefault.Object, RegistrationDataDefault.Object);
            Assert.NotNull(registrationService);
        }

        [Fact]
        public void TasksService_GivenNullLogger_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new RegistrationService(null, MapperDefault.Object, RegistrationDataDefault.Object));
            Assert.Equal("Value cannot be null. (Parameter 'logger')", exception.Message);
        }

        [Theory]
        [InlineData(1, "Customer 1 FirstName", "Customer 1 LastName", "XX-123456", "john@doe.co.uk")]
        [InlineData(2, "Customer 2 FirstName", "Customer 2 LastName", "XX-123456", "john@doe.com")]
        public async Task Create_GivenInput_ReturnsExpectedResult(int customerId, string firstName, string lastName, string referenceNumber, string email)
        {
            var returnDbo = new RegistrationDbo
            {
                CustomerId = customerId,
                FirstName = firstName,
                LastName = lastName,
                ReferenceNumber = referenceNumber,
                Email = email
            };

            var registrationDataMock = new Mock<IRegistrationData>();
            registrationDataMock.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(returnDbo);
            registrationDataMock.Setup(x => x.Create(It.IsAny<RegistrationDbo>())).ReturnsAsync(customerId);

            var registrationService = new RegistrationService(LoggerDefault.Object, Mapper, registrationDataMock.Object);

            var cutomerCreated = await registrationService.Create(new RegistrationInput()
            {
                FirstName = firstName,
                LastName = lastName,
                ReferenceNumber = referenceNumber,
                Email = email
            });

            // verify task return is the one we create
            Assert.NotNull(cutomerCreated);

            // verify task Data is called once
            registrationDataMock.Verify(x => x.Get(customerId), Times.AtLeastOnce);

            // verify the return dalmock created matches the inputs
            Assert.Equal(cutomerCreated.CustomerId, customerId);
            Assert.Equal(cutomerCreated.FirstName, firstName);
            Assert.Equal(cutomerCreated.LastName, lastName);
            Assert.Equal(cutomerCreated.Email, email);

        }

        [Theory]
        [InlineData(1, "Customer 1 FirstName", "Customer 1 LastName", "XX-123456", "john@doe.co.uk")]
        [InlineData(2, "Customer 2 FirstName", "Customer 2 LastName", "XX-123456", "john@doe.com")]
        public async Task Get_GivenId_ReturnsExpectedResult(int customerId, string firstName, string lastName, string referenceNumber, string email)
        {
            var returnDbo = new RegistrationDbo
            {
                CustomerId = customerId,
                FirstName = firstName,
                LastName = lastName,
                ReferenceNumber = referenceNumber,
                Email = email
            };

            var registrationDataMock = new Mock<IRegistrationData>();
            registrationDataMock.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(returnDbo);

            var registrationService = new RegistrationService(LoggerDefault.Object, Mapper, registrationDataMock.Object);

            var customer = await registrationService.Get(customerId);

            // verify task return is not NULL
            Assert.NotNull(customer);

            // verify task Data is called once
            registrationDataMock.Verify(x => x.Get(customerId), Times.AtLeastOnce);

            // verify the return dalmock matches the inputs
            Assert.Equal(customer.CustomerId, customerId);
            Assert.Equal(customer.FirstName, firstName);
            Assert.Equal(customer.ReferenceNumber, referenceNumber);
            Assert.Equal(customer.Email, email);
        }

    }
}
