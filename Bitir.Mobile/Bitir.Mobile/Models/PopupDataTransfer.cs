namespace Bitir.Mobile.Models
{
    public class PopupDataTransfer
    {
        public PopupDataTransfer(string value, string placeHolder, string propertyName, bool isPassword = false)
        {
            this.Value = value;
            this.IsPassword = isPassword;
            this.PlaceHolder = placeHolder;
            this.PropertyName = propertyName;
        }
        public string Value { get; set; }
        public bool IsPassword { get; set; }
        public string PropertyName { get; set; }

        public string PlaceHolder { get; set; }
    }
}
