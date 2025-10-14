[web] GET /api/binders/{binderId:Guid}/worksheets/{id:guid}/hyperlinks  (Workpapers.Next.API.Controllers.Workpapers.Worksheets.BinderWorksheetsController.GetHyperlinks)  [L285–L305] [auth=AuthorizationPolicies.User]
  └─ maps_to HyperlinkDto [L296]
  └─ maps_to HyperlinkDto [L290]
  └─ calls WorkpaperRecordRepository.ReadQuery [L296]
  └─ calls WorksheetRepository.ReadQuery [L290]
  └─ queries WorkpaperRecord [L296]
    └─ reads_from WorkpaperRecords
  └─ queries Worksheet [L290]
    └─ reads_from Worksheets
  └─ uses_service IControlledRepository<WorkpaperRecord>
    └─ method ReadQuery [L296]
  └─ uses_service IControlledRepository<Worksheet>
    └─ method ReadQuery [L290]
  └─ sends_request CanIAccessWorksheetQuery [L288]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Worksheets.CanIAccessWorksheetQueryHandler.Handle [L36–L60]
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L54]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L58]

