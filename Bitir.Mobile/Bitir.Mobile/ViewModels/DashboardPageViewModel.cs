﻿using Bitir.Mobile.Models;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Bitir.Mobile.ViewModels
{
    /// <summary>
    /// ViewModel for stock overview page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class DashboardPageViewModel : BaseViewModel
    {
        #region Field

        /// <summary>
        /// To store the health care data collection.
        /// </summary>
        private ObservableCollection<HealthCare> healthCareCardItems;

        /// <summary>
        /// To store the health care data collection.
        /// </summary>
        private ObservableCollection<HealthCare> healthCareListItems;

        /// <summary>
        /// To store the heart rate data collection.
        /// </summary>
        private ObservableCollection<ChartModel> heartRateData;

        /// <summary>
        /// To store the calories burned data collection.
        /// </summary>
        private ObservableCollection<ChartModel> caloriesBurnedData;

        /// <summary>
        /// To store the sleep time data collection.
        /// </summary>
        private ObservableCollection<ChartModel> sleepTimeData;

        /// <summary>
        /// To store the water consumed data collection.
        /// </summary>
        private ObservableCollection<ChartModel> waterConsumedData;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="DashboardPageViewModel" /> class.
        /// </summary>
        public DashboardPageViewModel()
        {
            this.GetChartData();
            this.healthCareCardItems = new ObservableCollection<HealthCare>()
            {
                new HealthCare()
                {
                    Category = "Bekleyen",
                    CategoryValue = "4",
                    ChartData = this.heartRateData,
                    BackgroundGradientStart = "#f59083",
                    BackgroundGradientEnd = "#fae188",
                },
                new HealthCare()
                {
                    Category = " Bugün Gönderilen",
                    CategoryValue = "6",
                    ChartData = this.caloriesBurnedData,
                    BackgroundGradientStart = "#ff7272",
                    BackgroundGradientEnd = "#f650c5",
                },
                new HealthCare()
                {
                    Category = "Aylık Toplam",
                    CategoryValue = "51",
                    ChartData = this.sleepTimeData,
                    BackgroundGradientStart = "#5e7cea",
                    BackgroundGradientEnd = "#1dcce3",
                },
                new HealthCare()
                {
                    Category = "Yıllık Toplam",
                    CategoryValue = "125",
                    ChartData = this.waterConsumedData,
                    BackgroundGradientStart = "#255ea6",
                    BackgroundGradientEnd = "#b350d1",
                },
            };

            this.healthCareListItems = new ObservableCollection<HealthCare>()
            {
                new HealthCare()
                {
                    Category = "Günlük Sipariş Durumu",
                    CategoryValue = "6/4",
                    CategoryPercentage = "66%",
                    BackgroundGradientStart = "#cf86ff",
                },
                new HealthCare()
                {
                    Category = "Aylık Sipariş Durumu",
                    CategoryValue = "51",
                    CategoryPercentage = "92%",
                    BackgroundGradientStart = "#8691ff",
                },
                new HealthCare()
                {
                    Category = "Tamamlanmayan",
                    CategoryValue = "5",
                    CategoryPercentage = "4%",
                    BackgroundGradientStart = "#ff9686",
                },
            };

            this.ProfileImage = App.ImageServerPath + "ProfileImage1.png";
            this.MenuCommand = new Command(this.MenuButtonClicked);
            this.ProfileSelectedCommand = new Command(this.ProfileImageClicked);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the profile image path.
        /// </summary>
        public string ProfileImage { get; set; }

        /// <summary>
        /// Gets the health care items collection.
        /// </summary>
        public ObservableCollection<HealthCare> HealthCareCardItems
        {
            get
            {
                return this.healthCareCardItems;
            }

            private set
            {
                this.SetProperty(ref this.healthCareCardItems, value);
            }
        }

        /// <summary>
        /// Gets the health care items collection.
        /// </summary>
        public ObservableCollection<HealthCare> HealthCareListItems
        {
            get
            {
                return this.healthCareListItems;
            }

            private set
            {
                this.SetProperty(ref this.healthCareListItems, value);
            }
        }

        #endregion

        #region Comments

        /// <summary>
        /// Gets or sets the command is executed when the menu button is clicked.
        /// </summary>
        public Command MenuCommand { get; set; }

        /// <summary>
        /// Gets or sets the command is executed when the profile image is clicked.
        /// </summary>
        public Command ProfileSelectedCommand { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Chart Data Collection
        /// </summary>
        private void GetChartData()
        {
            DateTime dateTime = new DateTime(2019, 5, 1);

            this.heartRateData = new ObservableCollection<ChartModel>()
            {
                new ChartModel(dateTime, 15),
                new ChartModel(dateTime.AddMonths(1), 20),
                new ChartModel(dateTime.AddMonths(2), 17),
                new ChartModel(dateTime.AddMonths(3), 23),
                new ChartModel(dateTime.AddMonths(4), 18),
                new ChartModel(dateTime.AddMonths(5), 25),
                new ChartModel(dateTime.AddMonths(6), 19),
                new ChartModel(dateTime.AddMonths(7), 21),
            };

            this.caloriesBurnedData = new ObservableCollection<ChartModel>()
            {
                new ChartModel(dateTime, 940),
                new ChartModel(dateTime.AddMonths(1), 960),
                new ChartModel(dateTime.AddMonths(2), 942),
                new ChartModel(dateTime.AddMonths(3), 957),
                new ChartModel(dateTime.AddMonths(4), 940),
                new ChartModel(dateTime.AddMonths(5), 942),
            };

            this.sleepTimeData = new ObservableCollection<ChartModel>()
            {
                new ChartModel(dateTime, 7.8),
                new ChartModel(dateTime.AddMonths(1), 7.2),
                new ChartModel(dateTime.AddMonths(2), 8.0),
                new ChartModel(dateTime.AddMonths(3), 6.8),
                new ChartModel(dateTime.AddMonths(4), 7.6),
                new ChartModel(dateTime.AddMonths(5), 7.0),
                new ChartModel(dateTime.AddMonths(6), 7.5),
            };

            this.waterConsumedData = new ObservableCollection<ChartModel>()
            {
                new ChartModel(dateTime, 36),
                new ChartModel(dateTime.AddMonths(1), 41),
                new ChartModel(dateTime.AddMonths(2), 38),
                new ChartModel(dateTime.AddMonths(3), 41),
                new ChartModel(dateTime.AddMonths(4), 35),
                new ChartModel(dateTime.AddMonths(5), 37),
                new ChartModel(dateTime.AddMonths(6), 38),
                new ChartModel(dateTime.AddMonths(7), 36),
            };
        }

        /// <summary>
        /// Invoked when the menu button is clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void MenuButtonClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the profile image is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void ProfileImageClicked(object obj)
        {
            // Do something
        }

        #endregion
    }
}
