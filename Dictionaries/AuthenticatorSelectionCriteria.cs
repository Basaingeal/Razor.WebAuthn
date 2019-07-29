namespace CurrieTechnologies.Razor.WebAuthn
{
    public class AuthenticatorSelectionCriteria
    {
        public AuthenticatorAttachment? AuthenticatorAttachment { get; set; }
        public bool? RequireResidentKey { get; set; } = false;
        //TODO: Patch in ResidentKey on frontend
        internal ResidentKeyRequirement? ResidentKey { get; set; }
        public UserVerificationRequirement? UserVerification { get; set; } = UserVerificationRequirement.Preferred;
    }
}
