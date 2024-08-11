using AnimalFriends.Registration.API.Models;
using AnimalFriends.Registration.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AnimalFriends.Registration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private ILogger<RegistrationController> _logger;
        private IRegistrationService _registrationService;

        public RegistrationController(ILogger<RegistrationController> logger, IRegistrationService regService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _registrationService = regService ?? throw new ArgumentNullException(nameof(regService));
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<RegistrationDto>> Get(int id)
        {
            _logger.LogInformation("{@controller} - {@method} about to get task => {@id}", nameof(RegistrationController), nameof(Get), id);

            var resource = await _registrationService.Get(id);
            if (resource == null)
            {
                return NotFound($"Task Id with Id => {id} not found");
            }

            return Ok(resource);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(422)]
        public async Task<ActionResult<RegistrationDto>> Post([FromBody] RegistrationInput input)
        {
            _logger.LogInformation("{@controller} - {@method} about to Post  => {@input}",
                                   nameof(RegistrationController), nameof(Post), JsonSerializer.Serialize(input));

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            var resource = await _registrationService.Create(input);
            return Ok(resource);
        }

    }
}
