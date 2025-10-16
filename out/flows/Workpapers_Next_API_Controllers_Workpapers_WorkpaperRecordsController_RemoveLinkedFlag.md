[web] PUT /api/workpaper-records/{id:Guid}/remove-linking  (Workpapers.Next.API.Controllers.Workpapers.WorkpaperRecordsController.RemoveLinkedFlag)  [L300–L312] status=200 [auth=AuthorizationPolicies.User]
  └─ calls WorkpaperRecordRepository.WriteQuery [L303]
  └─ write WorkpaperRecord [L303]
    └─ reads_from WorkpaperRecords
  └─ sends_request CanIAccessWorkpaperRecordQuery -> CanIAccessWorkpaperRecordQueryHandler [L307]
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
    └─ requests 1
      └─ CanIAccessWorkpaperRecordQuery
    └─ handlers 1
      └─ CanIAccessWorkpaperRecordQueryHandler

