using DBMapping.Data;
using DBMapping.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace DBMapping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ApplicationDBContext _dbContext;
        public EmployeeController(IConfiguration configuration, ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
            _config = configuration;
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeRequest request)
        {
            var employee = new Employee
            {
                Name = request.Name,
                Id = Guid.NewGuid(),
        };
            var address = new Address
            {
                AddressId = employee.Id,
                City = request.City,
                State = request.State,
            };
            var department = new Department
            {
                DepartmentName = request.DepartmentName,
                DaprtmentId = employee.Id,
                DepartmentLoc = request.DepartmentLoc,
            };
            employee.Address = address;
            employee.Department = department;
            await _dbContext.Employee.AddAsync(employee);
            await _dbContext.SaveChangesAsync();
            return Ok(employee);
        }
        [HttpGet]
        public ActionResult GetEmployee()
        {

            //Eager Loading 
           var response= _dbContext.Employee.Include(s=>
               s.Address).Include(s=>s.Department);
            return Ok(response);    
        }
        public class EmployeeRequest
        {
            public String Name { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string DepartmentName { get; set; }
            public string DepartmentLoc { get; set; }
        }
    }
}
