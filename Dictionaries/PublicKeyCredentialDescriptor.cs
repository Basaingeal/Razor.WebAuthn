using System;
using System.Collections.Generic;
using System.Text;

namespace CurrieTechnologies.Razor.WebAuthn
{
    public class PublicKeyCredentialDescriptor
    {
        public PublicKeyCredentialType Type { get; set; }
#pragma warning disable CA1819 // Properties should not return arrays
        public byte[] Id { get; set; }
#pragma warning restore CA1819 // Properties should not return arrays
        public IEnumerable<AuthenticatorTransport>? Transports { get; set; }
    }
}
