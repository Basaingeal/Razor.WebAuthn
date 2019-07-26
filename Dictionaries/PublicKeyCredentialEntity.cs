using System;
using System.Collections.Generic;
using System.Text;

namespace CurrieTechnologies.Razor.WebAuthn
{
    public abstract class PublicKeyCredentialEntity
    {
        public string Name { get; set; }
        public string? Icon { get; set; }
    }
}
