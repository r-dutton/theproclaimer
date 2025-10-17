[web] GET /api/accounting/files/for-entity/{entityId}  (Cirrus.Api.Controllers.Accounting.FilesController.GetForEntity)  [L137–L158] status=200 [auth=user]
  └─ maps_to FileLightDto [L153]
    └─ automapper.registration CirrusMappingProfile (File->FileLightDto) [L192]
  └─ calls FileRepository.ReadQuery [L153]
  └─ calls EntityRepository.ReadQuery [L145]
  └─ query File [L153]
    └─ reads_from Files
  └─ query Entity [L145]
  └─ sends_request CanIAccessEntityQuery -> CanIAccessEntityQueryHandler [L152]
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.CanIAccessEntityQueryHandler.Handle [L41–L99]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L83]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
      └─ uses_service IControlledRepository<Entity> (Scoped (inferred))
        └─ method ReadQuery [L81]
          └─ implementation Cirrus.Data.Repository.Firm.EntityRepository.ReadQuery
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L72]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
            └─ uses_service User
              └─ method GetUserId [L67]
                └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
            └─ uses_service Guid?
              └─ method GetUserId [L64]
                └─ ... (no implementation details available)
            └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L72]
          └─ implementation Cirrus.Central.Tenants.TenantService.GetCurrentTenant [L26-L139]
            └─ uses_service IRequestProcessor (InstancePerDependency)
              └─ method GetCatalogByDataverseId [L111]
                └─ implementation DataGet.Services.Features.Requests.RequestProcessor.GetCatalogByDataverseId [L7-L35]
            └─ uses_service Tenant
              └─ method AssignCurrentTenantId [L80]
                └─ implementation Dataverse.Tenants.Model.Tenant.AssignCurrentTenantId [L20-L187]
            └─ uses_cache IMemoryCache.GetOrCreate [read] [L104]
      └─ uses_service IRequestInfoService (AddScoped)
        └─ method IsValidServiceAccountRequest [L67]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
      └─ uses_cache IDistributedCache.SetRecordAsync [write] [L92]
      └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L74]
      └─ uses_cache IDistributedCache.CreateAccessKey [write] [L72]
  └─ impact_summary
    └─ entities 2 (writes=0, reads=2)
      └─ Entity writes=0 reads=1
      └─ File writes=0 reads=1
    └─ requests 1
      └─ CanIAccessEntityQuery
    └─ handlers 1
      └─ CanIAccessEntityQueryHandler
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 1
      └─ FileLightDto

