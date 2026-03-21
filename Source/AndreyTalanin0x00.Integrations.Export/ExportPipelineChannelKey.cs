namespace AndreyTalanin0x00.Integrations.Export;

public record ExportPipelineChannelKey
{
    public static readonly ExportPipelineChannelKey Empty = new(string.Empty);

    public string Value { get; init; }

    public ExportPipelineChannelKey(string value)
    {
        Value = value;
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}
