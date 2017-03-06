using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using XamarinBackendService.DataObjects;
using XamarinBackendService.Models;
using XamarinBackendService.Helpers;

namespace XamarinBackendService.Controllers
{
    public class CharacterController : TableController<Character>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            XamarinBackendContext context = new XamarinBackendContext();
            DomainManager = new EntityDomainManager<Character>(context, Request);
        }

        // GET tables/Character
        [QueryableExpand("Weapons,Appearances")]
        public IQueryable<Character> GetAllCharacters()
        {
            return Query();
        }

        // GET tables/Character/48D68C86-6EA6-4C25-AA33-223FC9A27959
        [QueryableExpand("Weapons,Appearances")]
        public SingleResult<Character> GetCharacter(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Character/48D68C86-6EA6-4C25-AA33-223FC9A27959
        //public Task<Character> PatchCharacter(string id, Delta<Character> patch)
        //{
        //    return UpdateAsync(id, patch);
        //}

        // POST tables/Character
        //public async Task<IHttpActionResult> PostCharacter(Character item)
        //{
        //    Character current = await InsertAsync(item);
        //    return CreatedAtRoute("Tables", new { id = current.Id }, current);
        //}

        // DELETE tables/Character/48D68C86-6EA6-4C25-AA33-223FC9A27959
        //public Task DeleteCharacter(string id)
        //{
        //    return DeleteAsync(id);
        //}
    }
}