using System;
using System.Collections.Generic;
using System.Text;

namespace CurrieTechnologies.Razor.WebAuthn
{
    public class AuthenticationExtensionsClientOutputs
    {
        public bool? AppId { get; set; }
        public string? TxAuthSimple { get; set; }
#pragma warning disable CA1819 // Properties should not return arrays
        public byte[]? TxAuthGeneric { get; set; }
#pragma warning restore CA1819 // Properties should not return arrays
        public bool? AuthnSel { get; set; }
        public IEnumerable<string>? Exts { get; set; }
#pragma warning disable CA1819 // Properties should not return arrays
        public byte[]? Uvi { get; set; }
#pragma warning restore CA1819 // Properties should not return arrays
        public GeolocationCoordinates? Loc { get; set; }
        public IEnumerable<IEnumerable<ulong>>? Uvm { get; set; }
        public CredentialPropertiesOutput? CredProps { get; set; }
    }
}
