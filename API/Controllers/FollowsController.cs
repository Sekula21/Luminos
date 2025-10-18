using System;
using System.Runtime.CompilerServices;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class FollowsController(
    IUnitOfWork uOW
) : BaseApiController
{
    [HttpPost("{targetMemberId}")]
    public async Task<ActionResult> ToggleFollow(string targetMemberId)
    {
        var sourceMemberId = User.GetMemberId();

        if (sourceMemberId == targetMemberId) return BadRequest("You cannot follow yourself");

        var existingFollow = await uOW.FollowsRepository.GetMemberFollow(sourceMemberId, targetMemberId);

        if (existingFollow == null)
        {
            var follow = new MemberFollow
            {
                SourceMemberId = sourceMemberId,
                TargetMemberId = targetMemberId
            };
            uOW.FollowsRepository.AddFollow(follow);
        }
        else
        {
            uOW.FollowsRepository.DeleteFollow(existingFollow);
        }

        if (await uOW.Complete()) return Ok();
        return BadRequest("Failed to update follow");
    }

    [HttpGet("list")]
    public async Task<ActionResult<IEnumerable<string>>> GetCurrentMemberFollowIds()
    {
        return Ok(await uOW.FollowsRepository.GetCurrentMemberFollowIds(User.GetMemberId()));
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Member>>> GetMemberFollows(string predicate)
    {
        var members = await uOW.FollowsRepository.GetMemberFollows(predicate, User.GetMemberId());
        return Ok(members);
    }
}
