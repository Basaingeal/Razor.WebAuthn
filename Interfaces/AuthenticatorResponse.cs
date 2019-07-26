using System;
using System.Collections.Generic;
using System.Text;

namespace CurrieTechnologies.Razor.WebAuthn
{
    public abstract class AuthenticatorResponse
    {
#pragma warning disable CA1819 // Properties should not return arrays
        public byte[] ClientDataJSON { get; set; }
#pragma warning restore CA1819 // Properties should not return arrays
    }
}
