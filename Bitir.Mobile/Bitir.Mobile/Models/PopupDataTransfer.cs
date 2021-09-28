namespace Bitir.Mobile.Models
{
    public class PopupDataTransfer
    {
        public PopupDataTransfer(string value,string placeHolder, bool isPassword = false)
        {
            this.Value = value;
            this.IsPassword = isPassword;
            this.PlaceHolder = placeHolder;
        }
        public string Value { get; set; }
        public bool IsPassword { get; set; }

        public string PlaceHolder { get; set; }
    }
}
