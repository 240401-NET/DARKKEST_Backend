namespace DarkkestP3.API.DTO;

public record struct NewOpp(string JobTitle, string Description);
public record struct UpdateOpp(int Id, string JobTitle, string Description);