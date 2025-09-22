using System;
using System.Collections.Generic;
using System.Linq;

public class RelationshipManager
{
    private List<Person> _people = new List<Person>();
    private List<Relationship> _allRelationships = new List<Relationship>();
    
    public void AddPerson(Person person)
    {
        if (!_people.Contains(person))
        {
            _people.Add(person);
        }
    }
    
    public void AddRelationship(Person from, Person to, RelationshipType type, RelationshipQuality quality)
    {
        // Stelle sicher, dass beide Personen registriert sind
        AddPerson(from);
        AddPerson(to);
        
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
    
    public IEnumerable<Relationship> GetOutgoingRelationships(Person person)
    {
        return _allRelationships.Where(r => r.From == person);
    }
    
    public IEnumerable<Relationship> GetIncomingRelationships(Person person)
    {
        return _allRelationships.Where(r => r.To == person);
    }
    
    public IEnumerable<Relationship> GetAllRelationshipsInvolving(Person person)
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
    
    public Relationship GetRelationship(Person from, Person to)
    {
        return _allRelationships.FirstOrDefault(r => r.From == from && r.To == to);
    }
    
    public IEnumerable<Person> GetAllPeople() => _people.AsReadOnly();
    
    public int TotalRelationships => _allRelationships.Count;
    public int TotalPeople => _people.Count;
    
    public void RemoveRelationship(Person from, Person to)
    {
        var relationship = GetRelationship(from, to);
        if (relationship != null)
        {
            _allRelationships.Remove(relationship);
        }
    }
    
    public void UpdateRelationshipQuality(Person from, Person to, RelationshipQuality newQuality)
    {
        var relationship = GetRelationship(from, to);
        if (relationship != null)
        {
            relationship.Quality = newQuality;
        }
    }
}