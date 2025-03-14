using System.Text;

namespace Lab1.Core.Models;

/// <summary>
/// Represents an abstract production entity.
/// </summary>
public abstract class Production
{
    private readonly ISpecification<string> _nameSpecification = StandardSpecifications.NonEmptyString();
    private readonly ISpecification<string> _managerSpecification = StandardSpecifications.NonEmptyString();
    private readonly ISpecification<ICollection<string>> _productionListSpecification = StandardSpecifications.AllNonEmptyStrings();

    // NAME
    private string _name;
    public string Name => _name;
    public void SetName(string value)
    {
        Set<string>(_nameSpecification, out _name, value);
    }
    public bool TrySetName(string value, out string errorMessage)
    {
        return TrySet<string>(_nameSpecification, ref _name, value, out errorMessage);
    }

    // MANAGER
    private string _manager;
    public string Manager => _manager;
    public void SetManager(string value)
    {
        Set<string>(_managerSpecification, out _manager, value);
    }
    public bool TrySetManager(string value, out string errorMessage)
    {
        return TrySet<string>(_managerSpecification, ref _manager, value, out errorMessage);
    }

    // WORKER COUNT
    private uint _workerCount;
    public uint WorkerCount { get; set; }

    // PRODUCT_LIST
    private HashSet<string> _productList;
    public HashSet<string> ProductList => _productList;

    public void SetProductList(HashSet<string> value)
    {
        Set<ICollection<string>>(_productionListSpecification, out _productList, value);
    }
    public bool TrySetProductList(HashSet<string> value, out string errorMessage)
    {
        return TrySet<ICollection<string>>(_productionListSpecification, ref _productList, value, out errorMessage);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Production"/> class with the specified parameters.
    /// Validates the input values using predefined specifications and sets the corresponding fields.
    /// </summary>
    /// <param name="name">The name of the production. Must not be null or empty.</param>
    /// <param name="manager">The name of the production manager. Must not be null or empty.</param>
    /// <param name="workerCount">The number of workers in the production.</param>
    /// <param name="productList">The list of manufactured products. Must not be null, empty, or contain null or empty items.</param>
    /// <exception cref="ArgumentException">
    /// Thrown if <paramref name="name"/>, <paramref name="manager"/>, or <paramref name="productList"/> fails validation.
    /// </exception>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="productList"/> is null.</exception>
    protected Production(string name, string manager, uint workerCount, HashSet<string> productList)
    {
        // Setting values with verification
        Set<string>(_nameSpecification, out _name, name);
        Set<string>(_managerSpecification, out _manager, manager);
        _workerCount = workerCount;
        Set<HashSet<string>>(_productionListSpecification, out _productList, productList);
    }

    /// <summary>
    /// Adds a product to the product list.
    /// </summary>
    /// <param name="product">The name of the product to add.</param>
    /// <exception cref="ArgumentException">Thrown if the product name is null or empty.</exception>
    public void AddProduct(string product)
    {
        if (string.IsNullOrEmpty(product))
        {
            throw new ArgumentException("Product name cannot be null or empty.");
        }
        ProductList.Add(product);
    }

    /// <summary>
    /// Removes a product from the product list.
    /// </summary>
    /// <param name="product">The name of the product to remove.</param>
    /// <returns>True if the product was removed; otherwise, false.</returns>
    public bool RemoveProduct(string product)
    {
        return ProductList.Remove(product);
    }

    /// <summary>
    /// Generates a string with production details.
    /// </summary>
    /// <returns>A formatted string containing production information.</returns>
    public string GetProductionInfo()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Production: {Name}");
        sb.AppendLine($"Manager: {Manager}");
        sb.AppendLine($"Number of workers: {WorkerCount}");
        sb.Append(FormatList(ProductList, "The list of the nomenclature of manufactured products:", item => $" - {item}"));

        return sb.ToString();
    }

    /// <summary>
    /// Displays production information using the provided output method.
    /// </summary>
    /// <param name="output">A delegate to handle the output (e.g., console, file).</param>
    public void ShowProductionInfo(Action<string> output)
    {
        string info = GetProductionInfo();
        output(info); // passing the output string
    }

    // AUXILIARY METHODS

    /// <summary>
    /// Sets a value to a variable after validating it against the provided specification.
    /// If the value does not satisfy the specification, an <see cref="ArgumentException"/> is thrown.
    /// </summary>
    /// <typeparam name="T">The type of the value to be set.</typeparam>
    /// <param name="specification">The specification used to validate the value.</param>
    /// <param name="_variable">The variable to which the value will be assigned.</param>
    /// <param name="value">The value to be validated and set.</param>
    /// <exception cref="ArgumentException">Thrown if the value does not satisfy the specification.</exception>
    private void Set<T>(ISpecification<T> specification, out T _variable, T value)
    {
        if (!specification.IsSatisfiedBy(value))
        {
            throw new ArgumentException(specification.ErrorMessage);
        }

        _variable = value;
    }

    /// <summary>
    /// Attempts to set a value to a variable after validating it against the provided specification.
    /// If the value does not satisfy the specification, the method returns false and provides an error message.
    /// </summary>
    /// <typeparam name="T">The type of the value to be set.</typeparam>
    /// <param name="specification">The specification used to validate the value.</param>
    /// <param name="_variable">The variable to which the value will be assigned.</param>
    /// <param name="value">The value to be validated and set.</param>
    /// <param name="errorMessage">The error message if the validation fails.</param>
    /// <returns>True if the value satisfies the specification and is successfully set; otherwise, false.</returns>
    private bool TrySet<T>(ISpecification<T> specification, ref T _variable, T value, out string errorMessage)
    {
        if (!specification.IsSatisfiedBy(value))
        {
            errorMessage = specification.ErrorMessage;
            return false;
        }

        _variable = value;
        errorMessage = string.Empty;
        return true;
    }

    /// <summary>
    /// Formats a list of items into a readable string with a title.
    /// </summary>
    /// <typeparam name="T">The type of items in the list.</typeparam>
    /// <param name="list">The list of items to format.</param>
    /// <param name="title">The title to display above the list.</param>
    /// <param name="formatItem">A function to format each item.</param>
    /// <returns>A formatted string representation of the list.</returns>
    protected string FormatList<T>(IEnumerable<T> list, string title, Func<T, string> formatItem)
    {
        var sb = new StringBuilder();
        sb.AppendLine(title);

        if (list == null || !list.Any())
        {
            sb.AppendLine("- No items available.");
        }
        else
        {
            foreach (var item in list)
            {
                sb.AppendLine(formatItem(item));
            }
        }

        return sb.ToString();
    }
}