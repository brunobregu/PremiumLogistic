namespace PremiumLogistic_BAL.Dtos.LocalTransportation;

public record LocalTransportationDto(
    int Id,
    string AuctionLocation,
    string Auction,
    string City,
    string State,
    string Zip,
    decimal Savannah_GA,
    decimal Elizabeth_NJ,
    decimal Houston_TX,
    decimal LosAngeles_CA,
    decimal Indianapolis_IN
);
