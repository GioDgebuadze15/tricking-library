using Microsoft.AspNetCore.Mvc;
using TrickingLibrary.Data;
using TrickingLibrary.Models;

namespace TrickingLibrary.Api.Controllers;

[ApiController]
[Route("api/difficulties")]
public class DifficultyController : ControllerBase
{
    private readonly AppDbContext _ctx;

    public DifficultyController(AppDbContext ctx)
    {
        _ctx = ctx;
    }

    // /api/tricks
    [HttpGet]
    public IEnumerable<Difficulty> All() => _ctx.Difficulties.ToList();

    // /api/tricks/{id}
    [HttpGet("{id}")]
    public Difficulty? Get(string id) =>
        _ctx.Difficulties
            .FirstOrDefault(x => x.Id.Equals(id, StringComparison.InvariantCultureIgnoreCase));

    [HttpGet("{id}/tricks")]
    public IEnumerable<Trick> ListDifficultyTricks(string id) => // todo change check fot null
        _ctx.Tricks
            .Where(x => x.Difficulty != null && x.Difficulty.Equals(id, StringComparison.InvariantCultureIgnoreCase))
            .ToList();

    // /api/tricks
    [HttpPost]
    public async Task<Difficulty> Create([FromBody] Difficulty difficulty)
    {
        difficulty.Id = difficulty.Name.Replace(" ", "-").ToLowerInvariant();
        _ctx.Difficulties.Add(difficulty);
        await _ctx.SaveChangesAsync();
        return difficulty;
    }
}