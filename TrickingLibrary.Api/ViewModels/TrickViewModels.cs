﻿using System.Linq.Expressions;
using TrickingLibrary.Models;

namespace TrickingLibrary.Api.ViewModels;

public  static class TrickViewModels
{
    public static Expression<Func<Trick, object>> Default =>
        trick => new
        {
            trick.Id,
            trick.Name,
            trick.Description,
            trick.Difficulty,
            Categories = trick.TrickCategories.Select(x=>x.CategoryId),
            // Prerequisites = trick.Prerequisites.Select(x=>x.PrerequisiteId),
            // Progressions = trick.Progressions.Select(x=>x.ProgressionId),
        };
}