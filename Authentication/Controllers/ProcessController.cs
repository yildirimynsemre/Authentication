using Authentication.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Authentication.Controllers
{
    [Authorize]
    public class ProcessController : ApiController
    {
        private List<Material> materials = new List<Material>();
        public ProcessController()
        {
            materials = new List<Material>
            {
                new Material{MaterialId=1001,Title="CO2",Quantity=34.50},
                new Material{MaterialId=1002,Title="H2SO4",Quantity=42.50},
                new Material{MaterialId=1003,Title="NiO3",Quantity=24.50},
                new Material{MaterialId=1004,Title="U",Quantity=15.50},
            };
        }
        // GET api/values
        public List<Material> GetMaterial()
        
        {
            var materialList = materials;
            return materialList;
        }

        // GET api/values/5
        public Material GetSpecificMaterial(int id)
        {
            var material = materials.Where(m => m.MaterialId == id).FirstOrDefault();
            if (material != null)
                return material;
            else
                throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
