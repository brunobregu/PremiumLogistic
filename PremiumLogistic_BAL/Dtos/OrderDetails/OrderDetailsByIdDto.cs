namespace PremiumLogistic_BAL.Dtos.OrderDetails;

public record OrderDetailsByIdDto(
    int id,
    string VIN,
    string Make,
    string Model,
    int Year,
    int Lot,
    string DspOrderID,
    string Port,
    int InlandCargoloop,
    int OcCargoloop,
    int Broker,
    int InlandDspch,
    int OcCost,
    int Storage,
    string PaymentStatus,
    int PartlyPaid
);
