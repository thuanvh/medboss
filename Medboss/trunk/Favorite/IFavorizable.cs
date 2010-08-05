namespace Nammedia.Medboss.Favorite
{
    using System;

    internal interface IFavorizable
    {
        ParamNode[] getParams();
        void setParams(ParamNode[] paras);
    }
}
