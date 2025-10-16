[web] GET /api/ui/clients/{id}/audit  (Dataverse.Api.Controllers.UI.Core.ClientsController.GetClientAudit)  [L209–L220] status=200 [auth=Authentication.UserPolicy]
  └─ calls ClientRepository.ReadQuery [L214]
  └─ query Client [L214]
  └─ uses_service IControlledRepository<Client>
    └─ method ReadQuery [L214]
      └─ ... (no implementation details available)
  └─ sends_request CanIAccessClientQuery [L212]
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

