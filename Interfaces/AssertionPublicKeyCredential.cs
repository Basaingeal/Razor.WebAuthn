using System;
using System.Collections.Generic;
using System.Text;

namespace CurrieTechnologies.Razor.WebAuthn
{
    public class AssertionPublicKeyCredential : PublicKeyCredential
    {
#pragma warning disable CA1819 // Properties should not return arrays
        public new byte[] RawId { get; internal set; }
#pragma warning restore CA1819 // Properties should not return arrays

        public new AuthenticatorAssertionResponse Response { get; internal set; }
    }
}
