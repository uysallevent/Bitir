﻿using System;
using Xamarin.Forms.Internals;

namespace Bitir.Mobile.Models
{
    /// <summary>
    /// Model for chart template.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class ChartModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="ChartModel" /> class.
        /// </summary>
        /// <param name="dateTime">The date time</param>
        /// <param name="value">The value</param>
        public ChartModel(DateTime dateTime, double value)
        {
            this.DateTimeXValue = dateTime;
            this.YValue = value;
        }

        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the property that has been bound the chart X value.
        /// </summary>
        public DateTime DateTimeXValue { get; set; }

        /// <summary>
        /// Gets or sets the property that has been bound the chart Y value.
        /// </summary>       
        public double YValue { get; set; }

        #endregion
    }
}
