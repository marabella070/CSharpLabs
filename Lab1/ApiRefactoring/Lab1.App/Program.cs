using System.Runtime.CompilerServices;

using Lab1.Core.Models;





using System.Reflection;

Type type = typeof(Workshop);

// Получение всех методов
Console.WriteLine("Methods:");
foreach (MethodInfo method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
{
    Console.WriteLine($"{method.ReturnType.Name} {method.Name}({string.Join(", ", method.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}"))})");
}

// Получение всех свойств
Console.WriteLine("\nProperties:");
foreach (PropertyInfo property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
{
    Console.WriteLine($"{property.PropertyType.Name} {property.Name} {{ get; set; }}");
}

// Получение индексаторов
Console.WriteLine("\nIndexers:");
foreach (PropertyInfo indexer in type.GetProperties().Where(p => p.GetIndexParameters().Length > 0))
{
    Console.WriteLine($"{indexer.PropertyType.Name} this[{string.Join(", ", indexer.GetIndexParameters().Select(p => $"{p.ParameterType.Name} {p.Name}"))}] {{ get; set; }}");
}







    /*
    var (shifts, scheduleElements) = StandardSchedules.ThreeShiftFiveBrigade;

    // Creating Workshop
    Workshop workshop = new Workshop(
        name: "Global AutoWorks",
        manager: "Michael Reynolds",
        workerCount: 1200,
        productList: new List<string>
        {
            "Engine Blocks",
            "Transmission Systems",
            "Brake Discs",
            "Suspension Components",
            "Electric Vehicle Batteries"
        },
        id: 1,
        new List<Brigade> {
            new Brigade(1, "Alpha"),
            new Brigade(2, "Bronson"),
            new Brigade(3, "Chuck-Norris"),
            new Brigade(4, "Vortex"),
            new Brigade(5, "Titan"),        
        },
        shifts: shifts,
        schedule: scheduleElements
    );

    workshop.ShowInfo(Console.Write);

    Console.WriteLine();

    workshop.ShowSchedule(Console.Write);
    */