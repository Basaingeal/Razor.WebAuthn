namespace CurrieTechnologies.Razor.WebAuthn
{
    public class AuthenticatorAssertionResponse : AuthenticatorResponse
    {
#pragma warning disable CA1819 // Properties should not return arrays
        public byte[] AuthenticatorData { get; internal set; }
        public byte[] Signature { get; internal set; }
        public byte[]? UserHandle { get; internal set; }
#pragma warning restore CA1819 // Properties should not return arrays
    }
}
