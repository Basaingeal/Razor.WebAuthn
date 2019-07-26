namespace CurrieTechnologies.Razor.WebAuthn
{
    public class TxAuthGenericArg
    {
        public string ContentType { get; set; }
#pragma warning disable CA1819 // Properties should not return arrays
        public byte[] Content { get; set; }
#pragma warning restore CA1819 // Properties should not return arrays
    }
}
