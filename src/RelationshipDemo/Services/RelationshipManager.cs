using RelationshipDemo.Classes;
using RelationshipDemo.Contracts;
using RelationshipDemo.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RelationshipDemo.Services
{
    public class RelationshipManager
    {
        private List<IRelationshipParticipant> _participants = new List<IRelationshipParticipant>();
        private List<Relationship> _allRelationships = new List<Relationship>();

        public void AddParticipant(IRelationshipParticipant participant)
        {
            if (!_participants.Contains(participant))
            {
                _participants.Add(participant);
            }
        }

        public void AddRelationship(IRelationshipParticipant from, IRelationshipParticipant to, RelationshipType type, RelationshipQuality quality)
        {
            // Stelle sicher, dass beide Personen registriert sind
            AddParticipant(from);
            AddParticipant(to);

            // Prüfe, ob die Beziehung bereits existiert
            if (GetRelationship(from, to) != null)
                return;

            var relationship = new Relationship
            {
                From = from,
                To = to,
                Type = type,
                Quality = quality,
                StartDate = DateTime.Now.AddDays(-new Random().Next(1, 3650)) // 1-10 Jahre zurück
            };

            _allRelationships.Add(relationship);
        }

        public IEnumerable<Relationship> GetAllRelationships() => _allRelationships.AsReadOnly();

        public IEnumerable<Relationship> GetOutgoingRelationships(IRelationshipParticipant person)
        {
            return _allRelationships.Where(r => r.From == person);
        }

        public IEnumerable<Relationship> GetIncomingRelationships(IRelationshipParticipant person)
        {
            return _allRelationships.Where(r => r.To == person);
        }

        public IEnumerable<Relationship> GetAllRelationshipsInvolving(IRelationshipParticipant person)
        {
            return _allRelationships.Where(r => r.From == person || r.To == person);
        }

        public IEnumerable<(Relationship Outgoing, Relationship Incoming)> GetMutualRelationships()
        {
            var mutualPairs = new List<(Relationship, Relationship)>();

            foreach (var rel in _allRelationships)
            {
                var reciprocal = _allRelationships.FirstOrDefault(r =>
                    r.From == rel.To && r.To == rel.From);

                if (reciprocal != null && !mutualPairs.Any(mp =>
                    (mp.Item1 == rel && mp.Item2 == reciprocal) ||
                    (mp.Item1 == reciprocal && mp.Item2 == rel)))
                {
                    mutualPairs.Add((rel, reciprocal));
                }
            }

            return mutualPairs;
        }

        public Relationship GetRelationship(IRelationshipParticipant from, IRelationshipParticipant to)
        {
            return _allRelationships.FirstOrDefault(r => r.From == from && r.To == to);
        }

        public IEnumerable<IRelationshipParticipant> GetAllParticipants() => _participants.AsReadOnly();

        public int TotalRelationships => _allRelationships.Count;
        public int TotalPeople => _participants.Count;

        public void RemoveRelationship(IRelationshipParticipant from, IRelationshipParticipant to)
        {
            var relationship = GetRelationship(from, to);
            if (relationship != null)
            {
                _allRelationships.Remove(relationship);
            }
        }

        public void UpdateRelationshipQuality(IRelationshipParticipant from, IRelationshipParticipant to, RelationshipQuality newQuality)
        {
            var relationship = GetRelationship(from, to);
            if (relationship != null)
            {
                relationship.Quality = newQuality;
            }
        }
    }
}