using System;
using System.Collections.Generic;
using System.Text;

namespace CurrieTechnologies.Razor.WebAuthn
{
    public class PublicKeyCredentialParameters
    {
        public PublicKeyCredentialType Type { get; set; }
        public long Alg { get; set; }
    }
}
