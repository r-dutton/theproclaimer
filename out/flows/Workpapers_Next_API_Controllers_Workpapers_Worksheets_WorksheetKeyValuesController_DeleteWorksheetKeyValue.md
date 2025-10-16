[web] DELETE /api/binders/{binderId:Guid}/worksheets/{worksheetId:guid}/key-values/{id:guid}  (Workpapers.Next.API.Controllers.Workpapers.Worksheets.WorksheetKeyValuesController.DeleteWorksheetKeyValue)  [L94–L105] status=200 [auth=AuthorizationPolicies.User]
  └─ calls WorksheetRepository.WriteQuery [L97]
  └─ write Worksheet [L97]
    └─ reads_from Worksheets
  └─ sends_request CanIAccessWorksheetQuery -> CanIAccessWorksheetQueryHandler [L100]
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
    └─ entities 1 (writes=1, reads=0)
      └─ Worksheet writes=1 reads=0
    └─ requests 1
      └─ CanIAccessWorksheetQuery
    └─ handlers 1
      └─ CanIAccessWorksheetQueryHandler

