[web] PUT /api/workpaper-records/{id:Guid}/remove-linking  (Workpapers.Next.API.Controllers.Workpapers.WorkpaperRecordsController.RemoveLinkedFlag)  [L300–L312] [auth=AuthorizationPolicies.User]
  └─ calls WorkpaperRecordRepository.WriteQuery [L303]
  └─ writes_to WorkpaperRecord [L303]
    └─ reads_from WorkpaperRecords
  └─ uses_service IControlledRepository<WorkpaperRecord>
    └─ method WriteQuery [L303]
  └─ sends_request CanIAccessWorkpaperRecordQuery [L307]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.WorkpaperRecords.CanIAccessWorkpaperRecordQueryHandler.Handle [L35–L59]
      └─ uses_service IControlledRepository<WorkpaperRecord>
        └─ method ReadQuery [L55]
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L53]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L57]

