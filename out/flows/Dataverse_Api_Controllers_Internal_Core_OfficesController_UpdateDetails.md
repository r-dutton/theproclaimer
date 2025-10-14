[web] PUT /api/internal/offices/{id}  (Dataverse.Api.Controllers.Internal.Core.OfficesController.UpdateDetails)  [L78–L86] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request CreateOrUpdateOfficeCommand [L82]
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

