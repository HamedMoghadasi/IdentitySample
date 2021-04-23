using Authorization.Filters;
using Authorization.Constants;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Authorization.Filters.Security;
using Authorizations.Constants;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IdentitySample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ISecurity _security;
        public ValuesController(ISecurity security)
        {
            _security = security;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            var result = new List<string> { "value1", "value2" };
            if (await _security.IsGrantedAsync(RolesValue.Admin))
            {
                result = new List<string> { "admin" };
            }
            if (await _security.IsGrantedAsync(GlobalClaimsType.Permission, "ValuesController.foo")) {
                result = new List<string> { "value1", "value2", "value3", "value4", "value5", "value6" };
            }

            return result;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
