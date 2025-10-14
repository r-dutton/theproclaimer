[web] POST /api/ui/clients/  (Dataverse.Api.Controllers.UI.Core.ClientsController.Create)  [L229–L271] [auth=Authentication.UserPolicy]
  └─ calls ClientRepository.Add [L268]
  └─ calls ClientRepository.WriteQuery [L253]
  └─ calls OfficeRepository.ReadQuery [L237]
  └─ queries Office [L237]
    └─ reads_from Offices
  └─ writes_to Client [L268]
  └─ writes_to Client [L253]
  └─ uses_service FirmSettingsService
    └─ method GetCurrentSettingsAsync [L248]
  └─ uses_service IControlledRepository<Client>
    └─ method WriteQuery [L253]
  └─ uses_service IControlledRepository<Office>
    └─ method ReadQuery [L237]
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L261]
  └─ sends_request CanIAccessOfficeQuery [L236]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.CanIAccessOfficeQueryHandler.Handle [L39–L110]
      └─ uses_service IControlledRepository<OfficeUser>
        └─ method ReadQuery [L86]
      └─ uses_service IRequestInfoService (AddScoped)
        └─ method IsValidServiceAccountRequest [L60]
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L65]
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L66]
      └─ uses_cache IDistributedCache [L103]
        └─ method SetRecordAsync [write] [L103]
      └─ uses_cache IDistributedCache [L79]
        └─ method SetRecordAsync [write] [L79]
      └─ uses_cache IDistributedCache [L69]
        └─ method DoesRecordExistAsync [access] [L69]
      └─ uses_cache IDistributedCache [L66]
        └─ method CreateAccessKey [write] [L66]
  └─ sends_request CanIAccessTeamQuery [L245]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.CanIAccessTeamQueryHandler.Handle [L39–L110]
      └─ uses_service IControlledRepository<TeamUser>
        └─ method ReadQuery [L86]
      └─ uses_service IRequestInfoService (AddScoped)
        └─ method IsValidServiceAccountRequest [L60]
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L65]
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L66]
      └─ uses_cache IDistributedCache [L103]
        └─ method SetRecordAsync [write] [L103]
      └─ uses_cache IDistributedCache [L79]
        └─ method SetRecordAsync [write] [L79]
      └─ uses_cache IDistributedCache [L69]
        └─ method DoesRecordExistAsync [access] [L69]
      └─ uses_cache IDistributedCache [L66]
        └─ method CreateAccessKey [write] [L66]

