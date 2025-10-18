using System;

namespace API.Interfaces;

public interface IUnitOfWork
{
    IMemberRepository MemberRepository { get; }
    IMessageRepository MessageRepository { get; }
    IFollowsRepository FollowsRepository { get; }
    Task<bool> Complete();
    bool HasChanges();
}
