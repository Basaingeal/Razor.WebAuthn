using System.Collections.Generic;

namespace CurrieTechnologies.Razor.WebAuthn
{
    public class AuthenticationExtensionsClientInputs
    {
        public string? AppId { get; set; }
        public string? TxAuthSimple { get; set; }
        public TxAuthGenericArg? TxAuthGeneric { get; set; }
        public IEnumerable<byte[]>? AuthnSel { get; set; }
        public bool? Exts { get; set; }
        public bool? Uvi { get; set; }
        public bool? Loc { get; set; }
        public bool? Uvm { get; set; }
        public bool? CredProps { get; set; }
    }
}
