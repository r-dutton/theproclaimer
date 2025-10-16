[web] GET /api/accounting/files/for-entity/{entityId}  (Cirrus.Api.Controllers.Accounting.FilesController.GetForEntity)  [L137–L158] status=200 [auth=user]
  └─ maps_to FileLightDto [L153]
    └─ automapper.registration CirrusMappingProfile (File->FileLightDto) [L192]
  └─ calls FileRepository.ReadQuery [L153]
  └─ calls EntityRepository.ReadQuery [L145]
  └─ query File [L153]
    └─ reads_from Files
  └─ query Entity [L145]
  └─ uses_service IControlledRepository<Entity>
    └─ method ReadQuery [L145]
      └─ ... (no implementation details available)
  └─ uses_service IControlledRepository<File>
    └─ method ReadQuery [L153]
      └─ ... (no implementation details available)
  └─ sends_request CanIAccessEntityQuery [L152]
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.CanIAccessEntityQueryHandler.Handle [L41–L99]
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L72]
          └─ implementation Cirrus.Central.Tenants.TenantService.GetCurrentTenant [L26-L139]
          └─ implementation Cirrus.Central.Tenants.TenantService.GetCurrentTenant [L26-L139]
      └─ uses_service IControlledRepository<Entity>
        └─ method ReadQuery [L81]
          └─ ... (no implementation details available)
      └─ uses_service IRequestInfoService (AddScoped)
        └─ method IsValidServiceAccountRequest [L67]
          └─ implementation IRequestInfoService.IsValidServiceAccountRequest [L20-L20]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L83]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L72]
          └─ implementation IUserService.GetUserId [L18-L18]
          └─ ... (no implementation details available)
      └─ uses_cache IDistributedCache.SetRecordAsync [write] [L92]
      └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L74]
      └─ uses_cache IDistributedCache.CreateAccessKey [write] [L72]

