namespace Distribt.Services.Emails.DTOs
{
    public record EmailDto
    (
        string From,
        string To,
        string Subject,
        string Body
    );
}
