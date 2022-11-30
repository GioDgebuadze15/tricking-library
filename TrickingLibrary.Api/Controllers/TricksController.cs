﻿using Microsoft.AspNetCore.Mvc;
using TrickingLibrary.Data;
using TrickingLibrary.Models;

namespace TrickingLibrary.Api.Controllers;

[ApiController]
[Route("api/tricks")]
public class TricksController : ControllerBase
{
    private readonly AppDbContext _ctx;

    public TricksController(AppDbContext ctx)
    {
        _ctx = ctx;
    }

    // /api/tricks
    [HttpGet]
    public IEnumerable<Trick> All() => _ctx.Tricks.ToList();

    // /api/tricks/{id}
    [HttpGet("{id}")]
    public Trick? Get(string id) =>
        _ctx.Tricks.FirstOrDefault(x => x.Id.Equals(id, StringComparison.InvariantCultureIgnoreCase));

    [HttpGet("{trickId}/submissions")]
    public IEnumerable<Submission> ListSubmissionsForTrick(string trickId) =>
        _ctx.Submissions.Where(x => x.TrickId.Equals(trickId, StringComparison.InvariantCultureIgnoreCase)).ToList();

    // /api/tricks
    [HttpPost]
    public async Task<Trick> Create([FromBody] Trick trick)
    {
        trick.Id = trick.Name.Replace(" ", "-").ToLowerInvariant();
        _ctx.Tricks.Add(trick);
        await _ctx.SaveChangesAsync();
        return trick;
    }

    // /api/tricks
    [HttpPut]
    public async Task<Trick> Update([FromBody] Trick trick)
    {
        if (string.IsNullOrEmpty(trick.Id)) return trick;
        _ctx.Tricks.Update(trick);
        await _ctx.SaveChangesAsync();

        return trick;
    }

    // /api/tricks/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var trick = _ctx.Tricks.FirstOrDefault(x => x.Id.Equals(id));
        if (trick == null) return Ok();
        trick.Deleted = true;
        await _ctx.SaveChangesAsync();

        return Ok();
    }
}