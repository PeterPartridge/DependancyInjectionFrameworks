using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.MicroKernel;
using Castle.Windsor;
using WindsorDependancyInjectionFrameworks.Class;
using WindsorDependancyInjectionFrameworks.Interface;
using Microsoft.AspNetCore.Mvc;

namespace WindsorDependancyInjectionFrameworks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IKernel _container;
        public ValuesController(IKernel container)
        {
            _container = container;
        }
        //// GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
           // return "Not Working";
            return _container.Resolve<IHelloWorld>().SayHello();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
