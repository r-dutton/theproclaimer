[web] GET /api/accounting/reports/published/batch/{id:guid}  (Cirrus.Api.Controllers.Accounting.Reports.PublishedReportsController.Detail)  [L94–L100] status=200 [auth=user]
  └─ maps_to PublishedReportBatchDto [L99]
  └─ calls PublishedReportBatchRepository.ReadQuery [L97]
  └─ query PublishedReportBatch [L97]
    └─ reads_from PublishedReportBatches
  └─ uses_service IRequestProcessor (InstancePerDependency)
    └─ method ProcessAsync [L98]
      └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
        └─ constructs RequestProcessorWrapper<CanIAccessFileQuery,Unit>
        └─ resolves IPipelineBehavior<CanIAccessFileQuery,Unit> chain
        └─ invokes IAsyncRequestHandler<CanIAccessFileQuery,Unit>.Handle
        └─ dispatches CanIAccessFileQuery [L98]
          └─ handled_by Cirrus.ApplicationService.Firm.Queries.CanIAccessFileQueryHandler.Handle [L43–L101]
            └─ uses_service IRequestProcessor (InstancePerDependency)
              └─ method ProcessAsync [L90]
                └─ ... (service recursion detected)
            └─ uses_service IUserService (InstancePerLifetimeScope)
              └─ method GetUserId [L68]
                └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
                  └─ uses_service User
                    └─ method GetUserId [L67]
                      └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
                  └─ uses_service Guid?
                    └─ method GetUserId [L64]
                      └─ ... (no implementation details available)
                  └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
            └─ uses_service ITenantService (AddScoped)
              └─ method GetCurrentTenant [L68]
                └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenant [L6-L27]
                  └─ uses_service TenantIdentificationService
                    └─ method GetCurrentTenant [L20]
                      └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
                        └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
                        └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
                        └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
            └─ uses_service IRequestInfoService (AddScoped)
              └─ method IsValidServiceAccountRequest [L66]
                └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
            └─ uses_cache IDistributedCache.SetRecordAsync [write] [L79]
            └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L71]
            └─ uses_cache IDistributedCache.CreateAccessKey [write] [L68]
  └─ dispatches CanIAccessFileQuery -> CanIAccessFileQueryHandler [L98]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ PublishedReportBatch writes=0 reads=1
    └─ services 1
      └─ IRequestProcessor
    └─ requests 1
      └─ CanIAccessFileQuery
    └─ handlers 1
      └─ CanIAccessFileQueryHandler
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 1
      └─ PublishedReportBatchDto

