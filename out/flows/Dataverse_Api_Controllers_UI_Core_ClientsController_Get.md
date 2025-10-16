[web] GET /api/ui/clients/{id}  (Dataverse.Api.Controllers.UI.Core.ClientsController.Get)  [L111–L122] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to ClientDto [L116]
    └─ automapper.registration DataverseMappingProfile (Client->ClientDto) [L165]
    └─ automapper.registration WorkpapersMappingProfile (Client->ClientDto) [L69]
    └─ automapper.registration CirrusMappingProfile (Client->ClientDto) [L146]
  └─ calls ClientRepository.ReadQuery [L116]
  └─ query Client [L116]
  └─ uses_service FirmSettingsService
    └─ method GetCurrentSettingsAsync [L118]
      └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync [L18-L97]
  └─ uses_service IControlledRepository<Client>
    └─ method ReadQuery [L116]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method GetUserId [L118]
      └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId [L15-L185]
  └─ sends_request CanIAccessClientQuery [L114]
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.CanIAccessClientQueryHandler.Handle [L41–L104]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettings [L83]
          └─ implementation Cirrus.ApplicationService.Firm.FirmSettingsService.GetCurrentSettings [L15-L112]
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L71]
          └─ implementation Cirrus.Central.Tenants.TenantService.GetCurrentTenant [L26-L139]
          └─ implementation Cirrus.Central.Tenants.TenantService.GetCurrentTenant [L26-L139]
      └─ uses_service IControlledRepository<Client>
        └─ method ReadQuery [L85]
          └─ ... (no implementation details available)
      └─ uses_service IRequestInfoService (AddScoped)
        └─ method IsValidServiceAccountRequest [L64]
          └─ implementation IRequestInfoService.IsValidServiceAccountRequest [L20-L20]
          └─ ... (no implementation details available)
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L71]
          └─ implementation IUserService.GetUserId [L18-L18]
          └─ ... (no implementation details available)
      └─ uses_cache IDistributedCache.SetRecordAsync [write] [L98]
      └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L73]
      └─ uses_cache IDistributedCache.CreateAccessKey [write] [L71]

