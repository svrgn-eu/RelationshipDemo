using System;
using System.Collections.Generic;
using System.Linq;

public class Relationship
{
    public Person From { get; set; }
    public Person To { get; set; }
    public RelationshipType Type { get; set; }
    public RelationshipQuality Quality { get; set; }
    public DateTime StartDate { get; set; }
    
    public override string ToString()
    {
        return $"{From.Name} -> {To.Name} ({Type}): {Quality.GetOverallSentiment()} (Score: {Quality.OverallScore:F1})";
    }
}