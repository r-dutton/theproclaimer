[web] GET /api/workpaper-records/{id:guid}/hyperlinks  (Workpapers.Next.API.Controllers.Workpapers.WorkpaperRecordsController.GetHyperlinks)  [L399–L423] status=200 [auth=AuthorizationPolicies.User]
  └─ maps_to HyperlinkDto [L411]
  └─ calls WorkpaperRecordRepository.ReadQuery [L404]
  └─ query WorkpaperRecord [L404]
    └─ reads_from WorkpaperRecords
  └─ sends_request CanIAccessWorkpaperRecordQuery -> CanIAccessWorkpaperRecordQueryHandler [L402]
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
    └─ entities 1 (writes=0, reads=1)
      └─ WorkpaperRecord writes=0 reads=1
    └─ requests 1
      └─ CanIAccessWorkpaperRecordQuery
    └─ handlers 1
      └─ CanIAccessWorkpaperRecordQueryHandler
    └─ mappings 1
      └─ HyperlinkDto

