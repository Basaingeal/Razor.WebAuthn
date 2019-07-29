namespace CurrieTechnologies.Razor.WebAuthn
{
    public abstract class AuthenticatorResponse
    {
#pragma warning disable CA1819 // Properties should not return arrays
        public byte[] ClientDataJSON { get; internal set; }
#pragma warning restore CA1819 // Properties should not return arrays
    }
}
