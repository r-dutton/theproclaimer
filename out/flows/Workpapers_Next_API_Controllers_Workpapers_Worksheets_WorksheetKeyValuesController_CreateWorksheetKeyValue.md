[web] POST /api/binders/{binderId:Guid}/worksheets/{worksheetId:guid}/key-values  (Workpapers.Next.API.Controllers.Workpapers.Worksheets.WorksheetKeyValuesController.CreateWorksheetKeyValue)  [L68–L79] [auth=AuthorizationPolicies.User]
  └─ calls WorksheetRepository.WriteQuery [L71]
  └─ writes_to Worksheet [L71]
    └─ reads_from Worksheets
  └─ uses_service IControlledRepository<Worksheet>
    └─ method WriteQuery [L71]
  └─ sends_request CanIAccessWorksheetQuery [L74]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Worksheets.CanIAccessWorksheetQueryHandler.Handle [L36–L60]
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L54]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L58]

