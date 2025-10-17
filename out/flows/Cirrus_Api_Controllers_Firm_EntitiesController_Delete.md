[web] DELETE /api/entities/{id:Guid}  (Cirrus.Api.Controllers.Firm.EntitiesController.Delete)  [L128–L135] status=200 [auth=user]
  └─ calls EntityRepository (methods: Remove,WriteQuery) [L133]
  └─ delete Entity [L133]
  └─ write Entity [L131]
  └─ sends_request CanIAccessEntityQuery -> CanIAccessEntityQueryHandler [L132]
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
    └─ entities 1 (writes=2, reads=0)
      └─ Entity writes=2 reads=0
    └─ requests 1
      └─ CanIAccessEntityQuery
    └─ handlers 1
      └─ CanIAccessEntityQueryHandler
    └─ caches 1
      └─ IMemoryCache

