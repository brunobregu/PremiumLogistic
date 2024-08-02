namespace PremiumLogistic_BAL.Dtos.OrderDetails;

public record OrderDetailsDto(
    string VIN,
    string Make,
    string Model,
    int Year,
    int Lot,
    string Auction,
    string TrackingNumber,
    string Port,
    int ClientTotal,
    string PaymentStatus,
    int PartlyPaid
);
