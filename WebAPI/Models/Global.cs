namespace WebAPI.Models
{
    public enum RepeatFrequency
    {
        Never,
        Daily,
        Weekly,
        Monthly,
        Quarterly,
        Yearly
    }

    public enum Importance
    {
        Low,
        Normal,
        High,
        Critical,
    }
    
    public enum CalendarView
    {
        Day,
        Week,
        Month,
    }
}
