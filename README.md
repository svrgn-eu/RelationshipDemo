[TOC]

# Relationship Management System

A C# program for modeling and managing interpersonal relationships with detailed quality assessment.

## 🚀 Features

- **Directed Relationships**: Each person can rate others differently
- **Detailed Quality Assessment**: Trust, Respect, Affection, Compatibility (each -10 to +10)
- **Various Relationship Types**: Friend, Family, Colleague, Romantic, Business, Neighbor
- **Centralized Management**: RelationshipManager for consistent data handling
- **Comprehensive Analysis Functions**: Statistics, mutual relationships, sentiment analysis
- **Random Generator**: Creates realistic relationship networks for testing

## 📋 Table of Contents

- [Installation](https://claude.ai/chat/46e3b06b-cd3a-4d2f-82e8-20b22ab2d7d8#installation)
- [Usage](https://claude.ai/chat/46e3b06b-cd3a-4d2f-82e8-20b22ab2d7d8#usage)
- [Class Structure](https://claude.ai/chat/46e3b06b-cd3a-4d2f-82e8-20b22ab2d7d8#class-structure)
- [API Reference](https://claude.ai/chat/46e3b06b-cd3a-4d2f-82e8-20b22ab2d7d8#api-reference)
- [Examples](https://claude.ai/chat/46e3b06b-cd3a-4d2f-82e8-20b22ab2d7d8#examples)
- [Extensions](https://claude.ai/chat/46e3b06b-cd3a-4d2f-82e8-20b22ab2d7d8#extensions)

## 🛠️ Installation

1. Install .NET 6.0 or higher
2. Clone project or copy code
3. Compile and run:

```bash
dotnet run
```

## 💡 Usage

### Basic Usage

```csharp
// Create RelationshipManager
var manager = new RelationshipManager();

// Add people
var alice = new Person { Id = 1, Name = "Alice", Email = "alice@example.com" };
var bob = new Person { Id = 2, Name = "Bob", Email = "bob@example.com" };

manager.AddPerson(alice);
manager.AddPerson(bob);

// Create relationship
var quality = new RelationshipQuality
{
    Trust = 8,
    Respect = 7,
    Affection = 9,
    Compatibility = 6
};

manager.AddRelationship(alice, bob, RelationshipType.Friend, quality);
```

### Generate Random Network

```csharp
var manager = RelationshipGenerator.GenerateRandomNetwork();
Console.WriteLine($"Generated: {manager.TotalPeople} people, {manager.TotalRelationships} relationships");
```

## 🏗️ Class Structure

### Person

Represents a single person in the system.

- `Id`: Unique identifier
- `Name`: Person's name
- `Email`: Email address

### Relationship

Models a directed relationship between two people.

- `From`: Source person
- `To`: Target person
- `Type`: Type of relationship (RelationshipType Enum)
- `Quality`: Detailed quality assessment
- `StartDate`: Beginning of relationship

### RelationshipQuality

Detailed assessment of a relationship with four dimensions:

- `Trust`: Trust level (-10 to +10)
- `Respect`: Respect level (-10 to +10)
- `Affection`: Affection level (-10 to +10)
- `Compatibility`: Compatibility level (-10 to +10)
- `OverallScore`: Average value of all dimensions
- `GetOverallSentiment()`: Categorization from Very Negative to Very Positive

### RelationshipManager

Central management class for all relationships and people.

## 📚 API Reference

### RelationshipManager Methods

#### Core Functions

```csharp
void AddPerson(Person person)                    // Add person
void AddRelationship(Person from, Person to, RelationshipType type, RelationshipQuality quality)
Relationship GetRelationship(Person from, Person to)  // Find specific relationship
void RemoveRelationship(Person from, Person to)       // Delete relationship
void UpdateRelationshipQuality(Person from, Person to, RelationshipQuality newQuality)
```

#### Query Functions

```csharp
IEnumerable<Person> GetAllPeople()               // All people
IEnumerable<Relationship> GetAllRelationships() // All relationships
IEnumerable<Relationship> GetOutgoingRelationships(Person person)  // Outgoing relationships
IEnumerable<Relationship> GetIncomingRelationships(Person person)  // Incoming relationships
IEnumerable<Relationship> GetAllRelationshipsInvolving(Person person) // Incoming and outgoing
IEnumerable<(Relationship, Relationship)> GetMutualRelationships() // Mutual relationships
```

#### Properties

```csharp
int TotalRelationships  // Total number of relationships
int TotalPeople        // Total number of people
```

## 🔍 Examples

### Relationship Analysis

```csharp
var manager = RelationshipGenerator.GenerateRandomNetwork();

// Find positive relationships
var positiveRelationships = manager.GetAllRelationships()
    .Where(r => r.Quality.GetOverallSentiment() == RelationshipSentiment.Positive);

// Analyze mutual relationships
var mutualRelationships = manager.GetMutualRelationships();
foreach (var (rel1, rel2) in mutualRelationships)
{
    Console.WriteLine($"{rel1.From.Name} ⟷ {rel1.To.Name}");
    var avgScore = (rel1.Quality.OverallScore + rel2.Quality.OverallScore) / 2;
    Console.WriteLine($"Average rating: {avgScore:F1}");
}
```

### Generate Statistics

```csharp
var allRelationships = manager.GetAllRelationships();

// Sentiment distribution
var sentimentStats = allRelationships
    .GroupBy(r => r.Quality.GetOverallSentiment())
    .ToDictionary(g => g.Key, g => g.Count());

// Type distribution  
var typeStats = allRelationships
    .GroupBy(r => r.Type)
    .ToDictionary(g => g.Key, g => g.Count());
```

### Person-Specific Analysis

```csharp
var person = manager.GetAllPeople().First();

Console.WriteLine($"Outgoing relationships: {manager.GetOutgoingRelationships(person).Count()}");
Console.WriteLine($"Incoming relationships: {manager.GetIncomingRelationships(person).Count()}");

// Find most liked person
var mostLiked = manager.GetAllPeople()
    .OrderByDescending(p => manager.GetIncomingRelationships(p)
        .Average(r => r.Quality.OverallScore))
    .First();
```

## 🎲 Random Generation

The `RelationshipGenerator` creates realistic relationship networks:

- **60%** positive relationships
- **25%** neutral relationships
- **15%** negative relationships
- Each person has **3-7** relationships
- Random relationship types and start dates

## 🔧 Extension Possibilities

### Database Integration

```csharp
public class DatabaseRelationshipManager : RelationshipManager
{
    // Entity Framework Integration
    // SQL Database persistence
}
```

### Temporal Evolution

```csharp
public class TemporalRelationship : Relationship
{
    public List<QualitySnapshot> QualityHistory { get; set; }
    public void UpdateQuality(RelationshipQuality newQuality) 
    {
        QualityHistory.Add(new QualitySnapshot { Quality = Quality, Date = DateTime.Now });
        Quality = newQuality;
    }
}
```

### Graph Algorithms

```csharp
// Shortest paths between people
// Community detection
// Influencer identification
// Clustering algorithms
```

### Web API

```csharp
[ApiController]
[Route("api/[controller]")]
public class RelationshipController : ControllerBase
{
    // REST API for CRUD operations
    // JSON serialization
    // Swagger documentation
}
```

### Machine Learning Integration

```csharp
// Relationship prediction based on existing patterns
// Sentiment analysis from text data
// Recommendation system for new connections
```

## 📊 Sample Output

```
=== Relationship Generator with RelationshipManager ===

Generated People:
- Anna (anna@example.com)
- Ben (ben@example.com)
- Clara (clara@example.com)
[...]

Network Overview: 10 people, 47 relationships

=== Statistics ===
Distribution by Sentiment:
  VeryNegative: 3
  Negative: 5
  Neutral: 12
  Positive: 18
  VeryPositive: 9

=== Mutual Relationships ===
Anna ⟷ Ben:
  Anna -> Ben: Positive (6.2)
  Ben -> Anna: VeryPositive (8.1)
  Average: 7.2, Symmetric: No
```

## 🤝 Contributing

Improvements and extensions are welcome! Potential areas:

- Performance optimizations
- Additional relationship types
- Extended analysis functions
- UI/Web interface
- Export/Import functionality

## 🏆 Use Cases

This system can be applied in various domains:

- **Social Network Analysis**: Understanding community structures and influence patterns
- **HR Management**: Modeling team dynamics and collaboration effectiveness
- **Game Development**: Creating realistic NPC relationship systems
- **Research**: Studying social psychology and relationship patterns
- **CRM Systems**: Managing customer and business partner relationships
- **Family Tree Applications**: Tracking family relationships and dynamics

## 🧪 Testing

The random generator creates diverse test scenarios:

```csharp
// Generate multiple networks for testing
for (int i = 0; i < 10; i++)
{
    var testNetwork = RelationshipGenerator.GenerateRandomNetwork();
    // Run analysis tests
}
```

## 📈 Performance Considerations

- **Time Complexity**: Most operations are O(n) where n is the number of relationships
- **Memory Usage**: Linear with the number of people and relationships
- **Optimization Opportunities**:
  - Index relationships by person for faster queries
  - Use HashSets for duplicate prevention
  - Implement caching for frequently accessed data

## 🔐 Data Privacy

When extending this system for real-world use:

- Implement proper access controls
- Consider data encryption for sensitive relationship information
- Follow GDPR/privacy regulations for personal data
- Provide data export/deletion capabilities

## 📄 License

This project is licensed under the MIT License - see LICENSE file for details.

## 🙏 Acknowledgments

- Inspired by social network theory and graph databases
- Built with modern C# best practices
- Designed for educational and research purposes