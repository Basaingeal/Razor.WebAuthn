namespace CurrieTechnologies.Razor.WebAuthn
{
    public class CredentialRequestOptions
    {
        public PublicKeyCredentialRequestOptions? PublicKey { get; set; }
        public CredentialMediationRequirement? Mediation { get; set; } = CredentialMediationRequirement.Optional;
    }
}
