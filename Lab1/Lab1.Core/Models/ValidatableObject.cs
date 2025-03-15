namespace Lab1.Core.Models;

using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

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


public static class TestValidator
{
    /// <summary>
    /// Validates a single property of the object.
    /// </summary>
    /// <typeparam name="T">The type of the property.</typeparam>
    /// <param name="propertyName">The name of the property to validate.</param>
    /// <param name="value">The value of the property to validate.</param>
    /// <exception cref="ValidationException">Thrown when the property value is invalid.</exception>
    public static void ValidateProperty<T>(string propertyName, T value)
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
    public static void ValidateObject()
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
    public static void SetValueWithValidation<T>(ref T field, string propertyName, T value)
    {
        ValidateProperty(propertyName, value); // Validation
        field = value; // Assignment
    }
}




public static class ValidatorHelper
{
    // Метод для валидации объекта
    public static void ValidateObject<T>(T obj)
    {
        if (obj == null)
            throw new ArgumentNullException(nameof(obj));

        // Получаем свойства, которые принадлежат только текущему классу (не унаследованные)
        var properties = GetNonInheritedProperties(typeof(T));

        // Создаем контекст валидации
        var context = new ValidationContext(obj);
        var validationResults = new List<ValidationResult>();

        // Валидируем каждое свойство
        foreach (var property in properties)
        {
            // Получаем значение свойства
            var value = property.GetValue(obj);

            // Валидируем значение свойства
            bool isValid = Validator.TryValidateProperty(value, new ValidationContext(obj) { MemberName = property.Name }, validationResults);

            if (!isValid)
            {
                foreach (var validationResult in validationResults)
                {
                    Console.WriteLine(validationResult.ErrorMessage);
                }
                throw new ValidationException($"Validation failed for property '{property.Name}'.");
            }
        }

        Console.WriteLine("Validation succeeded.");
    }

    // Метод для получения свойств, которые не унаследованы от базового класса
    public static IEnumerable<PropertyInfo> GetNonInheritedProperties(Type type)
    {
        if (type == null)
            throw new ArgumentNullException(nameof(type));

        // Получаем все свойства текущего класса
        var allProperties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

        // Получаем все свойства базового класса
        var baseProperties = type.BaseType?.GetProperties(BindingFlags.Public | BindingFlags.Instance) ?? Array.Empty<PropertyInfo>();

        // Исключаем свойства, которые есть в базовом классе
        var nonInheritedProperties = allProperties.Where(p => !baseProperties.Any(bp => bp.Name == p.Name && bp.PropertyType == p.PropertyType));

        return nonInheritedProperties;
    }
}
