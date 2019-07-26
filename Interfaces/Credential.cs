using System;
using System.Collections.Generic;
using System.Text;

namespace CurrieTechnologies.Razor.WebAuthn
{
    public abstract class Credential
    {
        public string Id { get; internal set; }
        public string Type { get; internal set; }
    }
}
