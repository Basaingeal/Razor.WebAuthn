using System;
using System.Collections.Generic;
using System.Text;

namespace CurrieTechnologies.Razor.WebAuthn
{
    public class PublicKeyCredentialUserEntity: PublicKeyCredentialEntity
    {
#pragma warning disable CA1819 // Properties should not return arrays
        public byte[] Id { get; set; }
#pragma warning restore CA1819 // Properties should not return arrays
        public string DisplayName { get; set; }
    }
}
