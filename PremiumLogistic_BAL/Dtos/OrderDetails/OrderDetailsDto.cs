namespace PremiumLogistic_BAL.Dtos.OrderDetails;

public record OrderDetailsDto(
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
    int Storage,
    string PaymentStatus,
    int PartlyPaid
);
