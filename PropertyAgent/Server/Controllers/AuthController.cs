using AutoMapper;

namespace PropertyAgent.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
 
        public AuthController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager
                              ,ITokenService tokenService)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._tokenService = tokenService;
            
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (registerDto == null || !ModelState.IsValid)
            { 
                return BadRequest();
            }

            if (await UserExist(registerDto.Email.ToLower()))
            {
                return BadRequest("User already has an Account");
            }

            var user = new AppUser
            {
                Email = registerDto.Email.ToLower(),
                UserName = registerDto.Email.ToLower()
            };
          
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
           
            return new UserDto
            {
                Username = registerDto.Email,
                Token =  _tokenService.CreateToken(user),
               
            };
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.Users
                            .FirstOrDefaultAsync(x => x.Email == loginDto.Email.ToLower());

            if (user == null) return Unauthorized("Invalid User");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password,false);

            if (result.Succeeded == false) return Unauthorized();

              return new UserDto
            {
                Id = user.Id,
                Username = user.UserName,
                Token = _tokenService.CreateToken(user),
                
                KnownAs = user.KnownAs,
                Gender = user.Gender,
            };

        }
        private async Task<bool> UserExist(string email)
        {
            return await _userManager.Users.AnyAsync(x => x.Email == email);
        }
    }
}
