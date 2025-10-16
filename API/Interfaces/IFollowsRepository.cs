using System;
using API.Entities;

namespace API.Interfaces;

public interface IFollowsRepository
{
    Task<MemberFollow?> GetMemberFollow(string sourceMemberId, string targetMemberId);
    Task<IReadOnlyList<Member>> GetMemberFollows(string predicate, string memberId);
    Task<IReadOnlyList<string>> GetCurrentMemberFollowIds(string memberId);
    void DeleteFollow(MemberFollow follow);
    void AddFollow(MemberFollow follow);
    Task<bool> SaveAllChanges();
}
