namespace Lab1.Core.Models;

using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

// An abstract base class that implements the validation logic
public abstract class ValidatableObject
{
    // A universal method for validating a property
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

    // A universal method for validating the entire object
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

    // A universal method for setting values with validation
    protected void SetValueWithValidation<T>(ref T field, string propertyName, T value)
    {
        ValidateProperty(propertyName, value); // Validation
        field = value; // Assignment
    }
}