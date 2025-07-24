using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;


[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly JwtToken _tokenService;
    private readonly IPassengerDetailService _passengerDetailService;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;

    public AuthController(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        JwtToken tokenService,
        IPassengerDetailService passengerDetailService,
        IMapper mapper,
        IEmailService emailService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _tokenService = tokenService;
        _passengerDetailService = passengerDetailService;
        _mapper = mapper;
        _emailService = emailService;
    }

    [HttpPost("register")]
public async Task<IActionResult> Register(RegisterDTO registerDTO)
{
    string role = "PASSENGER";

    if (await _userManager.FindByEmailAsync(registerDTO.Email) != null)
        return BadRequest("User already exists");

    var user = new ApplicationUser
    {
        UserName = registerDTO.Email,
        Email = registerDTO.Email,
        FullName = registerDTO.FullName
    };

    var result = await _userManager.CreateAsync(user, registerDTO.Password);
    if (!result.Succeeded)
        return BadRequest(result.Errors);

    if (!await _roleManager.RoleExistsAsync(role))
        await _roleManager.CreateAsync(new IdentityRole(role));

    await _userManager.AddToRoleAsync(user, role);

    var passengerDetail = new PassengerDetail
    {
        Name = registerDTO.FullName,
        Gender = registerDTO.Gender,
        Age = registerDTO.Age,
        Address = registerDTO.Address,
        ApplicationUserId = user.Id,
        ReservationId = null // ✅ Crucial to avoid FK constraint
    };

    var passengerDto = _mapper.Map<PassengerDetailDto>(passengerDetail);
    await _passengerDetailService.AddAsync(passengerDto);

    // ✉️ Send registration confirmation email
    string emailBody = $@"
        <h2>🎉 Registration Successful</h2>
        <p>Hello <strong>{registerDTO.FullName}</strong>,</p>
        <p>Your account has been successfully created.</p>
        <p><strong>Email:</strong> {registerDTO.Email}</p>
        <p><strong>Password:</strong> {registerDTO.Password}</p>
        <p>Login to your account and start booking your journey!</p>
        <br/>
        <p>Thank you,<br/>Railway Reservation Team <strong>Developed with ❤️ by Tushar Parashar</strong></p>";

    await _emailService.SendEmailAsync(registerDTO.Email, "Welcome to Railway Reservation", emailBody);


    return Ok(new { Success = true, Message = "Passenger registered successfully" });
}

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDTO loginDTO)
    {
        var user = await _userManager.FindByEmailAsync(loginDTO.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, loginDTO.Password))
            return Unauthorized("Invalid credentials");

        var token = await _tokenService.CreateTokenAsync(user);
        return Ok(token);
    }
}                                                                                                                                                                                                                                                                                                                                                                                                                                                              


































































































