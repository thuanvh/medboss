namespace Nammedia.Medboss.lib
{
    using Nammedia.Medboss;
    using System;

    public class QuayInfo : IFriendlyName
    {
        private int _maquay;
        private string _ten;

        public string FriendlyName()
        {
            return this._ten;
        }

        public override string ToString()
        {
            return this._ten;
        }

        public int MaQuay
        {
            get
            {
                return this._maquay;
            }
            set
            {
                this._maquay = value;
            }
        }

        public string TenQuay
        {
            get
            {
                return this._ten;
            }
            set
            {
                this._ten = value;
            }
        }
    }
}
