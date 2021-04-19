using System;

namespace AltaPay.Service
{
    public enum AuthType
    {
		payment, paymentAndCapture, subscription, subscriptionAndCharge, subscriptionAndReserve, verifyCard
    }
}
