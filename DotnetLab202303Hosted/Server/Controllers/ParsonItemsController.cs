using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DotnetLab202303Hosted.Server.Model;
using DotnetLab202303Hosted.Shared;
using Microsoft.Extensions.Logging;

namespace DotnetLab202303Hosted.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParsonItemsController : ControllerBase
    {
        private readonly Context _context;
        private readonly ILogger<ParsonItemsController> _logger;

        public ParsonItemsController(Context context, ILogger<ParsonItemsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/PersonItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Parson>>> GetPersonItems()
        {
            if (_context.PersonItems == null)
            {
                return NotFound();
            }
            List<Parson> parsons = new();
            var parsonItems = await _context.PersonItems.ToListAsync();
            foreach (var item in parsonItems)
            {
                Parson parson = new();
                parson.Id = item.Id;
                parson.Name = item.Name;
                parson.Birthday = item.Birthday;
                _logger.LogInformation(item.SkillId.ToString());
                var skills = await _context.SkillItems.Where(x => x.SkillId == item.SkillId).ToListAsync();
                foreach(var skill in skills)
                {
                    Skill skill1 = new();
                    skill1.SkillName = skill.SkillName;
                    skill1.Rare = skill.Rare;
                    parson.Skills.Add(skill1);
                }
                parsons.Add(parson);
            }
            return Ok(parsons);
        }

        // GET: api/PersonItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Parson>> GetPersonItem(long id)
        {
            if (_context.PersonItems == null)
            {
                return NotFound();
            }
            var personItem = await _context.PersonItems.FindAsync(id);

            if (personItem == null)
            {
                return NotFound();
            }
            Parson parson = new();
            parson.Id = personItem.Id;
            parson.Name = personItem.Name;
            parson.Birthday = personItem.Birthday;
            var skillItems = await _context.SkillItems.Where(x => x.SkillId == personItem.SkillId).ToListAsync();
            foreach (var skillItem in skillItems) 
            { 
                Skill skill = new();
                skill.SkillName = skillItem.SkillName;
                skill.Rare = skillItem.Rare;
                parson.Skills.Add(skill);
            }

            return parson;
        }

        // PUT: api/PersonItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutPersonItem(long id, ParsonItem personItem)
        //{
        //    if (id != personItem.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(personItem).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!PersonItemExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/PersonItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Parson>> PostPersonItem(Parson parson)
        {
            if (_context.PersonItems == null)
            {
                return Problem("Entity set 'Context.PersonItems'  is null.");
            }
            ParsonItem parsonItem = new();
            parsonItem.Name = parson.Name;
            parsonItem.Birthday = parson.Birthday;
            long skillId = _context.PersonItems.OrderByDescending(x => x.Id).Select(x => x.Id).ToList().FirstOrDefault();
            parsonItem.SkillId = skillId;
            var item = _context.PersonItems.Add(parsonItem);
            //await _context.SaveChangesAsync();
            //item.Entity.SkillId = item.Entity.Id;
            _logger.LogInformation(item.Entity.Id.ToString());
            _logger.LogInformation(parsonItem.Id.ToString());
            _logger.LogInformation("skillId" + skillId.ToString());
            foreach (var skill in parson.Skills)
            {
                SkillItem skillItem = new();
                skillItem.SkillId = skillId;
                skillItem.SkillName = skill.SkillName;
                skillItem.Rare = skill.Rare;
                _context.SkillItems.Add(skillItem);
            }


            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersonItem", new { id = parson.Id }, parson);
        }

        // DELETE: api/PersonItems/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeletePersonItem(long id)
        //{
        //    if (_context.PersonItems == null)
        //    {
        //        return NotFound();
        //    }
        //    var personItem = await _context.PersonItems.FindAsync(id);
        //    if (personItem == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.PersonItems.Remove(personItem);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool PersonItemExists(long id)
        //{
        //    return (_context.PersonItems?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
