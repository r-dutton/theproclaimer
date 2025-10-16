[web] PUT /api/workpaper-records/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.WorkpaperRecordsController.Update)  [L199–L225] status=200 [auth=AuthorizationPolicies.User]
  └─ calls WorkpaperRecordRepository.WriteQuery [L202]
  └─ write WorkpaperRecord [L202]
    └─ reads_from WorkpaperRecords
  └─ sends_request GetWorkpaperRecordParamsQuery -> GetWorkpaperRecordParamsQueryHandler [L221]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.WorkpaperRecords.GetWorkpaperRecordParamsQueryHandler.Handle [L76–L264]
      └─ calls SourceAccountRepository.ReadQuery [L251]
      └─ uses_service IControlledRepository<WorkpaperRecordTemplate> (Scoped (inferred))
        └─ method ReadQuery [L204]
          └─ implementation Workpapers.Next.Data.Repository.Templates.WorkpaperRecordTemplateRepository.ReadQuery
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L189]
          └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
      └─ uses_service IControlledRepository<RecordStatus> (AddScoped)
        └─ method WriteQuery [L169]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Binder> (Scoped (inferred))
        └─ method ReadQuery [L153]
          └─ implementation Workpapers.Next.Data.Repository.BinderRepository.ReadQuery
      └─ uses_service IControlledRepository<Worksheet> (Scoped (inferred))
        └─ method WriteQuery [L141]
          └─ implementation Workpapers.Next.Data.Repository.Workpapers.WorksheetRepository.WriteQuery
      └─ uses_service UserService
        └─ method GetUserId [L115]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
            └─ uses_service User
              └─ method GetUserId [L67]
                └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
            └─ uses_service Guid?
              └─ method GetUserId [L64]
                └─ ... (no implementation details available)
            └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ sends_request CanIAccessWorkpaperRecordQuery -> CanIAccessWorkpaperRecordQueryHandler [L210]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.WorkpaperRecords.CanIAccessWorkpaperRecordQueryHandler.Handle [L35–L59]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L57]
          └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
      └─ uses_service IControlledRepository<WorkpaperRecord> (Scoped (inferred))
        └─ method ReadQuery [L55]
          └─ implementation Workpapers.Next.Data.Repository.Workpapers.WorkpaperRecordRepository.ReadQuery
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L53]
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
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ WorkpaperRecord writes=1 reads=0
    └─ requests 2
      └─ CanIAccessWorkpaperRecordQuery
      └─ GetWorkpaperRecordParamsQuery
    └─ handlers 2
      └─ CanIAccessWorkpaperRecordQueryHandler
      └─ GetWorkpaperRecordParamsQueryHandler
    └─ caches 1
      └─ IMemoryCache

