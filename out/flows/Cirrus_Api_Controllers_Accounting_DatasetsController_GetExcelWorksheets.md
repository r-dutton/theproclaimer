[web] POST /api/accounting/datasets/{id}/excel-worksheets  (Cirrus.Api.Controllers.Accounting.DatasetsController.GetExcelWorksheets)  [L164–L172] status=201 [auth=user]
  └─ sends_request GetExcelWorksheetNamesCommand -> GetExcelWorksheetNamesCommandHandler [L169]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.GetExcelWorksheetNamesCommandHandler.Handle [L21–L55]
  └─ sends_request CanIAccessDatasetQuery -> CanIAccessDatasetQueryHandler [L167]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.CanIAccessDatasetQueryHandler.Handle [L58–L140]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L129]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
      └─ uses_service IControlledRepository<Dataset> (Scoped (inferred))
        └─ method ReadQuery [L127]
          └─ implementation Cirrus.Data.Repository.Accounting.DatasetRepository.ReadQuery
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L103]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
            └─ uses_service User
              └─ method GetUserId [L67]
                └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
            └─ uses_service Guid?
              └─ method GetUserId [L64]
                └─ ... (no implementation details available)
            └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
      └─ uses_service IRequestInfoService (AddScoped)
        └─ method IsValidServiceAccountRequest [L101]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
      └─ uses_service ITenantService (AddScoped)
        └─ method GetCurrentTenant [L88]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenant [L6-L27]
            └─ uses_service TenantIdentificationService
              └─ method GetCurrentTenant [L20]
                └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
                  └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
                  └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
                  └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
      └─ uses_cache IDistributedCache.SetRecordAsync [write] [L116]
      └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L106]
      └─ uses_cache IDistributedCache.CreateAccessKey [write] [L103]
      └─ uses_cache IDistributedCache.CreateDatasetLockKey [write] [L88]
  └─ impact_summary
    └─ requests 2
      └─ CanIAccessDatasetQuery
      └─ GetExcelWorksheetNamesCommand
    └─ handlers 2
      └─ CanIAccessDatasetQueryHandler
      └─ GetExcelWorksheetNamesCommandHandler
    └─ caches 1
      └─ IMemoryCache

