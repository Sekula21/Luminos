using System;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class FollowsRepository(
    AppDbContext context
) : IFollowsRepository
{
    public void AddFollow(MemberFollow follow)
    {
        context.Follows.Add(follow);
    }

    public void DeleteFollow(MemberFollow follow)
    {
        context.Follows.Remove(follow);
    }

    public async Task<IReadOnlyList<string>> GetCurrentMemberFollowIds(string memberId)
    {
        return await context.Follows
            .Where(x => x.SourceMemberId == memberId)
            .Select(x => x.TargetMemberId)
            .ToListAsync();
    }

    public async Task<MemberFollow?> GetMemberFollow(string sourceMemberId, string targetMemberId)
    {
        return await context.Follows.FindAsync(sourceMemberId, targetMemberId);
    }

    public async Task<IReadOnlyList<Member>> GetMemberFollows(string predicate, string memberId)
    {
        var query = context.Follows.AsQueryable();

        switch (predicate)
        {
            case "followed":
                return await query
                    .Where(x => x.SourceMemberId == memberId)
                    .Select(x => x.TargetMember)
                    .ToListAsync();
            case "followedBy":
                return await query
                    .Where(x => x.TargetMemberId == memberId)
                    .Select(x => x.SourceMember)
                    .ToListAsync();
            default: //mutal
                var followIds = await GetCurrentMemberFollowIds(memberId);

                return await query
                    .Where(x => x.TargetMemberId == memberId
                        && followIds.Contains(x.SourceMemberId))
                    .Select(x => x.SourceMember)
                    .ToListAsync();
        }
    }

}
