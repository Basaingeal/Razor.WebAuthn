using System;
using System.Collections.Generic;
using System.Text;

namespace CurrieTechnologies.Razor.WebAuthn
{
    public class TokenBinding
    {
        public TokenBindingStatus Status { get; set; }
        public string? Id { get; set; }
    }
}
