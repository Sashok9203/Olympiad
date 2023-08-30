using data_access.Entities;

namespace OlympiadWPF.Models.CommonClasses
{
    internal class MedalTableInfo
    {
        public Country Country { get; set; }
        public int Gold { get; set; }
        public int Silver { get; set; }
        public int Bronze { get; set; }
        public int Total => Gold + Silver + Bronze;
        public string? Flag => Country.FlagPath ?? "Images/Sportsmans/Unknown_Flag.png";
    }
}
