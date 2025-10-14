[web] GET /api/binders/{binderId:Guid}/worksheets/{id:Guid}/commands  (Workpapers.Next.API.Controllers.Workpapers.Worksheets.BinderWorksheetsController.GetRecordCommands)  [L140–L154] [auth=AuthorizationPolicies.User]
  └─ maps_to CommandDto [L145]
  └─ calls WorksheetRepository.ReadQuery [L145]
  └─ queries Worksheet [L145]
    └─ reads_from Worksheets
  └─ uses_service IControlledRepository<Worksheet>
    └─ method ReadQuery [L145]
  └─ sends_request CanIAccessWorksheetQuery [L143]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Worksheets.CanIAccessWorksheetQueryHandler.Handle [L36–L60]
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L54]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L58]

