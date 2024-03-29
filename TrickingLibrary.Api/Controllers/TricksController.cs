﻿using Microsoft.AspNetCore.Mvc;
using TrickingLibrary.Api.Form;
using TrickingLibrary.Api.ViewModels;
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
    public IEnumerable<object> All() => _ctx.Tricks.Select(TrickViewModels.Default).ToList();

    // /api/tricks/{id}
    [HttpGet("{id}")]
    public object Get(string id) =>
        _ctx.Tricks
            .Where(x => x.Id.Equals(id, StringComparison.InvariantCultureIgnoreCase))
            .Select(TrickViewModels.Default)
            .FirstOrDefault();

    [HttpGet("{trickId}/submissions")]
    public IEnumerable<Submission> ListSubmissionsForTrick(string trickId) =>
        _ctx.Submissions.Where(x => x.TrickId.Equals(trickId, StringComparison.InvariantCultureIgnoreCase)).ToList();

    // /api/tricks
    [HttpPost]
    public async Task<object> Create([FromBody] TrickForm trickForm)
    {
        var trick = new Trick
        {
            Id = trickForm.Name.Replace(" ", "-").ToLowerInvariant(),
            Name = trickForm.Name,
            Description = trickForm.Description,
            Difficulty = trickForm.Difficulty,
            // Prerequisites = trickForm.Prerequisites.Select(x=>new TrickRelationship
            // {
            //     PrerequisiteId = x,
            // }),
            // Progressions = trickForm.Prerequisites.Select(x=>new TrickRelationship
            // {
            //     PrerequisiteId = x,
            // }),
            TrickCategories = trickForm.Categories.Select(x => new TrickCategory
            {
                CategoryId = x
            }).ToList()
        };
        _ctx.Tricks.Add(trick);
        await _ctx.SaveChangesAsync();
        return TrickViewModels.Default.Compile().Invoke(trick);
    }

    // /api/tricks
    [HttpPut]
    public async Task<object> Update([FromBody] Trick trick)
    {
        if (string.IsNullOrEmpty(trick.Id)) return trick;
        _ctx.Tricks.Update(trick);
        await _ctx.SaveChangesAsync();

        return TrickViewModels.Default.Compile().Invoke(trick);
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