﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrickingLibrary.Data;
using TrickingLibrary.Models;

namespace TrickingLibrary.Api.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoryController : ControllerBase
{
    private readonly AppDbContext _ctx;

    public CategoryController(AppDbContext ctx)
    {
        _ctx = ctx;
    }

    // /api/tricks
    [HttpGet]
    public IEnumerable<Category> All() => _ctx.Categories.ToList();

    // /api/tricks/{id}
    [HttpGet("{id}")]
    public Category? Get(string id) =>
        _ctx.Categories
            .FirstOrDefault(x => x.Id.Equals(id, StringComparison.InvariantCultureIgnoreCase));

    [HttpGet("{id}/tricks")]
    public IEnumerable<Trick> ListCategoryTricks(string id) =>
        _ctx.TrickCategories
            .Include(x => x.Trick)
            .Where(x => x.CategoryId.Equals(id, StringComparison.InvariantCultureIgnoreCase))
            .Select(x => x.Trick)
            .ToList();

    // /api/tricks
    [HttpPost]
    public async Task<Category> Create([FromBody] Category category)
    {
        category.Id = category.Name.Replace(" ", "-").ToLowerInvariant();
        _ctx.Categories.Add(category);
        await _ctx.SaveChangesAsync();
        return category;
    }
}