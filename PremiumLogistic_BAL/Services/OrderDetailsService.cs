namespace PremiumLogistic_BAL.Services;

public class OrderDetailsService
(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    UserManager<ApplicationUser> userManager,
    IStringLocalizer<Resource> localizer,
    IEmailSender emailSender,
    IConfiguration configuration
) : IOrderDetailsService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IStringLocalizer<Resource> _localizer = localizer;
    private readonly IEmailSender _emailSender = emailSender;
    private readonly IConfiguration _configuration = configuration;

    public async Task AddOrderDetails(AddOrderDetailsDto orderDetailsDto, string email)
    {
        if (orderDetailsDto.PaymentStatus != "Partly Paid" && orderDetailsDto.PartlyPaid != 0)
            throw new BadRequestException("Partly Paid should be 0!");
        if (orderDetailsDto.PaymentStatus == "Partly Paid" && orderDetailsDto.PartlyPaid == 0)
            throw new BadRequestException("Partly Paid cannot be 0!");

        var orderDetails = _mapper.Map<OrderDetails>(orderDetailsDto);
        orderDetails.CreatedBy = email;
        orderDetails.CarStatus = "Dispatch";
        _unitOfWork.OrderDetailsRepository.Insert(orderDetails);
        await _unitOfWork.CommitAsync();
    }

    public async Task<List<OrderDetailsDto>> MyOrders(string email)
    {
        var user = await _userManager.FindByEmailAsync(email) ?? throw new NotFoundException(string.Format(_localizer["EmailNotFound"].Value, email));
        var orderDetails = await _unitOfWork.OrderDetailsRepository.GetManyAsync(x => x.UserId == user.Id);
        var result = _mapper.Map<List<OrderDetailsDto>>(orderDetails);
        return result;
    }

    public async Task<List<AllOrderDetailsDto>> GetAllOrderDetails()
    {
        var orderDetails = await _unitOfWork.OrderDetailsRepository.IncludeAsync(c => c.User);
        var result = _mapper.Map<List<AllOrderDetailsDto>>(orderDetails);
        return result;
    }

    public async Task<List<ClientsWithOrdersDto>> ClientsWithOrders()
    {
        var orderDetails = await _unitOfWork.OrderDetailsRepository.IncludeAsync(c => c.User);
        var result = _mapper.Map<List<ClientsWithOrdersDto>>(orderDetails).DistinctBy(u => u.UserId).ToList();
        return result;
    }

    public async Task<List<AllOrderDetailsDto>> GetAllOrderDetailsByClient(string userId)
    {
        var orderDetails = await _unitOfWork.OrderDetailsRepository.GetManyAsync(x => x.UserId == userId);
        var result = _mapper.Map<List<AllOrderDetailsDto>>(orderDetails);
        return result;
    }

    public async Task<OrderDetailsByIdDto> GetOrderDetailsById(int id)
    {
        var orderDetails = await _unitOfWork.OrderDetailsRepository.IncludeAsync(c => c.User);
        var ordersById = orderDetails.Where(x => x.Id== id).FirstOrDefault() ?? throw new NotFoundException(string.Format(_localizer["OrderNotFound"].Value, id));
        var result = _mapper.Map<OrderDetailsByIdDto>(ordersById);
        return result;
    }

    public async Task<AdminOrderDetailsByIdDto> GetOrderDetailsByIdForAdmin(int id)
    {
        var orderDetails = await _unitOfWork.OrderDetailsRepository.IncludeAsync(c => c.User);
        var ordersById = orderDetails.Where(x => x.Id == id).FirstOrDefault() ?? throw new NotFoundException(string.Format(_localizer["OrderNotFound"].Value, id));
        var result = _mapper.Map<AdminOrderDetailsByIdDto>(ordersById);
        return result;
    }

    public async Task<MyOrderDetailsByIdDto> MyOrderDetailsById(int id, string email)
    {
        var orderDetails = await _unitOfWork.OrderDetailsRepository.GetByIdAsync(id) ?? throw new NotFoundException(string.Format(_localizer["OrderNotFound"].Value, id));
        var user = await _userManager.FindByEmailAsync(email) ?? throw new NotFoundException(string.Format(_localizer["EmailNotFound"].Value, email));
        if (orderDetails.UserId != user.Id)
            throw new BadRequestException("You don't have permission to get data!");

        var link = await _unitOfWork.ProviderRepository.GetAsync(x => x.Name == orderDetails.Provider);
        var result = _mapper.Map<MyOrderDetailsByIdDto>(orderDetails);
        result.Link = link is null ? "" : link.Link;
        return result;
    }

    public async Task UpdateOrderDetail(int id, UpdateOrderDto updateOrderDetail, string email)
    {
        var orderDetails = await _unitOfWork.OrderDetailsRepository.GetByIdAsync(id) ?? throw new NotFoundException(string.Format(_localizer["OrderNotFound"].Value, id));
        if (updateOrderDetail.PaymentStatus != "Partly Paid" && updateOrderDetail.PartlyPaid != 0)
            throw new BadRequestException("Partly Paid should be 0!");
        if (updateOrderDetail.PaymentStatus == "Partly Paid" && updateOrderDetail.PartlyPaid == 0)
            throw new BadRequestException("Partly Paid cannot be 0!");

        orderDetails.UpdatedOn = DateTime.Now;
        orderDetails.UpdatedBy = email;
        orderDetails.VIN = updateOrderDetail.VIN;
        orderDetails.Make = updateOrderDetail.Make;
        orderDetails.Model = updateOrderDetail.Model;
        orderDetails.Year = updateOrderDetail.Year;
        orderDetails.Lot = updateOrderDetail.Lot;
        orderDetails.OrderID = updateOrderDetail.OrderID;
        orderDetails.Auction = updateOrderDetail.Auction;
        orderDetails.TrackingNumber = updateOrderDetail.TrackingNumber;
        orderDetails.Port = updateOrderDetail.Port;
        orderDetails.InlandPrice = updateOrderDetail.InlandPrice;
        orderDetails.OceanPrice = updateOrderDetail.OceanPrice;
        orderDetails.Broker = updateOrderDetail.Broker;
        orderDetails.ClientStorage = updateOrderDetail.ClientStorage;
        orderDetails.CarPrice = updateOrderDetail.CarPrice;
        orderDetails.ClientTotal = updateOrderDetail.ClientTotal;
        orderDetails.InlandCost = updateOrderDetail.InlandCost;
        orderDetails.OceanCost = updateOrderDetail.OceanCost;
        orderDetails.StorageCost = updateOrderDetail.StorageCost;
        orderDetails.TotalCost = updateOrderDetail.TotalCost;
        orderDetails.Profit = updateOrderDetail.Profit;
        orderDetails.PaymentStatus = updateOrderDetail.PaymentStatus;
        orderDetails.PartlyPaid = updateOrderDetail.PartlyPaid;
        orderDetails.UserId = updateOrderDetail.UserId;
        orderDetails.Provider = updateOrderDetail.Provider;
        orderDetails.ToBePaid = updateOrderDetail.ToBePaid;

        _unitOfWork.OrderDetailsRepository.Update(orderDetails);
        await _unitOfWork.CommitAsync();
    }

    public async Task DeleteOrderDetail(int id, string email)
    {
        var orderDetails = await _unitOfWork.OrderDetailsRepository.GetByIdAsync(id) ?? throw new NotFoundException(string.Format(_localizer["OrderNotFound"].Value, id));
        orderDetails.Invalidated = true;
        orderDetails.UpdatedBy = email;
        orderDetails.UpdatedOn = DateTime.Now;

        _unitOfWork.OrderDetailsRepository.Update(orderDetails);
        await _unitOfWork.CommitAsync();
    }

    public async Task<DetailsDto> Details()
    {
        var alldetails = await _unitOfWork.OrderDetailsRepository.GetAllAsync();
        var details = alldetails.GroupBy(o => 1)
                        .Select(g => new DetailsDto
                        {
                            NumberOfOrders = g.Count(),
                            ClientTotal = g.Sum(o => o.ClientTotal),
                            ToBePaid = g.Sum(o => o.ToBePaid)
                        })
                        .FirstOrDefault();
        return details ?? new DetailsDto() { ClientTotal = 0, NumberOfOrders = 0, ToBePaid = 0 };
    }

    public async Task<DetailsDto> DetailsByClient(string userId)
    {
        var alldetails = await _unitOfWork.OrderDetailsRepository.GetManyAsync(x => x.UserId == userId);
        var details = alldetails.GroupBy(o => 1)
                        .Select(g => new DetailsDto
                        {
                            NumberOfOrders = g.Count(),
                            ClientTotal = g.Sum(o => o.ClientTotal),
                            ToBePaid = g.Sum(o => o.ToBePaid)
                        })
                        .FirstOrDefault();
        return details ?? new DetailsDto() { ClientTotal = 0, NumberOfOrders = 0, ToBePaid = 0};
    }

    public async Task<DetailsDto> MyDetails(string email)
    {
        var user = await _userManager.FindByEmailAsync(email) ?? throw new NotFoundException(string.Format(_localizer["EmailNotFound"].Value, email));
        var myDetails = await _unitOfWork.OrderDetailsRepository.GetManyAsync(x => x.UserId == user.Id);
        var details = myDetails.GroupBy(o => 1)
                        .Select(g => new DetailsDto
                        {
                            NumberOfOrders = g.Count(),
                            ClientTotal = g.Sum(o => o.ClientTotal),
                            ToBePaid = g.Sum(o => o.ToBePaid)
                        })
                        .FirstOrDefault();
        return details ?? new DetailsDto() { ClientTotal = 0, NumberOfOrders = 0, ToBePaid = 0 };
    }

    public async Task<string> UpdateCarStatus(int id, UpdateCarStatusDto updateCarStatus, string email)
    {
        var order = await _unitOfWork.OrderDetailsRepository.GetByIdAsync(id) ?? throw new NotFoundException(string.Format(_localizer["OrderNotFound"].Value, id));
        string nextStatus = GetNextCarStatus(order.CarStatus);
        string response = nextStatus switch
        {
            "At terminal" => await ChangeStatusToAtTerminal(order, updateCarStatus, email, nextStatus),
            "Booked" => await ChangeStatusToBooked(order, nextStatus, email),
            "Loaded" => await ChangeStatusToLoaded(order, updateCarStatus, email, nextStatus),
            "Delivered" => await ChangeStatusToDelivered(order, nextStatus, email),
            _ => throw new BadRequestException(_localizer["InvalidCarStatus"].Value),
        };
        return response;
    }

    public async Task<List<FilesDto>> ViewPhotosOfOrder(int id)
    {
        var orders = await _unitOfWork.OrderDetailsRepository.GetByIdAsync(id) ?? throw new NotFoundException(string.Format(_localizer["OrderNotFound"].Value, id));
        string folderPath = orders.PhotosPath ?? throw new NotFoundException("No photos found!");
        if (string.IsNullOrEmpty(folderPath) || !Directory.Exists(folderPath))
            throw new NotFoundException("No photos found!");

        var files = Directory.GetFiles(folderPath);

        if (files.Length == 0)
            throw new NotFoundException("No photos found!");

        var photos = new List<FilesDto>();
        foreach (var file in files)
        {
            var fileName = Path.GetFileName(file);
            var fileBytes = File.ReadAllBytes(file);
            var base64Data = Convert.ToBase64String(fileBytes);
            FilesDto filesDto = new()
            {
                Filename = fileName,
                Base64 = base64Data
            };
            photos.Add(filesDto);
        }

        return photos;
    }

    public async Task<List<FilesDto>> ViewDocumentsOfOrder(int id)
    {
        var orders = await _unitOfWork.OrderDetailsRepository.GetByIdAsync(id) ?? throw new NotFoundException(string.Format(_localizer["OrderNotFound"].Value, id));
        string folderPath = orders.DocumentsPath ?? throw new NotFoundException("No documents found!");
        if (string.IsNullOrEmpty(folderPath) || !Directory.Exists(folderPath))
            throw new NotFoundException("No documents found!");

        var files = Directory.GetFiles(folderPath);

        if (files.Length == 0)
            throw new NotFoundException("No documents found!");

        var documents = new List<FilesDto>();
        foreach (var file in files)
        {
            var fileName = Path.GetFileName(file);
            var fileBytes = File.ReadAllBytes(file);
            var base64Data = Convert.ToBase64String(fileBytes);
            FilesDto filesDto = new()
            {
                Filename = fileName,
                Base64 = base64Data
            };
            documents.Add(filesDto);
        }

        return documents;
    }

    private async Task<string> ChangeStatusToAtTerminal(OrderDetails order, UpdateCarStatusDto updateCarStatus, string email, string nextStatus)
    {
        ValidatePhotos(updateCarStatus);
        var path = _configuration["GeneralConfigs:PhotoPath"] ?? throw new NotFoundException("Path cannot find!");
        var uploadPath = Path.Combine(path, order.Id.ToString());
        if(!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);

        var photos = updateCarStatus.Photos ?? throw new NotFoundException("No photos uploaded!");
        foreach (var item in photos)
        {
            var fileName = Path.GetFileName(item.FileName);
            var filePath = Path.Combine(uploadPath, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await item.CopyToAsync(stream);
        }

        order.PhotosPath = uploadPath;
        order.CarStatus = nextStatus;
        order.UpdatedBy = email;
        order.UpdatedOn = DateTime.Now;
        _unitOfWork.OrderDetailsRepository.Update(order);
        await _unitOfWork.CommitAsync();
        try
        {
            //Send email
            var users = await _userManager.FindByIdAsync(order.UserId) ?? throw new NotFoundException("User not found!");
            string userEmail = users.Email ?? throw new NotFoundException("Email not found!");
            IEnumerable<string> emails = [userEmail];
            string addPhotos = _configuration["GeneralConfigs:AddPhotos"] ?? throw new NotFoundException("Cannot get the error message!");
            Message message = new(emails, "Njoftim - Notification", addPhotos);
            await _emailSender.SendEmail(message);
        }
        catch
        {
            return $"Status updated successfully to {nextStatus}, but failed to send email with photos!";
        }

        return nextStatus;
    }

    private async Task<string> ChangeStatusToBooked(OrderDetails order, string status, string email)
    {
        order.CarStatus = status;
        order.UpdatedBy = email;
        order.UpdatedOn = DateTime.Now;
        _unitOfWork.OrderDetailsRepository.Update(order);
        await _unitOfWork.CommitAsync();
        return status;
    }

    private async Task<string> ChangeStatusToLoaded(OrderDetails order, UpdateCarStatusDto updatStatusCar, string email, string nextStatus)
    {
        if(string.IsNullOrEmpty(updatStatusCar.TrackingNumber))
            throw new BadRequestException(_localizer["TrackingNrRequired"].Value);
        ValidateDocuments(updatStatusCar);

        var docPath = _configuration["GeneralConfigs:DocumentPath"] ?? throw new NotFoundException("Documents path not found!");
        var uploadPath = Path.Combine(docPath, order.Id.ToString());
        if(!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);

        var documents = updatStatusCar.Documents ?? throw new NotFoundException("Documents not found!");
        foreach (var item in documents)
        {
            var fileName = Path.GetFileName(item.FileName);
            var filePath = Path.Combine(uploadPath, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await item.CopyToAsync(stream);
        }
        order.CarStatus = nextStatus;
        order.TrackingNumber = updatStatusCar.TrackingNumber;
        order.DocumentsPath = uploadPath;
        order.UpdatedBy = email;
        order.UpdatedOn = DateTime.Now;
        _unitOfWork.OrderDetailsRepository.Update(order);
        await _unitOfWork.CommitAsync();

        try
        {
            //Send email
            var users = await _userManager.FindByIdAsync(order.UserId) ?? throw new NotFoundException("User not found!");
            var userEmail = users.Email ?? throw new NotFoundException("User email not found!");
            IEnumerable<string> emails = [userEmail];
            var formFileCollection = new FormFileCollection();
            foreach (var file in updatStatusCar.Documents)
            {
                formFileCollection.Add(file);
            }
            var addDocs = _configuration["GeneralConfigs:AddDocuments"] ?? throw new NotFoundException("Message not found!");
            Message message = new(emails, "Njoftim - Notification", addDocs, formFileCollection);
            await _emailSender.SendEmail(message);
        }
        catch (Exception)
        {
            return $"Status updated successfully to {nextStatus}, but failed to send email with documents!";
        }
        return nextStatus;
    }

    private async Task<string> ChangeStatusToDelivered(OrderDetails order, string status, string email)
    {
        //delete photos and docs
        if(Directory.Exists(order.DocumentsPath))
            Directory.Delete(order.DocumentsPath, recursive: true);
        if(Directory.Exists(order.PhotosPath))
            Directory.Delete(order.PhotosPath, recursive: true);
        

        order.CarStatus = status;
        order.UpdatedBy = email;
        order.UpdatedOn = DateTime.Now;
        order.PhotosPath = null;
        order.DocumentsPath = null;
        _unitOfWork.OrderDetailsRepository.Update(order);
        await _unitOfWork.CommitAsync();
        return status;
    }

    private string GetNextCarStatus(string currentStatus)
    {
        var statusTransitions = new Dictionary<string, string>
        {
            { "Dispatch", "At terminal" },
            { "At terminal", "Booked" },
            { "Booked", "Loaded" },
            { "Loaded", "Delivered" }
        };

        if (statusTransitions.TryGetValue(currentStatus, out string nextStatus))
            return nextStatus;

        throw new BadRequestException(_localizer["InvalidCarStatus"].Value);
    }

    private void ValidatePhotos(UpdateCarStatusDto updateCarStatus)
    {
        if (updateCarStatus.Photos is null)
            throw new BadRequestException(_localizer["PhotoRequired"].Value);
        if (updateCarStatus.Photos.Count > 10)
            throw new BadRequestException(_localizer["MaxPhotoRequired"].Value);
        if(updateCarStatus.Photos.Sum(file => file.Length) > 5 * 1024 * 1024)
            throw new BadRequestException(_localizer["MaxPhotosSize"].Value);
        string[] allowedExtensions = [".jpg", ".jpeg", ".png"];
        foreach (var file in updateCarStatus.Photos)
        {
            var extension = Path.GetExtension(file.FileName).ToLower();
            if (!allowedExtensions.Contains(extension))
                throw new BadRequestException(string.Format(_localizer["PhotoAllowedFormat"].Value, file.FileName));
        }  
    }

    private void ValidateDocuments(UpdateCarStatusDto updateCarStatus)
    {
        if(updateCarStatus.Documents is null || updateCarStatus.Documents.Count != 2)
            throw new BadRequestException(_localizer["DocsNrUpload"].Value);
        if (updateCarStatus.Documents.Sum(file => file.Length) > 5 * 1024 * 1024)
            throw new BadRequestException(_localizer["DocsMaxSize"].Value);

        string[] allowedExtensions = [".pdf"];
        foreach (var file in updateCarStatus.Documents)
        {
            var extension = Path.GetExtension(file.FileName).ToLower();
            if (!allowedExtensions.Contains(extension))
            {
                throw new BadRequestException(string.Format(_localizer["DocsAllowedFormat"].Value, file.FileName));
            }
        }
    }
}
