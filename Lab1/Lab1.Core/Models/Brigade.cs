namespace Lab1.Core.Models;

/// <summary>
/// Represents a brigade with an ID and a name.
/// </summary>
public struct Brigade
{
    



    public int Id { get; }


    public string Name { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Brigade"/> structure.
    /// </summary>
    /// <param name="id">The unique ID of the brigade.</param>
    /// <param name="name">The name of the brigade.</param>
    /// <exception cref="ArgumentException">Thrown when the name is null or empty.</exception>
    public Brigade(int id, string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("Name cannot be null or empty.", nameof(name));
        }

        Id = id;
        Name = name;
    }

    /// <summary>
    /// Returns a string representation of the brigade.
    /// </summary>
    /// <returns>A string containing the brigade's ID and name.</returns>
    public override string ToString()
    {
        return $"Brigade [Id: {Id}, Name: {Name}]";
    }
}