using System.Net;
using System.Net.Http.Headers;

namespace PropertyAgent.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyInterface _propertyInterface;
        private readonly IEmployeeInterface _employeeInterface;
        private readonly IPhotoService _photoService;
        private readonly IMapper _mapper;

        public PropertyController(IPropertyInterface propertyInterface,
                                  IEmployeeInterface employeeInterface,
                                  IPhotoService photoService,
                                  IMapper mapper)
        {
            _propertyInterface = propertyInterface;
            _employeeInterface = employeeInterface;
            _photoService = photoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PropertyDto>>> GetAll()
        {
            try
            {
                var properties = await _propertyInterface.GetProperties();
                return Ok(properties);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PropertyDto>> Get(int id)
        {
            try
            {
                var property = await _propertyInterface.GetProperty(id);
                return Ok(property);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPut]
        public async Task<ActionResult> Update(PropertyDto propertyDto)
        {
            try
            {
                var prop = new Property();
                var property = _mapper.Map(propertyDto, prop);
                await _propertyInterface.UpdateProperty(property);
                if (await _propertyInterface.Complete())
                 return NoContent();

                return BadRequest();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _propertyInterface.DeleteProperty(id);
                if (await _propertyInterface.Complete())
                    return NoContent();

                return BadRequest();
            }
            catch (Exception)
            {

                throw;
            }
        }



        [HttpPost]
        public async Task<ActionResult> AddProperty(PropertyDto propertyDto)
        {
            var user = await _employeeInterface.GetUser(User.FindFirst(c => c.Type.Contains("nameid"))?.Value);
            if (user == null)
            {
                return BadRequest("User not found");
            }
            var property = _mapper.Map<Property>(propertyDto);
            
             user.Properties.Add(await _propertyInterface.AddProperty(property));
            if(await _employeeInterface.Complete())
            return Ok(property);

            return BadRequest("Failed to add property");
        }

      
      

          
    }
}
