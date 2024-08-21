namespace PremiumLogistic_API.Controllers;

[ApiController]
[Route("api/v{v:apiVersion}/{culture:culture}/[controller]")]
[ApiVersion("1.0")]
public class ContactController : ControllerBase
{
    private readonly IContactService _contactService;
    private readonly IStringLocalizer<Resource> _localizer;
    public ContactController(IContactService contactService, IStringLocalizer<Resource> localizer)
    {
        _contactService = contactService;
        _localizer = localizer;
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] AddContactDto addContactDto)
    {
        await _contactService.Add(addContactDto);
        return Created(nameof(Add), _localizer["ContactAdded"].Value);
    }

    [HttpGet("contacts")]
    [Authorize(Roles = "Admin, Account Manager")]
    public async Task<IActionResult> All()
    {
        var result = await _contactService.All();
        return Ok(result);
    }
}
