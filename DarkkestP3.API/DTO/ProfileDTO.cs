namespace DarkkestP3.API.DTO;

public record struct NewProfile(string userId, string interests, string skills, string missionStatement);
public record struct UpdateProfile(string userId, string updatedInterests, string updatedSkills, string updatedMissionStatement);
public record struct PatchProfileInterests(string userId, string updatedInterests);
public record struct PatchProfileSkills(string userId, string updatedSkills);
public record struct PatchProfileMissionStatement(string userId, string updatedMissionStatement);
