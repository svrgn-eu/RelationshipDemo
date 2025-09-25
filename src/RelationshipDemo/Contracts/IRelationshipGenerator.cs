using RelationshipDemo.Services;

namespace RelationshipDemo.Contracts
{
    public interface IRelationshipGenerator
    {
        static abstract RelationshipManager GenerateRandomNetwork();
    }
}