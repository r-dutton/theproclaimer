[web] POST /api/audit-trail/binders/{binderId:guid}  (Workpapers.Next.API.Controllers.Workpapers.AuditHistoriesController.CreateAuditMessageForBinder)  [L46–L52] status=201 [auth=AuthorizationPolicies.User]
  └─ sends_request CreateAuditMessageForBinderCommand -> CreateAuditMessageForBinderCommandHandler [L49]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.AuditHistories.CreateAuditMessageForBinderCommandHandler.Handle [L94–L145]
      └─ uses_service AuditHistoryStorageService
        └─ method QueueLog [L142]
          └─ implementation Workpapers.Next.Data.Storage.AuditHistory.AuditHistoryStorageService.QueueLog [L19-L174]
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
      └─ uses_service UserService
        └─ method GetUserIdIfPresent [L117]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserIdIfPresent [L20-L295]
            └─ uses_service RequestProcessor
              └─ method GetUserByDataverseId [L287]
                └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.GetUserByDataverseId
                └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.GetUserByDataverseId
                └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.GetUserByDataverseId
                └─ +1 additional_requests elided
            └─ uses_service bool?
              └─ method IsSuperUser [L174]
                └─ ... (no implementation details available)
            └─ uses_service Firm>
              └─ method GetFirmId [L139]
                └─ ... (no implementation details available)
            └─ uses_service User>
              └─ method GetUserIdFromToken [L106]
                └─ ... (no implementation details available)
            └─ uses_service User
              └─ method GetUserId [L67]
                └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
            └─ uses_service Guid?
              └─ method GetUserId [L64]
                └─ ... (no implementation details available)
            └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L113]
          └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
      └─ uses_storage AuditHistoryStorageService.QueueLog [L142]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ PublishedReportBatch writes=0 reads=1
    └─ requests 2
      └─ CanIAccessFileQuery
      └─ CreateAuditMessageForBinderCommand
    └─ handlers 2
      └─ CanIAccessFileQueryHandler
      └─ CreateAuditMessageForBinderCommandHandler
    └─ caches 1
      └─ IMemoryCache

