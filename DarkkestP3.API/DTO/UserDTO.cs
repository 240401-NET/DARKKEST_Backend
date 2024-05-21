namespace DarkkestP3.API.DTO;

public record struct RegisterUser(string Username, string Email, string Password);
public record struct LoginUser(string Username, string Password);