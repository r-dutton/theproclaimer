[web] GET /api/ui/clients/{id}/audit  (Dataverse.Api.Controllers.UI.Core.ClientsController.GetClientAudit)  [L209–L220] [auth=Authentication.UserPolicy]
  └─ calls ClientRepository.ReadQuery [L214]
  └─ queries Client [L214]
  └─ uses_service IControlledRepository<Client>
    └─ method ReadQuery [L214]
  └─ sends_request CanIAccessClientQuery [L212]
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

