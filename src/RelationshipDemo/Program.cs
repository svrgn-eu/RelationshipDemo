using RelationshipDemo.Enums;
using RelationshipDemo.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RelationshipDemo
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("=== Relationship-Generator with RelationshipManager ===\n");

            // Netzwerk generieren
            var manager = RelationshipGenerator.GenerateRandomNetwork();

            Console.WriteLine("Generated Persons:");
            foreach (var person in manager.GetAllPeople())
            {
                Console.WriteLine($"- {person.Name} ({person.Email})");
            }

            Console.WriteLine($"Network Overview: {manager.TotalPeople} Persons, {manager.TotalRelationships} Relations");

            // Alle Beziehungen anzeigen (gruppiert nach Person)
            Console.WriteLine("\n=== All Relations (outbound) ===");
            foreach (var person in manager.GetAllPeople())
            {
                var outgoingRels = manager.GetOutgoingRelationships(person).OrderBy(r => r.To.Name);
                Console.WriteLine($"\n{person.Name}s outbound Relations:");
                foreach (var rel in outgoingRels)
                {
                    Console.WriteLine($"  {rel}");
                    Console.WriteLine($"    Details: {rel.Quality}");
                }
            }

            // Statistiken
            Console.WriteLine("\n=== Statistics ===");
            var allRelationships = manager.GetAllRelationships().ToList();

            var sentimentStats = allRelationships
                .GroupBy(r => r.Quality.GetOverallSentiment())
                .ToDictionary(g => g.Key, g => g.Count());

            Console.WriteLine("Distribution by Sentiment:");
            foreach (var sentiment in Enum.GetValues<RelationshipSentiment>())
            {
                var count = sentimentStats.GetValueOrDefault(sentiment, 0);
                Console.WriteLine($"  {sentiment}: {count}");
            }

            var typeStats = allRelationships
                .GroupBy(r => r.Type)
                .ToDictionary(g => g.Key, g => g.Count());

            Console.WriteLine("Distribution by Type:");
            foreach (var type in Enum.GetValues<RelationshipType>())
            {
                var count = typeStats.GetValueOrDefault(type, 0);
                Console.WriteLine($"  {type}: {count}");
            }

            // Besondere Beziehungen
            Console.WriteLine("\n=== Special Relations ===");

            var veryPositive = allRelationships
                .Where(r => r.Quality.GetOverallSentiment() == RelationshipSentiment.VeryPositive)
                .OrderByDescending(r => r.Quality.OverallScore)
                .Take(3);

            Console.WriteLine("\nTop 3 positive Relations:");
            foreach (var rel in veryPositive)
            {
                Console.WriteLine($"  {rel}");
            }

            var veryNegative = allRelationships
                .Where(r => r.Quality.GetOverallSentiment() == RelationshipSentiment.VeryNegative)
                .OrderBy(r => r.Quality.OverallScore)
                .Take(3);

            if (veryNegative.Any())
            {
                Console.WriteLine("\nTop 3 negative Relations:");
                foreach (var rel in veryNegative)
                {
                    Console.WriteLine($"  {rel}");
                }
            }

            // Gegenseitige Beziehungen über den Manager
            Console.WriteLine("\n=== Mutual Relations===");
            var mutualRelationships = manager.GetMutualRelationships().Take(5);

            foreach (var (outgoing, incoming) in mutualRelationships)
            {
                Console.WriteLine($"{outgoing.From.Name} ⟷ {outgoing.To.Name}:");
                Console.WriteLine($"  {outgoing.From.Name} -> {outgoing.To.Name}: {outgoing.Quality.GetOverallSentiment()} ({outgoing.Quality.OverallScore:F1})");
                Console.WriteLine($"  {incoming.From.Name} -> {incoming.To.Name}: {incoming.Quality.GetOverallSentiment()} ({incoming.Quality.OverallScore:F1})");

                var avgScore = (outgoing.Quality.OverallScore + incoming.Quality.OverallScore) / 2;
                var isSymmetric = Math.Abs(outgoing.Quality.OverallScore - incoming.Quality.OverallScore) < 2;
                Console.WriteLine($"    Average: {avgScore:F1}, Symmetric: {(isSymmetric ? "Ja" : "Nein")}");
            }

            // Demo der Manager-Funktionen
            Console.WriteLine("\n=== Manager Functions Demo ===");
            var samplePerson = manager.GetAllPeople().First();

            Console.WriteLine($"\n{samplePerson.Name}s complete Relations:");
            Console.WriteLine($"  Outbound: {manager.GetOutgoingRelationships(samplePerson).Count()}");
            Console.WriteLine($"  Inbound: {manager.GetIncomingRelationships(samplePerson).Count()}");
            Console.WriteLine($"  all involved: {manager.GetAllRelationshipsInvolving(samplePerson).Count()}");

            // Zeige eingehende Beziehungen
            var incomingRels = manager.GetIncomingRelationships(samplePerson).Take(3);
            Console.WriteLine($"First 3 inbound Relations for {samplePerson.Name}:");
            foreach (var rel in incomingRels)
            {
                Console.WriteLine($"  {rel.From.Name} -> {samplePerson.Name}: {rel.Quality.GetOverallSentiment()}");
            }
        }
    }
}