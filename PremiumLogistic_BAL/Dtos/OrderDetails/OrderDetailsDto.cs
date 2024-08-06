namespace PremiumLogistic_BAL.Dtos.OrderDetails;

public record OrderDetailsDto(
    int Id,
    string VIN,
    string Make,
    string Model,
    int Year,
    int Lot,
    string Auction,
    string TrackingNumber,
    string CarStatus,
    string Port,
    int ClientTotal,
    string PaymentStatus
);
