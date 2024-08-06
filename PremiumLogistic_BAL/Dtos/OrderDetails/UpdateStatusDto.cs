namespace PremiumLogistic_BAL.Dtos.OrderDetails
{
    public class UpdateStatusDto
    {
        public string CarStatus { get; set; }
        public string TrackingNumber { get; set; }
        public List<UploadDocumentsDTO> UploadDocumentsDTOs { get; set; }
    }
}
