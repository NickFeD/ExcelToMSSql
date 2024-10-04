namespace DB.Entities;

public class Client
{
    public int Id { get; set; }
    public int CardCode { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string? FirstName { get; set; }
    public string? SurName { get; set; }
    public string PhoneMobile { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? GenderId { get; set; }
    public DateOnly Birthday { get; set; }
    public string? City { get; set; }
    public string? Pincode { get; set; }
    public int? Bonus { get; set; }
    public int? Turnover { get; set; }
}
