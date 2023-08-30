namespace OlympiadWPF.Models.CommonClasses
{
    internal class MedalTableInfo
    {
        public string Country { get; set; }
        public int Gold { get; set; }
        public int Silver { get; set; }
        public int Bronze { get; set; }
        public int Total => Gold + Silver + Bronze;
        public string? Flag { get; set; }
    }
}
