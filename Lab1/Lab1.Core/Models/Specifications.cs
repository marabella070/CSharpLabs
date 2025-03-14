namespace Lab1.Core.Models;

/// <summary>
/// Defines a contract for validating an entity of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of entity to validate.</typeparam>
public interface ISpecification<T>
{
    /// <summary>
    /// Validates the entity against the specification.
    /// </summary>
    /// <param name="entity">The entity to validate.</param>
    /// <returns>True if the entity satisfies the specification; otherwise, false.</returns>
    bool IsSatisfiedBy(T entity);

    /// <summary>
    /// Gets the error message if the entity does not satisfy the specification.
    /// </summary>
    string ErrorMessage { get; }
}

/// <summary>
/// Represents a concrete implementation of <see cref="ISpecification{T}"/>.
/// </summary>
/// <typeparam name="T">The type of entity to validate.</typeparam>
public class FabricSpecification<T> : ISpecification<T>
{
    private readonly Func<T, bool> _validationRule;
    private readonly string _errorMessage;

    /// <summary>
    /// Initializes a new instance of the <see cref="Specification{T}"/> class.
    /// </summary>
    /// <param name="validationRule">The rule used to validate the entity.</param>
    /// <param name="errorMessage">The error message if the entity fails validation.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="validationRule"/> or <paramref name="errorMessage"/> is null.
    /// </exception>
    public FabricSpecification(Func<T, bool> validationRule, string errorMessage)
    {
        _validationRule = validationRule ?? throw new ArgumentNullException(nameof(validationRule));
        _errorMessage = errorMessage ?? throw new ArgumentNullException(nameof(errorMessage));
    }

    /// <inheritdoc />
    public bool IsSatisfiedBy(T value)
    {
        return _validationRule(value);
    }

    /// <inheritdoc />
    public string ErrorMessage => _errorMessage;
}

public static class StandardSpecifications
{
    /// <summary>
    /// Returns a specification that checks if a string is not null or empty.
    /// </summary>
    /// <param name="errorMessage">The error message to return if the validation fails.</param>
    /// <returns>A specification for non-null and non-empty strings.</returns>
    public static ISpecification<string> NonEmptyString(string errorMessage = "Value cannot be null or empty.")
    {
        return new FabricSpecification<string>(
            value => !string.IsNullOrEmpty(value),
            errorMessage
        );
    }

    /// <summary>
    /// Returns a specification that checks if a collection is not null and contains at least one element.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="errorMessage">The error message to return if the validation fails.</param>
    /// <returns>A specification for non-null and non-empty collections.</returns>
    public static ISpecification<ICollection<T>> NonEmptyCollection<T>(string errorMessage = "Collection cannot be null or empty.")
    {
        return new FabricSpecification<ICollection<T>>(
            value => value != null && value.Count > 0,
            errorMessage
        );
    }

    /// <summary>
    /// Returns a specification that checks if a collection of strings is not null, not empty, and contains no null or empty strings.
    /// </summary>
    /// <param name="errorMessage">The error message to return if the validation fails.</param>
    /// <returns>A specification for non-null, non-empty collections with non-null and non-empty strings.</returns>
    public static ISpecification<ICollection<string>> AllNonEmptyStrings(string errorMessage = "Collection cannot be null, empty, or contain null or empty strings.")
    {
        return new FabricSpecification<ICollection<string>>(
            value => value != null && // Checking that the collection is not null
                    value.Count > 0 && // Checking that the collection is not empty
                    value.All(item => !string.IsNullOrEmpty(item)), // Checking that all elements are not null or empty
            errorMessage
        );
    }
}