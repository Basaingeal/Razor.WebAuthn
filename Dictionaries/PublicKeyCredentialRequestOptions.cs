using System;
using System.Collections.Generic;
using System.Text;

namespace CurrieTechnologies.Razor.WebAuthn
{
    public class PublicKeyCredentialRequestOptions
    {
#pragma warning disable CA1819 // Properties should not return arrays
        public byte[] Challenge { get; set; }
#pragma warning restore CA1819 // Properties should not return arrays
        public ulong? Timeout { get; set; }
        public string? RpId { get; set; }
        public IEnumerable<PublicKeyCredentialDescriptor>? AllowCredentials { get; set; } = Array.Empty<PublicKeyCredentialDescriptor>();
        public UserVerificationRequirement? UserVerification { get; set; } = UserVerificationRequirement.Preferred;
        public AuthenticationExtensionsClientInputs? Extensions { get; set; }
    }
}
