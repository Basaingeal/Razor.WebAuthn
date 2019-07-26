using System;
using System.Collections.Generic;
using System.Text;

namespace CurrieTechnologies.Razor.WebAuthn
{
    public class GeolocationCoordinates
    {
        public double Latitude { get; internal set; }
        public double Longitude { get; internal set; }
        public double? Altitude { get; internal set; }
        public double Accuracy { get; internal set; }
        public double? AltitudeAccuracy { get; internal set; }
        public double? Heading { get; internal set; }
        public double? Speed { get; internal set; }
    }
}
