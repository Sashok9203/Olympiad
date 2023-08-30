using data_access.Entities;

namespace OlympiadWPF.Models.CommonClasses
{
    internal class SpotrsmenInfo
    {
        public Sportsman Sportsman { get; set; }
        public string PhotoPath => Sportsman.PhotoPath ?? "Images/Sportsmans/NoPhoto.png";
        public int Gold { get; set; }
        public int Silver { get; set; }
        public int Bronze { get; set; }
    }
}
