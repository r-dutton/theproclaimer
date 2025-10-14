[web] GET /api/binders/{binderId:Guid}/worksheets/{id:Guid}/linked-records  (Workpapers.Next.API.Controllers.Workpapers.Worksheets.BinderWorksheetsController.GetLinkedRecords)  [L126–L138] [auth=AuthorizationPolicies.User]
  └─ maps_to LinkedWorkpaperRecordDto [L131]
    └─ automapper.registration WorkpapersMappingProfile (WorkpaperRecord->LinkedWorkpaperRecordDto) [L559]
  └─ calls WorkpaperRecordRepository.ReadQuery [L131]
  └─ queries WorkpaperRecord [L131]
    └─ reads_from WorkpaperRecords
  └─ uses_service IControlledRepository<WorkpaperRecord>
    └─ method ReadQuery [L131]
  └─ sends_request CanIAccessWorksheetQuery [L129]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Worksheets.CanIAccessWorksheetQueryHandler.Handle [L36–L60]
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L54]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L58]

