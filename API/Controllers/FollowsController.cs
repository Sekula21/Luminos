using System;
using System.Runtime.CompilerServices;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class FollowsController(
    IFollowsRepository followsRepository
) : BaseApiController
{
    [HttpPost("{targetMemberId}")]
    public async Task<ActionResult> ToggleFollow(string targetMemberId)
    {
        var sourceMemberId = User.GetMemberId();

        if (sourceMemberId == targetMemberId) return BadRequest("You cannot follow yourself");

        var existingFollow = await followsRepository.GetMemberFollow(sourceMemberId, targetMemberId);

        if (existingFollow == null)
        {
            var follow = new MemberFollow
            {
                SourceMemberId = sourceMemberId,
                TargetMemberId = targetMemberId
            };
            followsRepository.AddFollow(follow);
        }
        else
        {
            followsRepository.DeleteFollow(existingFollow);
        }

        if (await followsRepository.SaveAllChanges()) return Ok();
        return BadRequest("Failed to update follow");
    }

    [HttpGet("list")]
    public async Task<ActionResult<IEnumerable<string>>> GetCurrentMemberFollowIds()
    {
        return Ok(await followsRepository.GetCurrentMemberFollowIds(User.GetMemberId()));
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Member>>> GetMemberFollows(string predicate)
    {
        var members = await followsRepository.GetMemberFollows(predicate, User.GetMemberId());
        return Ok(members);
    }
}
