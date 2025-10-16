[web] POST /api/ui/clients/  (Dataverse.Api.Controllers.UI.Core.ClientsController.Create)  [L229–L271] status=201 [auth=Authentication.UserPolicy]
  └─ calls ClientRepository.Add [L268]
  └─ calls ClientRepository.WriteQuery [L253]
  └─ calls OfficeRepository.ReadQuery [L237]
  └─ query Office [L237]
    └─ reads_from Offices
  └─ insert Client [L268]
  └─ write Client [L253]
  └─ uses_service FirmSettingsService
    └─ method GetCurrentSettingsAsync [L248]
      └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync [L18-L97]
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L261]
      └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
  └─ uses_service IControlledRepository<Client>
    └─ method WriteQuery [L253]
      └─ ... (no implementation details available)
  └─ uses_service IControlledRepository<Office>
    └─ method ReadQuery [L237]
      └─ ... (no implementation details available)
  └─ sends_request CanIAccessOfficeQuery [L236]
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.CanIAccessOfficeQueryHandler.Handle [L39–L110]
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L66]
          └─ implementation Cirrus.Central.Tenants.TenantService.GetCurrentTenant [L26-L139]
          └─ implementation Cirrus.Central.Tenants.TenantService.GetCurrentTenant [L26-L139]
      └─ uses_service IControlledRepository<OfficeUser>
        └─ method ReadQuery [L86]
          └─ ... (no implementation details available)
      └─ uses_service IRequestInfoService (AddScoped)
        └─ method IsValidServiceAccountRequest [L60]
          └─ implementation IRequestInfoService.IsValidServiceAccountRequest [L20-L20]
          └─ ... (no implementation details available)
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L65]
          └─ implementation IUserService.GetUserId [L18-L18]
          └─ ... (no implementation details available)
      └─ uses_cache IDistributedCache.SetRecordAsync [write] [L103]
      └─ uses_cache IDistributedCache.SetRecordAsync [write] [L79]
      └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L69]
      └─ uses_cache IDistributedCache.CreateAccessKey [write] [L66]
  └─ sends_request CanIAccessTeamQuery [L245]
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.CanIAccessTeamQueryHandler.Handle [L39–L110]
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L66]
          └─ implementation Cirrus.Central.Tenants.TenantService.GetCurrentTenant [L26-L139]
          └─ implementation Cirrus.Central.Tenants.TenantService.GetCurrentTenant [L26-L139]
      └─ uses_service IControlledRepository<TeamUser>
        └─ method ReadQuery [L86]
          └─ ... (no implementation details available)
      └─ uses_service IRequestInfoService (AddScoped)
        └─ method IsValidServiceAccountRequest [L60]
          └─ implementation IRequestInfoService.IsValidServiceAccountRequest [L20-L20]
          └─ ... (no implementation details available)
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L65]
          └─ implementation IUserService.GetUserId [L18-L18]
          └─ ... (no implementation details available)
      └─ uses_cache IDistributedCache.SetRecordAsync [write] [L103]
      └─ uses_cache IDistributedCache.SetRecordAsync [write] [L79]
      └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L69]
      └─ uses_cache IDistributedCache.CreateAccessKey [write] [L66]

