namespace DNCCorporate.ViewModels;

public record class EmailMessageViewModel(
    string Subject,
    string MessageBody,
    string Recipient);