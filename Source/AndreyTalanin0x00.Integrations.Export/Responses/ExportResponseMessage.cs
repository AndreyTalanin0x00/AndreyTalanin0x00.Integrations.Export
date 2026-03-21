namespace AndreyTalanin0x00.Integrations.Export.Responses;

public class ExportResponseMessage
{
    public required ExportResponseMessageType Type { get; set; }

    public required string Text { get; set; }
}
