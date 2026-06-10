using System;

namespace Pokladna.Events
{
    internal enum AppActionType
    {
        CloseWindow,
        OpenPriceList,
        OpenEployeesList,
        OpenGiftCards,
        OpenCoupons,
        OpenTransactions,
        OpenSettings,
        AddRecord,
        EditRecord,
        RemoveRecord

    }

    internal class AppActionEventArgs : EventArgs
    {
        public AppActionType Action { get; }
        public object Payload { get; }

        public AppActionEventArgs(AppActionType action, object payload = null)
        {
            Action = action;
            Payload = payload;
        }
    }
}