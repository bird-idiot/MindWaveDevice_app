using System;
using System.Collections.Generic;
using System.Text;

namespace MindWaveDevice_app
{
    class BrainData
    {
        public int? poorSignalLevel { get; set; }
        public string? status { get; set; }
        public int? rawEeg { get; set; }
        public int? blinkStrength { get; set; }
        public float? mentalEffort { get; set; }
        public float? familiarity { get; set; }
        public SenseData? eSense { get; set; }
        public EegData? eegPower { get; set; }
    }

    public class SenseData
    {
        public int attention { get; set; }
        public int meditation { get; set; }
    }

    public class EegData
    {
        public int delta { get; set; }
        public int theta { get; set; }
        public int lowAlpha { get; set; }
        public int highAlpha { get; set; }
        public int lowBeta { get; set; }
        public int highBeta { get; set; }
        public int lowGamma { get; set; }
        public int highGamma { get; set; }
    }
}
