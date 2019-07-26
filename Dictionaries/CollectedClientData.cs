namespace CurrieTechnologies.Razor.WebAuthn
{
    public class CollectedClientData
    {
        public string Type { get; set; }
        public string Challenge { get; set; }
        public string Origin { get; set; }
        public TokenBinding? TokenBinding { get; set; }
    }
}
