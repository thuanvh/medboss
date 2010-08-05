namespace Nammedia.Medboss.lib
{
    using System;

    public class ThuocTraoDoi
    {
        private Nammedia.Medboss.lib.DVT _dvt = new Nammedia.Medboss.lib.DVT();
        private int _maThuocTraoDoi;
        private int _tile;

        public Nammedia.Medboss.lib.DVT DVT
        {
            get
            {
                return this._dvt;
            }
            set
            {
                this._dvt = value;
            }
        }

        public int MaThuocTraoDoi
        {
            get
            {
                return this._maThuocTraoDoi;
            }
            set
            {
                this._maThuocTraoDoi = value;
            }
        }

        public int TiLe
        {
            get
            {
                return this._tile;
            }
            set
            {
                this._tile = value;
            }
        }
    }
}
