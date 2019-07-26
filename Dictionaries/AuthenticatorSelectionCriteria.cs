using System;
using System.Collections.Generic;
using System.Text;

namespace CurrieTechnologies.Razor.WebAuthn
{
    public class AuthenticatorSelectionCriteria
    {
        public AuthenticatorAttachment? AuthenticatorAttachment { get; set; }
        public bool? RequireResidentKey { get; set; } = false;
        public ResidentKeyRequirement? ResidentKey { get; set; }
        public UserVerificationRequirement? UserVerification { get; set; } = UserVerificationRequirement.Preferred;
    }
}
