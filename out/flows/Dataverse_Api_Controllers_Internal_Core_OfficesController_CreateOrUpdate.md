[web] POST /api/internal/offices/create-or-update  (Dataverse.Api.Controllers.Internal.Core.OfficesController.CreateOrUpdate)  [L69–L76] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request CreateOrUpdateOfficeCommand [L72]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Firms.CreateOrUpdateOfficeCommandHandler.Handle [L38–L78]
      └─ maps_to OfficeDto [L76]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettingsAsync [L53]
      └─ uses_service IControlledRepository<Office>
        └─ method WriteQuery [L60]
      └─ uses_service IMapper
        └─ method Map [L76]

