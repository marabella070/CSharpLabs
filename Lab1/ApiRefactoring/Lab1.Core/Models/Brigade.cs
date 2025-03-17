namespace Lab1.Core.Models;

using Lab1.Core.Helpers;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Represents a brigade with an ID and a name.
/// </summary>
public class Brigade
{
    //! ID
    private readonly uint _id;

    [Range(1, uint.MaxValue, ErrorMessage = "ID must be greater than zero.")]
    public uint Id => _id;

    //! NAME
    private string _name;

    [Required(ErrorMessage = "Name is required.")] // Name cannot be null or empty.
    public string Name
    {
        get => _name;
        set => ValidatorHelper.SetValueWithValidation(this, ref _name, nameof(Name), value); // Validation and assignment
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Brigade"/> class.
    /// </summary>
    /// <param name="id">The unique identifier of the brigade.</param>
    /// <param name="name">The name of the brigade.</param>
    public Brigade(uint id, string name)
    {
        _id = id;
        _name = name;

        ValidatorHelper.ValidateObject(this);
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