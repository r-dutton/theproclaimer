[web] PUT /api/workpaper-records/move  (Workpapers.Next.API.Controllers.Workpapers.WorkpaperRecordsController.Move)  [L357–L363] [auth=AuthorizationPolicies.User]
  └─ sends_request MoveWorkpaperRecordsCommand [L360]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.WorkpaperRecords.MoveWorkpaperRecordsCommandHandler.Handle [L37–L208]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L77]
      └─ uses_service IControlledRepository<WorkpaperRecord>
        └─ method ReadQuery [L59]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L79]

