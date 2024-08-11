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
        [InlineData(1, "Customer 1 FirstName", "Customer 1 LastName", "XX-123456","john@doe.co.uk")]
        [InlineData(2, "Customer 2 FirstName", "Customer 2 LastName", "XX-123456", "john@doe.com")]
        public async Task Create_GivenInput_ReturnsExpectedResult(int customerId, string firstName, string lastName, string referenceNumber,string email)
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

        //[Theory]
        //[InlineData(1, "Task 1", "Task 1 Descr", false, 0)]
        //[InlineData(1, "Task 2", "Task 2 Descr", false, 20)]
        //[InlineData(1, "Task 3", "Task 3 Descr", true, 10)]
        //[InlineData(1, "Task 4", "Task 4 Descr", true, 20)]
        //public async Task GetByTaskId_GivenId_ReturnsExpectedResult(int taskId, string name, string description, bool isComplete, int subTasks)
        //{
        //    var returnDbo = new TaskDbo
        //    {
        //        Id = taskId,
        //        Name = name,
        //        Description = description,
        //        IsComplete = isComplete,
        //        NoOfSubTasks = subTasks
        //    };

        //    var taskDataMock = new Mock<ITaskData>();
        //    taskDataMock.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(returnDbo);

        //    var tasksService = new TasksService(LoggerDefault.Object, Mapper, taskDataMock.Object, SubTaksDataDefault.Object);

        //    var task = await tasksService.GetTaskById(taskId);

        //    // verify task return is not NULL
        //    Assert.NotNull(task);

        //    // verify task Data is called once
        //    taskDataMock.Verify(x => x.Get(taskId), Times.AtLeastOnce);

        //    // verify the return dalmock matches the inputs
        //    Assert.Equal(task.Id, taskId);
        //    Assert.Equal(task.Name, name);
        //    Assert.Equal(task.Description, description);
        //    Assert.Equal(task.NoOfSubTasks, subTasks);
        //}

    }
}
