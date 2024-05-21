namespace DarkkestP3.API.DTO;

public record struct NewProfile(int userId, string interests, string skills, string missionStatement);
public record struct UpdateProfile(int userId, string updatedInterests, string updatedSkills, string updatedMissionStatement);
public record struct PatchProfileInterests(int userId, string updatedInterests);
public record struct PatchProfileSkills(int userId, string updatedSkills);
public record struct PatchProfileMissionStatement(int userId, string updatedMissionStatement);
