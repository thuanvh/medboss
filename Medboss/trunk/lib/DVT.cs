namespace Nammedia.Medboss.lib
{
    using System;

    public class DVT
    {
        private int _madv;
        private string _tendv;

        public override string ToString()
        {
            return this._tendv;
        }

        public int MaDVT
        {
            get
            {
                return this._madv;
            }
            set
            {
                this._madv = value;
            }
        }

        public string TenDV
        {
            get
            {
                return this._tendv;
            }
            set
            {
                this._tendv = value;
            }
        }
    }
}
