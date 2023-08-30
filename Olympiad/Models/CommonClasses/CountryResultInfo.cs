using data_access.Entities;

namespace OlympiadWPF.Models.CommonClasses
{
    internal class CountryResultInfo
    {
        public Sport Sport { get; set; }
        public int Gold { get; set; }
        public int Silver { get; set; }
        public int Bronze { get; set; }
    }
}
