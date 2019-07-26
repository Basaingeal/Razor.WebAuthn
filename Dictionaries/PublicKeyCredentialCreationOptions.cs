using System;
using System.Collections.Generic;
using System.Text;

namespace CurrieTechnologies.Razor.WebAuthn
{
    public class PublicKeyCredentialCreationOptions
    {
        public PublicKeyCredentialRpEntity Rp { get; set; }
        public PublicKeyCredentialUserEntity User { get; set; }

#pragma warning disable CA1819 // Properties should not return arrays
        public byte[] Challenge { get; set; }
#pragma warning restore CA1819 // Properties should not return arrays
        public IEnumerable<PublicKeyCredentialParameters> PubKeyCredParams { get; set; }

        public ulong? Timeout { get; set; }
        public IEnumerable<PublicKeyCredentialDescriptor>? ExcludeCredentials { get; set; } = Array.Empty<PublicKeyCredentialDescriptor>();
        public AuthenticatorSelectionCriteria? AuthenticatorSelection { get; set; }
        public AttestationConveyancePreference? Attestation { get; set; } = AttestationConveyancePreference.None;
        public AuthenticationExtensionsClientInputs? Extensions { get; set; }
    }
}
