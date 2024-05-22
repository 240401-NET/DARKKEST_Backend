using DarkkestP3.API.Model;
// using DarkkestP3.API.Data;
using DarkkestP3.API.Repository;
using DarkkestP3.API.Service;
using DarkkestP3.API.DTO;
using Moq;

namespace DarkkestP3.Tests;

public class ProfileServiceTests
{
    [Fact]
    public void CreateUserProfile_ReturnsProfile()
    {
        
        //Arrange
        Mock<IProfileRepository> testRepo = new Mock<IProfileRepository>();

        Profile createdProfile = new Profile 
        {
            UserId = "UserId",
            Interersts = "interests",
            Skills = "skills",
            MissionStatement = "mission statement"
        };

        NewProfile newProfile = new NewProfile
        {
            userId = "UserId",
            interests = "interests",
            skills = "skills",
            missionStatement = "mission statement"
        };

        testRepo.Setup(r => r.AddUserProfile(createdProfile)).Returns(createdProfile);

        ProfileService profileService = new ProfileService(testRepo.Object);

        //Act
        var result = profileService.CreateUserProfile(newProfile);


        //Assert
        Assert.IsType(typeof(Task<Profile>), result);
    }

    [Fact]
    public async void GetUserProfileByUserId_Returns_Profile()
    {
        //Arrange
        Mock<IProfileRepository> testRepo = new Mock<IProfileRepository>();

        string userId = "userId";
        Profile profile = new Profile
        {
            UserId = "UserId",
            Interersts = "interests",
            Skills = "skills",
            MissionStatement = "mission statement"
        };

        testRepo.Setup(r => r.GetUserProfileByUserId(userId)).ReturnsAsync(profile);

        ProfileService profileService = new ProfileService(testRepo.Object);

        //Act
        var result = await profileService.GetUserProfileByUserId(userId);

        //Assert
        Assert.IsType(typeof(Profile), result);

    }

    [Fact]
    public void UpdateUserProfile_Updates_Profile()
    {
        //Arrange
        Mock<IProfileRepository> testRepo = new Mock<IProfileRepository>();

        Profile profile = new Profile
        {
            UserId = "UserId",
            Interersts = "updated interests",
            Skills = "updated skills",
            MissionStatement = "updated mission statement"
        };

        UpdateProfile updateProfile = new UpdateProfile
        {
            userId = "userId",
            updatedInterests = "updated interests",
            updatedSkills = "updated skills",
            updatedMissionStatement = "updated mission statement"
        };

        testRepo.Setup(r => r.UpdateUserProfile(updateProfile)).ReturnsAsync(profile);

        ProfileService profileService = new ProfileService(testRepo.Object);

        //Act
        var result = profileService.UpdateUserProfile(updateProfile);

        //Assert
        Assert.IsType(typeof(Task<Profile>), result);
        Assert.Equal(updateProfile.updatedInterests,profile.Interersts);
        Assert.Equal(updateProfile.updatedSkills,profile.Skills);
        Assert.Equal(updateProfile.updatedMissionStatement,profile.MissionStatement);

    }

    [Fact]
    public void UpdateUserProfileInterests_Updates_ProfileInterests()
    {
        //Arrange
        Mock<IProfileRepository> testRepo = new Mock<IProfileRepository>();

        Profile patchedProfile = new Profile
        {
            UserId = "UserId",
            Interersts = "updated interests",
            Skills = "skills",
            MissionStatement = "mission statement"
        };

        PatchProfileInterests patchProfile = new PatchProfileInterests
        {
            userId = "userId",
            updatedInterests = "updated interests"

        };

        testRepo.Setup(r => r.UpdateUserProfileInterests(patchProfile)).ReturnsAsync(patchedProfile);

        ProfileService profileService = new ProfileService(testRepo.Object);

        //Act
        var result = profileService.UpdateUserProfileInterests(patchProfile);

        //Assert
        Assert.IsType(typeof(Task<Profile>), result);
        Assert.Equal(patchProfile.updatedInterests,patchedProfile.Interersts);
    }

    [Fact]
    public void UpdateUserProfileSkills_Updates_ProfileSkills()
    {
        //Arrange
        Mock<IProfileRepository> testRepo = new Mock<IProfileRepository>();

        Profile patchedProfile = new Profile
        {
            UserId = "UserId",
            Interersts = "interests",
            Skills = "updated skills",
            MissionStatement = "mission statement"
        };

        PatchProfileSkills patchProfile = new PatchProfileSkills
        {
            userId = "userId",
            updatedSkills = "updated skills"

        };

        testRepo.Setup(r => r.UpdateUserProfileSkills(patchProfile)).ReturnsAsync(patchedProfile);

        ProfileService profileService = new ProfileService(testRepo.Object);

        //Act
        var result = profileService.UpdateUserProfileSkills(patchProfile);

        //Assert
        Assert.IsType(typeof(Task<Profile>), result);
        Assert.Equal(patchProfile.updatedSkills,patchedProfile.Skills);
    }

        [Fact]
    public void UpdateUserProfileMissionStatement_Updates_ProfileMissionStatement()
    {
        //Arrange
        Mock<IProfileRepository> testRepo = new Mock<IProfileRepository>();

        Profile patchedProfile = new Profile
        {
            UserId = "UserId",
            Interersts = "interests",
            Skills = "skills",
            MissionStatement = "updated mission statement"
        };

        PatchProfileMissionStatement patchProfile = new PatchProfileMissionStatement
        {
            userId = "userId",
            updatedMissionStatement = "updated mission statement"

        };

        testRepo.Setup(r => r.UpdateUserProfileMissionStatement(patchProfile)).ReturnsAsync(patchedProfile);

        ProfileService profileService = new ProfileService(testRepo.Object);

        //Act
        var result = profileService.UpdateUserProfileMissionStatement(patchProfile);

        //Assert
        Assert.IsType(typeof(Task<Profile>), result);
        Assert.Equal(patchProfile.updatedMissionStatement,patchedProfile.MissionStatement);
    }

    [Fact]
    public void DeleteUserProfile_Deletes_Profile()
    {
        //Arrange
        Mock<IProfileRepository> testRepo = new Mock<IProfileRepository>();

        string userId = "userId";
        Profile profile = new Profile
        {
            UserId = "UserId",
            Interersts = "interests",
            Skills = "skills",
            MissionStatement = "updated mission statement"
        };

        testRepo.Setup(r => r.DeleteUserProfile(userId)).ReturnsAsync(profile);

        ProfileService profileService = new ProfileService(testRepo.Object);

        //Act
        var result = profileService.DeleteUserProfile(userId);

        //Assert
        Assert.IsType(typeof(Task<Profile>), result);

    }

}