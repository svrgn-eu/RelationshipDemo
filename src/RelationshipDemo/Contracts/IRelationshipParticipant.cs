using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationshipDemo.Contracts
{
    public interface IRelationshipParticipant
    {
        string Name { get; set; }
        string Email { get; set; }
    }
}
