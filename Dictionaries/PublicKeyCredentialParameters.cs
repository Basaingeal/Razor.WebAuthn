namespace CurrieTechnologies.Razor.WebAuthn
{
    public class PublicKeyCredentialParameters
    {
        public PublicKeyCredentialType Type { get; set; }
        public long Alg { get; set; }
    }
}
