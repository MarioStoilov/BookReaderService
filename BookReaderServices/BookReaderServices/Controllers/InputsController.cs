using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookReaderServices.Controllers
{
    public class InputsController : ApiController
    {
        // GET api/inputs
        public IEnumerable<string> Get()
        {
            var context = new InputsModel.db680e9f024324438c8485a239007e3497Entities();
            var latestInputs = context.Inputs.OrderByDescending(x => x.Id).Take(10).Select(x=>x.input).ToList();
            return latestInputs;
        }

     
        public void Post([FromBody]string value)
        {
            var newInput = new InputsModel.Inputs();
            newInput.input = value;

            var context = new InputsModel.db680e9f024324438c8485a239007e3497Entities();
            context.Inputs.Add(newInput);
            context.SaveChanges();
        }
     
    }
}
