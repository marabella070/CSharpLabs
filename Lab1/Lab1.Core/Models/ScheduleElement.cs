namespace Lab1.Core.Models;

/// <summary>
/// Represents a schedule element containing work days, relaxation days, and a shift.
/// </summary>
public struct ScheduleElement
{
    public int WorkDays { get; }
    public int RelaxDays { get; }
    public Shift Shift { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ScheduleElement"/> class.
    /// </summary>
    /// <param name="workDays">The number of work days.</param>
    /// <param name="relaxDays">The number of relaxation days.</param>
    /// <param name="shift">The shift associated with the work days.</param>
    public ScheduleElement(int workDays, int relaxDays, Shift shift)
    {
        WorkDays = workDays;
        RelaxDays = relaxDays;
        Shift = shift;
    }

    /// <summary>
    /// Returns a string representation of the <see cref="ScheduleElement"/>.
    /// </summary>
    /// <returns>A string describing the work days, shift, and relaxation days.</returns>
    public override string ToString()
    {
        return $"{WorkDays} work days ({Shift}), followed by {RelaxDays} relaxation days";
    }
}