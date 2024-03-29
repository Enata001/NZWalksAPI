﻿using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories.Interface;

public interface IWalkRepository
{
    Task<Walk> CreateAsync(Walk walk);
    Task<List<Walk>> GetAll(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true,  int pageNumber = 1, int pageSize = 100);
    Task<Walk?> GetById(Guid id);

    Task<Walk?> Update(Guid id, Walk walk);
    Task<Walk?> Remove(Guid id);
}