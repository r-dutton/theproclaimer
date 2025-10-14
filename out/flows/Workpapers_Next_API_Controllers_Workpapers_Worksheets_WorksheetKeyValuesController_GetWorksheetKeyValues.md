[web] GET /api/binders/{binderId:Guid}/worksheets/{worksheetId:guid}/key-values  (Workpapers.Next.API.Controllers.Workpapers.Worksheets.WorksheetKeyValuesController.GetWorksheetKeyValues)  [L52–L66] [auth=AuthorizationPolicies.User]
  └─ maps_to KeyValueDto [L57]
  └─ calls WorksheetRepository.ReadQuery [L57]
  └─ queries Worksheet [L57]
    └─ reads_from Worksheets
  └─ uses_service IControlledRepository<Worksheet>
    └─ method ReadQuery [L57]
  └─ sends_request CanIAccessWorksheetQuery [L55]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Worksheets.CanIAccessWorksheetQueryHandler.Handle [L36–L60]
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L54]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L58]

