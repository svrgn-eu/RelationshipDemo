using RelationshipDemo.Classes;
using RelationshipDemo.Enums;

namespace RelationshipDemo.Contracts
{
    public interface IRelationshipManager
    {
        int TotalPeople { get; }
        int TotalRelationships { get; }

        void AddParticipant(IRelationshipParticipant participant);
        void AddRelationship(IRelationshipParticipant from, IRelationshipParticipant to, RelationshipType type, RelationshipQuality quality);
        IEnumerable<IRelationshipParticipant> GetAllParticipants();
        IEnumerable<Relationship> GetAllRelationships();
        IEnumerable<Relationship> GetAllRelationshipsInvolving(IRelationshipParticipant person);
        IEnumerable<Relationship> GetIncomingRelationships(IRelationshipParticipant person);
        IEnumerable<(Relationship Outgoing, Relationship Incoming)> GetMutualRelationships();
        IEnumerable<Relationship> GetOutgoingRelationships(IRelationshipParticipant person);
        Relationship GetRelationship(IRelationshipParticipant from, IRelationshipParticipant to);
        void RemoveRelationship(IRelationshipParticipant from, IRelationshipParticipant to);
        void UpdateRelationshipQuality(IRelationshipParticipant from, IRelationshipParticipant to, RelationshipQuality newQuality);
    }
}