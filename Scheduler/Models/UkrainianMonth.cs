namespace Scheduler.Models;

public static class UkrainianMonth
{
    public const string January = "Січня";
    public const string February = "Лютого";
    public const string March = "Березня";
    public const string April = "Квітня";
    public const string May = "Травня";
    public const string June = "Червня";
    public const string July = "Липня";
    public const string August = "Серпня";
    public const string September = "Вересня";
    public const string October = "Жовтня";
    public const string November = "Листопада";
    public const string December = "Грудня";
    public const string NotFound = "Не знайдено";

    public static string ConvertMonth(int month)
    {
        return month switch
        {
            1 => January,
            2 => February,
            3 => March,
            4 => April,
            5 => May,
            6 => June,
            7 => July,
            8 => August,
            9 => September,
            10 => October,
            11 => November,
            12 => December,
            _ => NotFound
        };
    }
}