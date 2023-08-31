using data_access.Entities;
using data_access.Repositories;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Win32;
using Olympiad;
using OlympiadWPF.Models.CommonClasses;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;

namespace OlympiadWPF.Models
{
    internal partial class OlympiadDBModel
    {
        private ModifySpotrsmanWindow? SportsmanWindow;

        private int selectedSportIndex;

        private List<Gender>? gndr = null;

        private List<Award>? awd = null;

        private List<Gender> genders => gndr ??= unitOW.Genders.Get().ToList();

        private List<Award> awards => awd ??= unitOW.Awards.Get().ToList();

        private void copySportsman(Sportsman from,Sportsman to)
        {
            to.Sport = from.Sport;
            to.Birthday = from.Birthday;
            to.Surname = from.Surname;
            to.Name = from.Name;
            to.SportId = from.SportId;
            to.Country = from.Country;
            to.CountryId = from.CountryId;
            to.Gender = from.Gender;
            to.GenderId = from.GenderId;
            to.PhotoPath = from.PhotoPath;
            foreach (var item in from.AwardOlympiads)
                to.AwardOlympiads.Add(item);
        }

        private void saveButton(object o) => SportsmanWindow.DialogResult = true;

        private void deleteContextMenu(object o)
        {
            EditableSportsman?.AwardOlympiads.Remove(SelectedAwardOlympiad);
            OnPropertyChanged("EditableSportsmanAwardsOlympiads");
            OnPropertyChanged("EditWindowComboBoxOlympiads");
        }

        private void addAwardOlympiad(object o)
        {
            SportsmanAwardOlympiad temp = new() { Olympiad = SelectedOlympiad, Award = SelectedAward };
            EditableSportsman?.AwardOlympiads.Add(temp);

            SelectedOlympiad = null;
            SelectedAward = null;
            OnPropertyChanged("SelectedOlympiad");
            OnPropertyChanged("SelectedAward");
            OnPropertyChanged("EditableSportsmanAwardsOlympiads");
            OnPropertyChanged("EditWindowComboBoxOlympiads");
        }

        private void getPhoto(object o)
        {
            OpenFileDialog openFileDialog = new() { Filter = "Image Files (*.png;*.jpeg;*.jpg;)|*.jpg;*.jpeg;*.png;" };
            openFileDialog.ShowDialog();
                EditableSportsman.PhotoPath =  openFileDialog.FileName;
            OnPropertyChanged("EditableSportsman");
        }

        private void ModifySportsman(bool isNew)
        {
            EditableSportsman = new();
            if (!isNew) copySportsman(SlectedSportsmanForEdit,EditableSportsman);
            
            countries?.RemoveAt(0);
            sports?.RemoveAt(0);
            olympiads?.RemoveAt(0);
            SportsmanWindow = new() { DataContext = this };
            if (SportsmanWindow.ShowDialog() == true)
            {
                if (isNew) unitOW.Sportsmans.Insert(EditableSportsman);
                else
                {
                    copySportsman(EditableSportsman,SlectedSportsmanForEdit);
                    unitOW.Sportsmans.Update(SlectedSportsmanForEdit);
                }
                unitOW.Save();
                sptms = null;
                spAwOl = null;
                OnPropertyChanged("AllSportsmans");
                OnPropertyChanged("Sportsmans");
                OnPropertyChanged("CountryResult");
                OnPropertyChanged("MedalTable");
            }
               
            countries?.Insert(0, new() { Name = "All", Id = -1 });
            sports?.Insert(0, new() { Name = "All", Id = -1 });
            olympiads?.Insert(0, new() { Id = -1 });
        }


        public IEnumerable<Award> Awards => awards;

        public IEnumerable<Olympiad_> EditWindowComboBoxOlympiads => olympiads.Where(x => x.SeasonId == EditableSportsman?.Sport?.SeasonId && !EditableSportsman.AwardOlympiads.Any(y=>y.Olympiad.Id == x.Id));

        public IEnumerable<Gender> Genders => genders;

        public IEnumerable<SportsmanAwardOlympiad>? EditableSportsmanAwardsOlympiads => EditableSportsman?.AwardOlympiads.ToArray();

        public Olympiad_? SelectedOlympiad { get; set; }

        public Award? SelectedAward { get; set; }

        public Sportsman? EditableSportsman { get; set; }

        public Sportsman? SlectedSportsmanForEdit { get; set; }

        public SportsmanAwardOlympiad? SelectedAwardOlympiad { get; set; }


        public RelayCommand Save => new((o) =>saveButton(o),(o=> !string.IsNullOrEmpty(EditableSportsman?.Name) 
                                                                              && !string.IsNullOrEmpty(EditableSportsman.Surname)
                                                                              && EditableSportsman.Sport!=null 
                                                                              && EditableSportsman.Country!=null
                                                                              && EditableSportsman.Gender!= null));

        public RelayCommand Add => new((o) => addAwardOlympiad(o),(o)=> SelectedOlympiad != null);

        public RelayCommand Delete => new((o) => deleteContextMenu(o),(o)=> SelectedAwardOlympiad != null);

        public RelayCommand GetPhoto => new((o) => getPhoto(o));


        public int SelectedSportIndex
        {
            get { return selectedSportIndex; }
            set
            {
                selectedSportIndex = value;
                if (selectedSportIndex >= 0)
                {
                   
                    if (EditableSportsman.Sport.SeasonId != Sports.ElementAt(selectedSportIndex).SeasonId) EditableSportsman?.AwardOlympiads?.Clear();
                    OnPropertyChanged("EditWindowComboBoxOlympiads");
                    OnPropertyChanged("EditableSportsmanAwardsOlympiads");
                }
             
            }
        }

        
    }
}
