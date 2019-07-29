using System;
using System.Collections.Generic;
using System.Text;

namespace CurrieTechnologies.Razor.WebAuthn
{
    public class AttestationPublicKeyCredential : PublicKeyCredential
    {
#pragma warning disable CA1819 // Properties should not return arrays
        public new byte[] RawId { get; internal set; }
#pragma warning restore CA1819 // Properties should not return arrays

        public new AuthenticatorAttestationResponse Response { get; internal set; }
    }
}
