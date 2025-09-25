using RelationshipDemo.Classes;
using RelationshipDemo.Contracts;
using RelationshipDemo.Enums;
using System;
using System.Collections.Generic;
using System.Linq;


namespace RelationshipDemo.Services
{
    public class RelationshipGenerator
    {
        private static readonly Random random = new Random();

        private static readonly string[] Names = {
        "Anna", "Ben", "Clara", "David", "Emma", "Felix", "Greta", "Hans", "Iris", "Jonas"
    };

        private static readonly RelationshipType[] RelationshipTypes =
            Enum.GetValues<RelationshipType>();

        public static RelationshipManager GenerateRandomNetwork()
        {
            var manager = new RelationshipManager();

            // Personen erstellen
            List<IRelationshipParticipant> participants = new List<IRelationshipParticipant>();
            for (int i = 0; i < Names.Length; i++)
            {
                IRelationshipParticipant person = new Person
                {
                    Id = i + 1,
                    Name = Names[i],
                    Email = $"{Names[i].ToLower()}@example.com"
                };
                participants.Add(person);
                manager.AddParticipant(person);
            }

            // Beziehungen generieren
            foreach (var person in participants)
            {
                var relationshipCount = random.Next(3, 8);
                var targets = participants.Where(p => p != person)
                                    .OrderBy(x => random.Next())
                                    .Take(relationshipCount);

                foreach (IRelationshipParticipant target in targets)
                {
                    manager.AddRelationship(person, target, GetRandomRelationshipType(), GenerateRandomQuality());
                }
            }

            return manager;
        }

        private static RelationshipType GetRandomRelationshipType()
        {
            return RelationshipTypes[random.Next(RelationshipTypes.Length)];
        }

        private static RelationshipQuality GenerateRandomQuality()
        {
            return new RelationshipQuality
            {
                Trust = GenerateWeightedRating(),
                Respect = GenerateWeightedRating(),
                Affection = GenerateWeightedRating(),
                Compatibility = GenerateWeightedRating()
            };
        }

        private static int GenerateWeightedRating()
        {
            // 60% Chance auf positive Werte, 25% neutral, 15% negativ
            var roll = random.NextDouble();

            if (roll < 0.15) // Negativ
                return random.Next(-10, 0);
            else if (roll < 0.40) // Neutral
                return random.Next(-2, 3);
            else // Positiv
                return random.Next(1, 11);
        }
    }
}