using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.CodeAnalysis;

namespace API.Entities;

public class Member
{
    public string Id { get; set; } = null!;
    public DateOnly DateOfBirth { get; set; }
    public string? ImageUrl { get; set; }
    public required string DisplayName { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime LastActive { get; set; } = DateTime.UtcNow;
    public required string Gender { get; set; }
    public string? Description { get; set; }
    public required string City { get; set; }
    public required string Country { get; set; }

    //navigation property
    [JsonIgnore]
    public List<Photo> Photos { get; set; } = [];

    [JsonIgnore]
    public List<MemberFollow> FollowedByMember { get; set; } = [];
    [JsonIgnore]
    public List<MemberFollow> FollowedMembers { get; set; } = [];
    
    [JsonIgnore]
    [ForeignKey(nameof(Id))]
    public User User { get; set; } = null!;

}
