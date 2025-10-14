[web] GET /api/binders/{binderId:Guid}/worksheets/{worksheetId:guid}/key-values/{id:guid}  (Workpapers.Next.API.Controllers.Workpapers.Worksheets.WorksheetKeyValuesController.GetWorksheetKeyValue)  [L39–L50] [auth=AuthorizationPolicies.User]
  └─ maps_to KeyValueDto [L44]
  └─ calls WorksheetRepository.ReadQuery [L44]
  └─ queries Worksheet [L44]
    └─ reads_from Worksheets
  └─ uses_service IControlledRepository<Worksheet>
    └─ method ReadQuery [L44]
  └─ sends_request CanIAccessWorksheetQuery [L42]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Worksheets.CanIAccessWorksheetQueryHandler.Handle [L36–L60]
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L54]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L58]

