using Microsoft.AspNetCore.Mvc;
using TrickingLibrary.Data;
using TrickingLibrary.Models;

namespace TrickingLibrary.Api.Controllers;

[ApiController]
[Route("api/submissions")]
public class SubmissionsController : ControllerBase
{
    private readonly AppDbContext _ctx;

    public SubmissionsController(AppDbContext ctx)
    {
        _ctx = ctx;
    }

    // /api/submissions
    [HttpGet]
    public IEnumerable<Submission> All() => _ctx.Submissions.ToList();

    [HttpGet("{id:int}")]
    public Submission? Get(int id) => _ctx.Submissions.FirstOrDefault(x => x.Id.Equals(id));


    [HttpPost]
    public async Task<Submission> Create([FromBody] Submission submission)
    {
        _ctx.Submissions.Add(submission);
        await _ctx.SaveChangesAsync();
        return submission;
    }

    [HttpPut]
    public async Task<Submission> Update([FromBody] Submission submission)
    {
        if (submission.Id == 0) return submission;
        _ctx.Submissions.Update(submission);
        await _ctx.SaveChangesAsync();

        return submission;
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var submission = _ctx.Submissions.FirstOrDefault(x => x.Id.Equals(id));
        if (submission == null) return NotFound();
        submission.Deleted = true;
        await _ctx.SaveChangesAsync();

        return Ok();
    }
}