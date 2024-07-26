namespace PremiumLogistic_DomainModels;

public enum PaymentStatus
{
    NotPaid = 1,
    PartlyPaid,
    Paid
}

public enum CarStatus
{
    Dispatch = 1,
    AtTerminal,
    Booked,
    Loaded,
    Delivered
}
