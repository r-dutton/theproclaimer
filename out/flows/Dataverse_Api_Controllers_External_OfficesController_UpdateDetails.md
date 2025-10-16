[web] PUT /  (Dataverse.Api.Controllers.External.OfficesController.UpdateDetails)  [L61–L65] status=200
  └─ sends_request CreateOrUpdateOfficeCommand [L64]
    └─ handled_by Dataverse.ApplicationService.Commands.Firms.CreateOrUpdateOfficeCommandHandler.Handle [L38–L78]
      └─ maps_to OfficeDto [L76]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettingsAsync [L53]
          └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync [L18-L97]
      └─ uses_service IControlledRepository<Office>
        └─ method WriteQuery [L60]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method Map [L76]
          └─ ... (no implementation details available)

