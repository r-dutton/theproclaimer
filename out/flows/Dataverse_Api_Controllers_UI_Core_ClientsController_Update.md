[web] PUT /api/ui/clients/{id}  (Dataverse.Api.Controllers.UI.Core.ClientsController.Update)  [L273–L310] [auth=Authentication.UserPolicy]
  └─ calls ClientRepository.WriteQuery [L276]
  └─ calls OfficeRepository.ReadQuery [L291]
  └─ queries Office [L291]
    └─ reads_from Offices
  └─ writes_to Client [L276]
  └─ uses_service FirmSettingsService
    └─ method GetCurrentSettingsAsync [L281]
  └─ uses_service IControlledRepository<Client>
    └─ method WriteQuery [L276]
  └─ uses_service IControlledRepository<Office>
    └─ method ReadQuery [L291]
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L301]
  └─ sends_request CanIAccessClientQuery [L280]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.CanIAccessClientQueryHandler.Handle [L41–L104]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettings [L83]
      └─ uses_service IControlledRepository<Client>
        └─ method ReadQuery [L85]
      └─ uses_service IRequestInfoService (AddScoped)
        └─ method IsValidServiceAccountRequest [L64]
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L71]
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L71]
      └─ uses_cache IDistributedCache [L98]
        └─ method SetRecordAsync [write] [L98]
      └─ uses_cache IDistributedCache [L73]
        └─ method DoesRecordExistAsync [access] [L73]
      └─ uses_cache IDistributedCache [L71]
        └─ method CreateAccessKey [write] [L71]

