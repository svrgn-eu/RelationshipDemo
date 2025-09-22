using RelationshipDemo.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RelationshipDemo.Classes
{
    public class RelationshipQuality
    {
        public int Trust { get; set; } // -10 bis +10
        public int Respect { get; set; } // -10 bis +10
        public int Affection { get; set; } // -10 bis +10
        public int Compatibility { get; set; } // -10 bis +10

        public double OverallScore => (Trust + Respect + Affection + Compatibility) / 4.0;

        public RelationshipSentiment GetOverallSentiment()
        {
            return OverallScore switch
            {
                < -5 => RelationshipSentiment.VeryNegative,
                < 0 => RelationshipSentiment.Negative,
                0 => RelationshipSentiment.Neutral,
                <= 5 => RelationshipSentiment.Positive,
                _ => RelationshipSentiment.VeryPositive
            };
        }

        public override string ToString()
        {
            return $"Trust: {Trust}, Respect: {Respect}, Affection: {Affection}, Compatibility: {Compatibility}";
        }
    }
}