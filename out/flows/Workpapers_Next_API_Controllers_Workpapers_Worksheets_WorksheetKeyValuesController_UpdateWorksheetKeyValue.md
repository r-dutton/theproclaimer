[web] PUT /api/binders/{binderId:Guid}/worksheets/{worksheetId:guid}/key-values/{id:guid}  (Workpapers.Next.API.Controllers.Workpapers.Worksheets.WorksheetKeyValuesController.UpdateWorksheetKeyValue)  [L81–L92] [auth=AuthorizationPolicies.User]
  └─ calls WorksheetRepository.WriteQuery [L84]
  └─ writes_to Worksheet [L84]
    └─ reads_from Worksheets
  └─ uses_service IControlledRepository<Worksheet>
    └─ method WriteQuery [L84]
  └─ sends_request CanIAccessWorksheetQuery [L87]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Worksheets.CanIAccessWorksheetQueryHandler.Handle [L36–L60]
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L54]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L58]

