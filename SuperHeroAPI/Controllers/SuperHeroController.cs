using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private static List<SuperHero> heroes = new List<SuperHero>
            {
                new SuperHero {
                    Id = 1,
                    Name = "Superman",
                    FirstName = "Mark",
                    LastName = "Pate",
                    Place = "New York City"
                },
                new SuperHero {
                    Id = 2,
                    Name = "Batman",
                    FirstName = "Mark",
                    LastName = "Pate",
                    Place = "New York City"
                }
            };
        private readonly DataContext dataContext;

        public SuperHeroController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {

            return Ok(await dataContext.SuperHeroes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            SuperHero hero = await dataContext.SuperHeroes.FindAsync(id);
            return Ok(hero);
        }


        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {

            heroes.Add(hero);
            return Ok(heroes);
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var hero = heroes.Find(h => h.Id == request.Id);
            if (hero == null)
                return BadRequest("Hero not found");

            hero.Name = request.Name;
            hero.FirstName = request.FirstName;
            hero.LastName = request.LastName;
            hero.Place = request.Place;

            return Ok(heroes);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Delete(int id) {

            var hero = heroes.Find(h => h.Id == id);
            if (hero == null)
                return BadRequest("Hero not found");

            heroes.Remove(hero);
            return Ok(hero);
        }

    }
}
