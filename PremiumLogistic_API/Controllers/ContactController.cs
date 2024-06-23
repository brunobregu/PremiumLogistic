namespace PremiumLogistic_API.Controllers;

[ApiController]
[Route("api/v{v:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class ContactController : ControllerBase
{
    private readonly IContactService _contactService;
    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }

    [HttpPost("addContact")]
    public async Task<IActionResult> Add([FromBody] AddContactDto addContactDto)
    {
        await _contactService.AddContact(addContactDto);
        return Created(nameof(Add), "Contact added successfully");
    }
}
