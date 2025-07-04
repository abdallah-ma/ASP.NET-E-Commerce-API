﻿using System.Runtime.Serialization;


namespace PaymentService.Models
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,
        
        [EnumMember(Value ="Payment Received")]
        PaymentReceived,

        [EnumMember(Value = "Payment Failed")]
        PaymentFailed
    }
}
