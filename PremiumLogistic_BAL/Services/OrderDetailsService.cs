namespace PremiumLogistic_BAL.Services;

public class OrderDetailsService : IOrderDetailsService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IStringLocalizer<Resource> _localizer;
    private readonly IEmailSender _emailSender;
    private readonly IConfiguration _configuration;
    public OrderDetailsService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager, IStringLocalizer<Resource> localizer, IEmailSender emailSender, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userManager = userManager;
        _localizer = localizer;
        _emailSender = emailSender;
        _configuration = configuration;
    }
    public async Task AddOrderDetails(AddOrderDetailsDto orderDetailsDto, string email)
    {
        var orderDetails = _mapper.Map<OrderDetails>(orderDetailsDto);
        orderDetails.PartlyPaid = orderDetailsDto.PartlyPaid;
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

    public async Task<MyOrderDetailsByIdDto> MyOrderDetailsById(int id)
    {
        var orderDetails = await _unitOfWork.OrderDetailsRepository.GetByIdAsync(id) ?? throw new NotFoundException(string.Format(_localizer["OrderNotFound"].Value, id));
        var link = await _unitOfWork.ProviderRepository.GetAsync(x => x.Name == orderDetails.Provider);
        var result = _mapper.Map<MyOrderDetailsByIdDto>(orderDetails);
        result.Link = link is null ? "" : link.Link;
        return result;
    }

    public async Task UpdateOrderDetail(int id, UpdateOrderDto updateOrderDetail, string email)
    {
        var orderDetails = await _unitOfWork.OrderDetailsRepository.GetByIdAsync(id) ?? throw new NotFoundException(string.Format(_localizer["OrderNotFound"].Value, id));
        
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
        return details;
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
        return details;
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
        return details;
    }

    public async Task<string> UpdateCarStatus(int id, UpdateCarStatusDto updateCarStatus, string email)
    {
        var order = await _unitOfWork.OrderDetailsRepository.GetByIdAsync(id) ?? throw new NotFoundException(string.Format(_localizer["OrderNotFound"].Value, id));
        string nextStatus = GetNextCarStatus(order.CarStatus);

        switch (nextStatus)
        {
            case "At terminal":
                await ChangeStatusToAtTerminal(order, updateCarStatus, email, nextStatus);
                break;
            case "Booked":
                await ChangeStatusToBooked(order, nextStatus, email);
                break;
            case "Loaded":
                await ChangeStatusToLoaded(order, updateCarStatus, email, nextStatus);
                break;
            case "Delivered":
                await ChangeStatusToDelivered(order, nextStatus, email);
                break;
            default:
                throw new BadRequestException(_localizer["InvalidCarStatus"].Value);
        }
        return nextStatus;
    }

    public async Task<List<FilesDto>> ViewPhotosOfOrder(int id)
    {
        var orders = await _unitOfWork.OrderDetailsRepository.GetByIdAsync(id) ?? throw new NotFoundException(string.Format(_localizer["OrderNotFound"].Value, id));
        string folderPath = orders.PhotosPath;
        if (string.IsNullOrEmpty(folderPath) || !Directory.Exists(folderPath))
            throw new NotFoundException("No photos found for the specified ID.");

        var files = Directory.GetFiles(folderPath);

        if (files.Length == 0)
            throw new NotFoundException("No photos found in the specified folder.");

        var photos = new List<FilesDto>();
        foreach (var file in files)
        {
            var fileName = Path.GetFileName(file);
            var fileBytes = File.ReadAllBytes(file);
            var base64Data = Convert.ToBase64String(fileBytes);
            FilesDto filesDto = new FilesDto()
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
        string folderPath = orders.DocumentsPath;
        if (string.IsNullOrEmpty(folderPath) || !Directory.Exists(folderPath))
            throw new NotFoundException("No documents found for the specified ID.");

        var files = Directory.GetFiles(folderPath);

        if (files.Length == 0)
            throw new NotFoundException("No documents found in the specified folder.");

        var documents = new List<FilesDto>();
        foreach (var file in files)
        {
            var fileName = Path.GetFileName(file);
            var fileBytes = File.ReadAllBytes(file);
            var base64Data = Convert.ToBase64String(fileBytes);
            FilesDto filesDto = new FilesDto()
            {
                Filename = fileName,
                Base64 = base64Data
            };
            documents.Add(filesDto);
        }

        return documents;
    }

    private async Task ChangeStatusToAtTerminal(OrderDetails order, UpdateCarStatusDto updateCarStatus, string email, string nextStatus)
    {
        ValidatePhotos(updateCarStatus);
        
        var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Photos", order.Id.ToString());
        if(!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);

        foreach(var item in updateCarStatus.Photos)
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
            var users = await _userManager.FindByIdAsync(order.UserId);
            IEnumerable<string> emails = new string[] { users.Email };
            Message message = new Message(emails, "Njoftim - Notification", _configuration["GeneralConfigs:AddPhotos"]);
            await _emailSender.SendEmail(message);
        }
        catch
        {

            throw;
        }
        
    }

    private async Task ChangeStatusToBooked(OrderDetails order, string status, string email)
    {
        order.CarStatus = status;
        order.UpdatedBy = email;
        order.UpdatedOn = DateTime.Now;
        _unitOfWork.OrderDetailsRepository.Update(order);
        await _unitOfWork.CommitAsync();
    }

    private async Task ChangeStatusToLoaded(OrderDetails order, UpdateCarStatusDto updatStatusCar, string email, string nextStatus)
    {
        if(string.IsNullOrEmpty(updatStatusCar.TrackingNumber))
            throw new BadRequestException(_localizer["TrackingNrRequired"].Value);
        ValidateDocuments(updatStatusCar);

        var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Documents", order.Id.ToString());
        if(!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);

        foreach (var item in updatStatusCar.Documents)
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

        //Send email
        var users = await _userManager.FindByIdAsync(order.UserId);
        IEnumerable<string> emails = new string[] { users.Email };
        var formFileCollection = new FormFileCollection();
        foreach (var file in updatStatusCar.Documents)
        {
            formFileCollection.Add(file);
        }
        Message message = new Message(emails, "Njoftim - Notification", _configuration["GeneralConfigs:AddDocuments"], formFileCollection);
        await _emailSender.SendEmail(message);
    }

    private async Task ChangeStatusToDelivered(OrderDetails order, string status, string email)
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
        string[] allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };
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

        string[] allowedExtensions = new string[] { ".pdf" };
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
