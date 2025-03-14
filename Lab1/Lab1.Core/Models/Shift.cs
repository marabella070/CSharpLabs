namespace Lab1.Core.Models;

public struct Shift
{
    private readonly TimeSpan _startTime;
    private readonly TimeSpan _endTime;

    /// <summary>
    /// Initializes a new instance of the <see cref="Shift"/> structure.
    /// </summary>
    /// <param name="startTime">The start time of the shift.</param>
    /// <param name="endTime">The end time of the shift.</param>
    /// <exception cref="ArgumentException">Thrown when endTime is earlier than startTime.</exception>
    public Shift(TimeSpan startTime, TimeSpan endTime)
    {
        _startTime = startTime;
        _endTime = endTime;
    }

    /// <summary>
    /// Gets the start time of the shift.
    /// </summary>
    public TimeSpan StartTime => _startTime;

    /// <summary>
    /// Gets the end time of the shift.
    /// </summary>
    public TimeSpan EndTime => _endTime;

    /// <summary>
    /// Gets the duration of the shift.
    /// </summary>
    public TimeSpan Duration => _endTime - _startTime;

    /// <summary>
    /// Returns a string representation of the shift in the format "hh:mm - hh:mm".
    /// </summary>
    /// <returns>A string that represents the shift's start and end times.</returns>
    public override string ToString()
    {
        return $"{_startTime:hh\\:mm} - {_endTime:hh\\:mm}";
    }
}