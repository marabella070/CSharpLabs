namespace Lab1.Core.Models;

using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Reflection;

/// <summary>
/// Represents an abstract base class that provides validation logic for derived classes.
/// </summary>
public abstract class ValidatableObject
{
    /// <summary>
    /// Validates a single property of the object.
    /// </summary>
    /// <typeparam name="T">The type of the property.</typeparam>
    /// <param name="propertyName">The name of the property to validate.</param>
    /// <param name="value">The value of the property to validate.</param>
    /// <exception cref="ValidationException">Thrown when the property value is invalid.</exception>
    protected void ValidateProperty<T>(string propertyName, T value)
    {
        var validationResults = new List<ValidationResult>();
        var context = new ValidationContext(this) { MemberName = propertyName };

        bool isValid = Validator.TryValidateProperty(value, context, validationResults);

        if (!isValid)
        {
            foreach (var result in validationResults)
            {
                Console.WriteLine(result.ErrorMessage);
            }
            throw new ValidationException($"Invalid value for '{propertyName}'.");
        }
    }

    /// <summary>
    /// Validates the entire object.
    /// </summary>
    /// <exception cref="ValidationException">Thrown when the object fails validation.</exception>
    protected void ValidateObject()
    {
        var validationResults = new List<ValidationResult>();
        var context = new ValidationContext(this);

        bool isValid = Validator.TryValidateObject(this, context, validationResults, true); // 'true' validates all properties

        if (!isValid)
        {
            foreach (var validationResult in validationResults)
            {
                Console.WriteLine(validationResult.ErrorMessage);
            }
            throw new ValidationException("Object validation failed.");
        }
    }

    /// <summary>
    /// Sets a property value with validation.
    /// </summary>
    /// <typeparam name="T">The type of the property.</typeparam>
    /// <param name="field">The backing field to set the value for.</param>
    /// <param name="propertyName">The name of the property.</param>
    /// <param name="value">The value to assign to the property.</param>
    protected void SetValueWithValidation<T>(ref T field, string propertyName, T value)
    {
        ValidateProperty(propertyName, value); // Validation
        field = value; // Assignment
    }
}

public static class ValidatorHelper
{
    // Method for object validation
    public static void ValidateObject<T>(T obj)
    {
        if (obj == null)
        {
            throw new ArgumentNullException(nameof(obj));
        }

        // Getting properties that belong only to the current class (not inherited)
        var properties = GetNonInheritedProperties(typeof(T));

        // Validating each property
        foreach (var property in properties)
        {
            // Getting the property value
            var value = property.GetValue(obj);

            // Validating the property value
            ValidateProperty(obj, property.Name, value);
        }

        Console.WriteLine("Validation succeeded.");
    }

    public static void SetValueWithValidation<T, K>(T obj, ref K field, string propertyName, K value)
    {
        ValidateProperty(obj, propertyName, value); // Validation
        field = value; // Assignment
    }

    // Method for validating a single property
    private static void ValidateProperty<T, K>(T obj, string propertyName, K value)
    {
        if (obj == null)
        {
            throw new ArgumentNullException(nameof(obj));
        }

        var validationResults = new List<ValidationResult>();
        var context = new ValidationContext(obj) { MemberName = propertyName };

        bool isValid = Validator.TryValidateProperty(value, context, validationResults);

        if (!isValid)
        {
            foreach (var result in validationResults)
            {
                Console.WriteLine(result.ErrorMessage);
            }
            throw new ValidationException($"Invalid value for '{propertyName}'.");
        }
    }

    public static IEnumerable<PropertyInfo> GetNonInheritedProperties(Type type)
    {
        if (type == null)
        {
            throw new ArgumentNullException(nameof(type));
        }

        // Getting all the properties of the current class
        var allProperties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

        // Getting all the properties of the base class
        var baseProperties = type.BaseType?.GetProperties(BindingFlags.Public | BindingFlags.Instance) ?? Array.Empty<PropertyInfo>();

        // We exclude properties that are in the base class
        var nonInheritedProperties = allProperties.Where(p =>
        {
            // Exclude indexers (properties with parameters)
            bool isIndexer = p.GetIndexParameters().Any();
            
            return !isIndexer && !baseProperties.Any(bp => bp.Name == p.Name && bp.PropertyType == p.PropertyType);
        });

        return nonInheritedProperties;
    }
}