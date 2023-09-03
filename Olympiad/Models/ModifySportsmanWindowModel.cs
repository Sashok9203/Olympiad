using data_access.Entities;
using data_access.Repositories;
using Google.Protobuf.WellKnownTypes;
using Microsoft.VisualBasic;
using Microsoft.Win32;
using Olympiad;
using OlympiadWPF.Models.CommonClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media.TextFormatting;

namespace OlympiadWPF.Models
{
    internal partial class OlympiadDBModel
    {
        private ModifySpotrsmanWindow? SportsmanWindow;

        private DateTime bBirthday;

        private Sport? bSport;

        string? bPhotoPath;

        private List<Gender>? genders = null;

        private List<Award>? awards = null;

        private Sportsman getNewSportsman()
        {
            Sportsman newSportsman = new()
            {
                Name = BName,
                Surname = BSurname,
                Country = BCountry,
                Birthday = BBirthday,
                Sport = BSport,
                PhotoPath = BPhotoPath,
                Gender = BGender
            };
            if (BAwardOlympiads.Count > 0)
                foreach (var item in BAwardOlympiads)
                    newSportsman.AwardOlympiads.Add(item);
            return newSportsman;
        }

        private void setBValues(Sportsman? spotrsman = null)
        {
            BName = spotrsman?.Name;
            BSurname = spotrsman?.Surname;
            BCountry = spotrsman?.Country;
            BSport = spotrsman?.Sport;
            BPhotoPath = spotrsman?.PhotoPath;
            BGender = spotrsman?.Gender;
            BBirthday = spotrsman?.Birthday ?? new DateTime(2000,1,1);
            BAwardOlympiads.Clear();
            if (spotrsman != null)
                foreach (var item in spotrsman.AwardOlympiads)
                    BAwardOlympiads.Add(item);
        }

        private void setFromBValues(Sportsman spotrsman )
        {
            spotrsman.Name = BName;
            spotrsman.Surname = BSurname;
            spotrsman.Country = BCountry;
            spotrsman.Sport = BSport;
            spotrsman.PhotoPath = BPhotoPath;
            spotrsman.Gender  = BGender;
            spotrsman.Birthday = BBirthday;
            spotrsman.AwardOlympiads.Clear();
             foreach (var item in BAwardOlympiads)
                spotrsman.AwardOlympiads.Add(item);
        }



        

        private void deleteContextMenu(object o)
        {
            BAwardOlympiads.Remove(BAwardOlympiad);
            OnPropertyChanged("EditWindowComboBoxOlympiads");
            OnPropertyChanged("OlympiadSportsmans");
        }

        private void addAwardOlympiad(object o)
        {
            BAwardOlympiads.Add(new() { Olympiad = BOlympiad, Award = BAward });
            BOlympiad = null;
            BAward = null;
            OnPropertyChanged("BOlympiad");
            OnPropertyChanged("BAward");
            OnPropertyChanged("EditWindowComboBoxOlympiads");
        }

        private void getPhoto(object o)
        {
            OpenFileDialog openFileDialog = new() { Filter = "Image Files (*.png;*.jpeg;*.jpg;)|*.jpg;*.jpeg;*.png;" };
            openFileDialog.ShowDialog();
            BPhotoPath =  openFileDialog.FileName;
            OnPropertyChanged("BPhotoPath");
        }

        private void ModifySportsman(bool isNew)
        {
            if (!isNew) setBValues(BSportsmanForEdit);
            else setBValues(); 
            countries?.RemoveAt(0);
            sports?.RemoveAt(0);
            olmp?.RemoveAt(0);
            SportsmanWindow = new() { DataContext = this };
            if (SportsmanWindow.ShowDialog() == true)
            {
                if (isNew) unitOW.Sportsmans.Insert(getNewSportsman());
                else
                {
                    setFromBValues(BSportsmanForEdit);
                    unitOW.Sportsmans.Update(BSportsmanForEdit);
                }
                unitOW.Save();
                sptms = null;
                spAwOl = null;
                if (!isNew) OnPropertyChanged("AllSportsmans");
                OnPropertyChanged("Sportsmans");
                OnPropertyChanged("CountryResult");
                OnPropertyChanged("MedalTable");
            }
            countries?.Insert(0, all_Countries);
            sports?.Insert(0, all_Sports);
            olmp?.Insert(0, all_Olympiads);
        }

        public IEnumerable<Award> Awards => awards ??= unitOW.Awards.Get().ToList();

        public IEnumerable<Gender> Genders => genders ??= unitOW.Genders.Get().ToList();

        public IEnumerable<Olympiad_>? EditWindowComboBoxOlympiads => Olympiads?.Where(x => x.SeasonId == BSport?.Season.Id 
                                                                                                    && !BAwardOlympiads.Any(y=>y.Olympiad.Id == x.Id)
                                                                                                    && x.Year - BBirthday.Year >= 16 
                                                                                                    && (x.Year - BBirthday.Year <= 39));

        public RelayCommand Save => new((o) =>saveButton(o),(o=> !string.IsNullOrEmpty(BName) 
                                                                                  && !string.IsNullOrEmpty(BSurname)
                                                                                  && BSport!=null 
                                                                                  && BCountry!=null
                                                                                  && BGender!= null));

        public RelayCommand Add => new((o) => addAwardOlympiad(o),(o)=> BOlympiad != null);

        public RelayCommand Delete => new((o) => deleteContextMenu(o),(o)=> BAwardOlympiad != null);

        public RelayCommand GetPhoto => new((o) => getPhoto(o));

        
        //Binding properties
        public Award? BAward { get; set; }

        public string? BName { get; set; }
       
        public string? BSurname { get; set; }

        public string? BPhotoPath
        {
            get =>  string.IsNullOrEmpty(bPhotoPath) ? "Images/Sportsmans/NoPhoto.png" : bPhotoPath;
            set => bPhotoPath = value;
        }

        public DateTime BBirthday 
        {
            get =>bBirthday;
            set
            {
                bBirthday = value;
                OnPropertyChanged("EditWindowComboBoxOlympiads");
                if (BAwardOlympiads.Count > 0)
                {
                    List<SportsmanAwardOlympiad> temp = new();
                    foreach(var item in BAwardOlympiads)
                        if (item.Olympiad?.Year - bBirthday.Year >= 16 && (item.Olympiad?.Year - bBirthday.Year <= 39)) temp.Add(item);
                    if (temp.Count > 0)
                        foreach (var item in temp)
                            BAwardOlympiads.Remove(item);
                }
            }
        }

        public Olympiad_? BOlympiad { get; set; }

        public Sportsman? BSportsmanForEdit { get; set; }

        public Country? BCountry { get; set; }

        public Gender? BGender { get; set; }

        public ObservableCollection<SportsmanAwardOlympiad> BAwardOlympiads { get; set; } = new();

        public Sport? BSport
        {
            get=> bSport; 
            set
            {
                if (value?.SeasonId != bSport?.SeasonId)
                        BAwardOlympiads?.Clear();
                bSport = value;
                OnPropertyChanged("EditWindowComboBoxOlympiads");
            }
        }

        public SportsmanAwardOlympiad? BAwardOlympiad { get; set; }

    }
}
