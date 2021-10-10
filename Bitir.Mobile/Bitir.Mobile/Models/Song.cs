using System.Runtime.Serialization;
using Xamarin.Forms.Internals;

namespace Bitir.Mobile.Models
{
    /// <summary>
    /// Model for the Songs play list page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class Song
    {
        #region Field

        private string songImage;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name of an song.
        /// </summary>
        public string SongName { get; set; }

        /// <summary>
        /// Gets or sets the composer.
        /// </summary>
        public string Composer { get; set; }

        /// <summary>
        /// Gets or sets the image of an item.
        /// </summary>
        public string SongImage
        {
            get
            {
                return App.ImageServerPath + this.songImage;
            }

            set
            {
                this.songImage = value;
            }
        }

        #endregion
    }
}
