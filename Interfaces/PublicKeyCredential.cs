using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CurrieTechnologies.Razor.WebAuthn.Models
{
    public abstract class PublicKeyCredential : Credential
    {
#pragma warning disable CA1819 // Properties should not return arrays
        public byte[] RawId { get; internal set; }
#pragma warning restore CA1819 // Properties should not return arrays

        public AuthenticatorResponse Response { get; internal set; }

        public AuthenticationExtensionsClientOutputs GetClientExtensionResults()
        {
            throw new NotImplementedException();
        }

        public static Task<bool> IsUserVerifyingPlatformAuthenticatorAvailable()
        {
            throw new NotImplementedException();
        }
    }
}
