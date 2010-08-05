using Nammedia.Medboss.Favorite;
using System;
using System.Runtime.CompilerServices;
namespace Nammedia.Medboss
{


    public interface IOperator
    {
        event ValidateHandler DataInvalid;

        event DeleteFinishHandler DeleteFinished;

        event f0_0 DeleteUnfinished;

        event InsertFinishHandler InsertFinished;

        event f0_0 InsertUnfinished;

        event SaveFunc SaveFired;

        event UnfoundHandler Unfound;

        event UpdateFinishHandler UpdateFinished;

        event f0_0 UpdateUnfinished;

        void Clear();
        void Insert();
        void RefreshAC();
    }
}
