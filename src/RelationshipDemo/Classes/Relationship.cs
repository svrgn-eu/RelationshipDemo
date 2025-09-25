using RelationshipDemo.Contracts;
using RelationshipDemo.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RelationshipDemo.Classes
{
    public class Relationship
    {
        public IRelationshipParticipant From { get; set; }
        public IRelationshipParticipant To { get; set; }
        public RelationshipType Type { get; set; }
        public RelationshipQuality Quality { get; set; }
        public DateTime StartDate { get; set; }

        public override string ToString()
        {
            return $"{From.Name} -> {To.Name} ({Type}): {Quality.GetOverallSentiment()} (Score: {Quality.OverallScore:F1})";
        }
    }
}