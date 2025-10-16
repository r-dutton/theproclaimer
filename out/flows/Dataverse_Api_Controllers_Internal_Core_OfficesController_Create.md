[web] POST /api/internal/offices/  (Dataverse.Api.Controllers.Internal.Core.OfficesController.Create)  [L58–L64] status=201 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request CreateOrUpdateOfficeCommand [L62]
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

