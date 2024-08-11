using AnimalFriends.Registration.API.Models;
using AnimalFriends.Registration.API.Services;
using Microsoft.AspNetCore.Mvc;

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
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(422)]
        public async Task<ActionResult<RegistrationDto>> Post([FromBody] RegistrationInput input)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            var resource = await _registrationService.Create(input);
            return Ok(resource);
        }

    }
}
