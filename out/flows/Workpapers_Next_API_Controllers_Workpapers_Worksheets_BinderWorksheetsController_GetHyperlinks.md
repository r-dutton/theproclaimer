[web] GET /api/binders/{binderId:Guid}/worksheets/{id:guid}/hyperlinks  (Workpapers.Next.API.Controllers.Workpapers.Worksheets.BinderWorksheetsController.GetHyperlinks)  [L285–L305] status=200 [auth=AuthorizationPolicies.User]
  └─ maps_to HyperlinkDto [L296]
  └─ calls WorkpaperRecordRepository.ReadQuery [L296]
  └─ calls WorksheetRepository.ReadQuery [L290]
  └─ query WorkpaperRecord [L296]
    └─ reads_from WorkpaperRecords
  └─ query Worksheet [L290]
    └─ reads_from Worksheets
  └─ sends_request CanIAccessWorksheetQuery -> CanIAccessWorksheetQueryHandler [L288]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Worksheets.CanIAccessWorksheetQueryHandler.Handle [L36–L60]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L58]
          └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L54]
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
    └─ entities 2 (writes=0, reads=2)
      └─ WorkpaperRecord writes=0 reads=1
      └─ Worksheet writes=0 reads=1
    └─ requests 1
      └─ CanIAccessWorksheetQuery
    └─ handlers 1
      └─ CanIAccessWorksheetQueryHandler
    └─ mappings 1
      └─ HyperlinkDto

