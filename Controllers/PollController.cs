﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PollAwards.Services;
using Microsoft.AspNetCore.Authorization;
using ArtistAwards;

namespace DotNetAPI
{
  [Route("api/[controller]")]
  [ApiController]
  public class PollController : ControllerBase
  {
    public PollController(PollService _pollService)
    {
      PollService = _pollService;
    }

    public PollService PollService;

    //[HttpGet]
    //public IEnumerable<Poll> GetPolls()
    //{
    //  return PollService.GetPolls();
    //  //string pollsJson = JsonSerializer.Serialize(PollService.GetPolls());
    //  //return pollsJson;
    //}

    [RouteAttribute("{id}")]
    [HttpGet]
    public async Task<Poll> GetPoll(Guid id)
    {
      Poll poll =  await PollService.GetPoll(id);
      return poll;
      //string pollsJson = JsonSerializer.Serialize(PollService.GetPolls());
      //return pollsJson;
    }

    [HttpPost, Authorize(Roles = "manager")]
    public async Task<Poll> CreatePoll([FromBody] Poll poll)
    {
      await PollService.CreatePoll(poll);

      return poll;
    }

    //[Route("vote")]
    //[HttpPost, Authorize(Roles = "voter")]
    //public async Task<StatusCodeResult> VoteAsync([FromBody] Poll poll)
    //{
    //  await PollService.VoteAsync(poll.Id);
    //  return Ok();

    //}
  }
}