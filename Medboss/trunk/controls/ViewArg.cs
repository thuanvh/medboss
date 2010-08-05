namespace Nammedia.Medboss.controls
{
    using System;

    public class ViewArg
    {
        public int id;
        public DataType Type;

        public ViewArg(DataType type, int Id)
        {
            this.Type = type;
            this.id = Id;
        }
    }
}
