[web] PUT /api/ui/clients/{id}  (Dataverse.Api.Controllers.UI.Core.ClientsController.Update)  [L273–L310] status=200 [auth=Authentication.UserPolicy]
  └─ calls ClientRepository.WriteQuery [L276]
  └─ calls OfficeRepository.ReadQuery [L291]
  └─ query Office [L291]
    └─ reads_from Offices
  └─ write Client [L276]
  └─ uses_service FirmSettingsService
    └─ method GetCurrentSettingsAsync [L281]
      └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync [L18-L97]
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L301]
      └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
  └─ uses_service IControlledRepository<Client>
    └─ method WriteQuery [L276]
      └─ ... (no implementation details available)
  └─ uses_service IControlledRepository<Office>
    └─ method ReadQuery [L291]
      └─ ... (no implementation details available)
  └─ sends_request CanIAccessClientQuery [L280]
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

