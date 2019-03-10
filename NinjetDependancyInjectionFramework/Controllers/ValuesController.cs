using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloWorld.Interface;
using Microsoft.AspNetCore.Mvc;
using Ninject;

namespace NinjetDependancyInjectionFramework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IHelloWorld HelloWorld;
        public ValuesController(IHelloWorld _helloWorld)
        {
            HelloWorld = _helloWorld;
        }
        //// GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            // return "Not Working";
            return $"Ninject says {HelloWorld.SayHello()}";
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
