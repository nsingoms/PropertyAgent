namespace PropertyAgent.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeInterface _employeeInterface;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;

        public EmployeeController(IEmployeeInterface employeeInterface, IMapper mapper,IPhotoService photoService)
        {
           _employeeInterface = employeeInterface;
           _mapper = mapper;
           _photoService = photoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> Get()
        {
            var employees = await _employeeInterface.GetEmployees();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> Get(string id)
        {
            var employee = await _employeeInterface.GetEmployee(id);
            return Ok(employee);
        }

        [HttpPut]
        public async Task<ActionResult> Put(EmployeeUpdateDto employee)
        {
            try
            {
                var user = await _employeeInterface.GetUser(User.FindFirst(c=>c.Type.Contains("nameid"))?.Value);

                _mapper.Map(employee, user);

                _employeeInterface.UpdateEmployee(user);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                  "Error Updating Data in the Database");
            }
        }
        [HttpPost("Add-Photo")]
        public async Task<ActionResult> AddPhoto([FromForm(Name ="image")]IFormFile formFile)
        {
            var user = await _employeeInterface.GetUser(User.FindFirst(c => c.Type.Contains("nameid"))?.Value);

                var result = await _photoService.AddPhotoAsync(formFile);

                if (result.Error != null)
                    return BadRequest(result.Error.Message);

                var photo = new Photo
                {
                    Url = result.SecureUrl.AbsoluteUri,
                    PublicId = result.PublicId
                };


            user.ProfilePic = photo.Url;
                if (await _employeeInterface.Complete())
                {
                    return CreatedAtRoute("GetUser", new
                    {
                        id =
                    user.Id
                    }, _mapper.Map<PhotoDto>(photo));
                }
                return BadRequest("Problem adding photo");
          
        }
    }
}
