using System;
using System.Collections.Generic;
using System.Text;

namespace CurrieTechnologies.Razor.WebAuthn
{
    public class AuthenticatorAttestationResponse : AuthenticatorResponse
    {
#pragma warning disable CA1819 // Properties should not return arrays
        public byte[] AttestationObject { get; internal set; }
#pragma warning restore CA1819 // Properties should not return arrays
        public IEnumerable<AuthenticatorTransport> GetTransports()
        {
            throw new NotImplementedException();
        }
    }
}
