[web] GET /api/ui/clients/{id}  (Dataverse.Api.Controllers.UI.Core.ClientsController.Get)  [L111–L122] [auth=Authentication.UserPolicy]
  └─ maps_to ClientDto [L116]
    └─ automapper.registration DataverseMappingProfile (Client->ClientDto) [L164]
    └─ automapper.registration WorkpapersMappingProfile (Client->ClientDto) [L69]
    └─ automapper.registration CirrusMappingProfile (Client->ClientDto) [L146]
  └─ calls ClientRepository.ReadQuery [L116]
  └─ queries Client [L116]
  └─ uses_service FirmSettingsService
    └─ method GetCurrentSettingsAsync [L118]
  └─ uses_service IControlledRepository<Client>
    └─ method ReadQuery [L116]
  └─ uses_service UserService
    └─ method GetUserId [L118]
  └─ sends_request CanIAccessClientQuery [L114]
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

