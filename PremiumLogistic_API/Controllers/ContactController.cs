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
        await _contactService.AddContact(addContactDto);
        return Created(nameof(Add), "Contact added successfully");
    }
}
