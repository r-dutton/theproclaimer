[web] GET /api/audit-trail/search  (Workpapers.Next.API.Controllers.Workpapers.AuditHistoriesController.Search)  [L27–L44] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetAuditHistoriesQuery -> GetAuditHistoriesQueryHandler [L40]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.AuditHistories.GetAuditHistoriesQueryHandler.Handle [L28–L184]
      └─ uses_service AuditHistoryStorageService
        └─ method GetLogs [L182]
          └─ implementation Workpapers.Next.Data.Storage.AuditHistory.AuditHistoryStorageService.GetLogs [L19-L174]
            └─ uses_service AuditHistoryEntityType[]
              └─ method BuildPartitionKey [L152]
                └─ ... (no implementation details available)
            └─ uses_service TableStorageService
              └─ method WriteToStorage [L97]
                └─ implementation Workpapers.Next.Data.Storage.AzureTables.TableStorageService.WriteToStorage [L15-L161]
                  └─ uses_service TenantIdentificationService
                    └─ method WriteBatch [L36]
                      └─ implementation Workpapers.Next.ApplicationService.Services.TenantIdentificationService.WriteBatch [L15-L131]
                        └─ uses_service RequestProcessor
                          └─ method GetCatalogByFirmId [L104]
                            └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                            └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                            └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                            └─ +1 additional_requests elided
                        └─ uses_service FirmLightDto
                          └─ method AssignActiveTenant [L77]
                            └─ implementation Workpapers.Next.DTOs.FirmLightDto.AssignActiveTenant [L8-L17]
                        └─ uses_cache IMemoryCache.GetOrCreate [read] [L116]
            └─ uses_service IBackgroundTaskQueue (AddSingleton)
              └─ method CommitLogs [L86]
                └─ ... (no implementation details available)
            └─ uses_service IServiceScopeFactory
              └─ method CommitLogs [L72]
                └─ ... (no implementation details available)
            └─ uses_service TenantIdentificationService
              └─ method CommitLogs [L67]
                └─ implementation Workpapers.Next.ApplicationService.Services.TenantIdentificationService.CommitLogs [L15-L131]
                  └─ uses_service RequestProcessor
                    └─ method GetCatalogByFirmId [L104]
                      └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                      └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                      └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                      └─ +1 additional_requests elided
                  └─ uses_service FirmLightDto
                    └─ method AssignActiveTenant [L77]
                      └─ implementation Workpapers.Next.DTOs.FirmLightDto.AssignActiveTenant (see previous expansion)
            └─ uses_service ClaimsPrincipalAccessor
              └─ method CommitLogs [L66]
                └─ ... (no implementation details available)
            └─ uses_service List<AuditHistoryToStorageDto>
              └─ method QueueLog [L58]
                └─ implementation Cirrus.Api.Controllers.Accounting.Reports.PublishedReportsController.List.QueueLog [L60-L77]
                  └─ calls PublishedReportBatchRepository.ReadQuery [L66]
                  └─ uses_service IRequestProcessor (InstancePerDependency)
                    └─ method ProcessAsync [L70]
                      └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
                        └─ constructs RequestProcessorWrapper<CanIAccessFileQuery,Unit>
                        └─ resolves IPipelineBehavior<CanIAccessFileQuery,Unit> chain
                        └─ invokes IAsyncRequestHandler<CanIAccessFileQuery,Unit>.Handle
                        └─ dispatches CanIAccessFileQuery [L70]
                  └─ uses_service IControlledRepository<PublishedReportBatch> (Scoped (inferred))
                    └─ method ReadQuery [L66]
                      └─ implementation Cirrus.Data.Repository.Accounting.Report.PublishedReportBatchRepository.ReadQuery
                  └─ query PublishedReportBatch [L66]
                  └─ dispatches CanIAccessFileQuery -> CanIAccessFileQueryHandler [L70]
                  └─ sends_request CanIAccessFileQuery -> CanIAccessFileQueryHandler [L70] ... (reused)
            └─ uses_storage TableStorageService.WriteToStorage [L97]
      └─ uses_service IControlledRepository<WorkpaperRecord> (Scoped (inferred))
        └─ method ReadQuery [L142]
          └─ implementation Workpapers.Next.Data.Repository.Workpapers.WorkpaperRecordRepository.ReadQuery
      └─ uses_service IControlledRepository<Worksheet> (Scoped (inferred))
        └─ method ReadQuery [L101]
          └─ implementation Workpapers.Next.Data.Repository.Workpapers.WorksheetRepository.ReadQuery
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L68]
          └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
      └─ uses_service IControlledRepository<Binder> (Scoped (inferred))
        └─ method ReadQuery [L56]
          └─ implementation Workpapers.Next.Data.Repository.BinderRepository.ReadQuery
      └─ uses_storage AuditHistoryStorageService.GetLogs [L182]
  └─ sends_request CanIAccessBinderQuery -> CanIAccessBinderQueryHandler [L36]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Binders.CanIAccessBinderQueryHandler.Handle [L60–L126]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L117]
          └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
      └─ uses_service IControlledRepository<Binder> (Scoped (inferred))
        └─ method ReadQuery [L101]
          └─ implementation Workpapers.Next.Data.Repository.BinderRepository.ReadQuery
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L92]
          └─ implementation Workpapers.Next.Services.Features.Tenants.TenantService.GetCurrentTenant [L5-L22]
            └─ uses_service TenantIdentificationService
              └─ method GetCurrentTenant [L20]
                └─ implementation Workpapers.Next.ApplicationService.Services.TenantIdentificationService.GetCurrentTenant [L15-L131]
                  └─ uses_service RequestProcessor
                    └─ method GetCatalogByFirmId [L104]
                      └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                      └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                      └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                      └─ +1 additional_requests elided
                  └─ uses_service FirmLightDto
                    └─ method AssignActiveTenant [L77]
                      └─ implementation Workpapers.Next.DTOs.FirmLightDto.AssignActiveTenant (see previous expansion)
      └─ uses_service UserService
        └─ method GetUserId [L91]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
            └─ uses_service User
              └─ method GetUserId [L67]
                └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
            └─ uses_service Guid?
              └─ method GetUserId [L64]
                └─ ... (no implementation details available)
            └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L89]
          └─ implementation Workpapers.Next.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L84]
            └─ uses_service RequestInfo
              └─ method IsValidServiceAccountRequest [L71]
                └─ implementation Cirrus.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L84]
                └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
                  └─ uses_service RequestInfo
                    └─ method IsValidServiceAccountRequest [L82]
                      └─ ... (service recursion detected)
                  └─ logs ILogger<IRequestInfoService> [Warning] [L89]
                └─ implementation Workpapers.Next.Services.Features.RequestInfoService.IsValidServiceAccountRequest (see previous expansion)
            └─ logs ILogger<IRequestInfoService> [Warning] [L81]
      └─ uses_cache IDistributedCache.SetRecordAsync [write] [L121]
      └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L109]
      └─ uses_cache IDistributedCache.CreateAccessKey [write] [L92]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ PublishedReportBatch writes=0 reads=1
    └─ requests 3
      └─ CanIAccessBinderQuery
      └─ CanIAccessFileQuery
      └─ GetAuditHistoriesQuery
    └─ handlers 3
      └─ CanIAccessBinderQueryHandler
      └─ CanIAccessFileQueryHandler
      └─ GetAuditHistoriesQueryHandler
    └─ caches 1
      └─ IMemoryCache
    └─ storages 1
      └─ TableStorageService

