using System.ComponentModel.DataAnnotations;

public record Book(
    [property: StringLength(50)] string Title,
    string? Publisher = default,
    int BookId = 0);
