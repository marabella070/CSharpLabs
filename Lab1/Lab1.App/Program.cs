using System.Runtime.CompilerServices;

using Lab1.Core.Models;

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

workshop.ShowProductionInfo(Console.Write);

workshop.ShowWorkshopInfo(Console.Write);

workshop.ShowSchedule(Console.Write);