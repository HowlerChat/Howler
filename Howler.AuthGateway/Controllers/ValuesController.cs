using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Howler.Database;
using Howler.Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace Howler.AuthGateway.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        private IFederationDatabaseContext _federatedDb;
        
        public ValuesController(IFederationDatabaseContext federatedDb) {
            this._federatedDb = federatedDb;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return (this._federatedDb.Servers).Where(s => s.ServerId == "foo").Select(s => s.ServerId).ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
