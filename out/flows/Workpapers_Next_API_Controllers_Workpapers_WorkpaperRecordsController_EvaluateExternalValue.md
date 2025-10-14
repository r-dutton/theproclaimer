[web] GET /api/workpaper-records/external-values/evaluate  (Workpapers.Next.API.Controllers.Workpapers.WorkpaperRecordsController.EvaluateExternalValue)  [L244–L248] [auth=AuthorizationPolicies.User]
  └─ sends_request EvaluateExternalValueQuery [L247]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.WorkpaperRecords.ExternalValues.EvaluateExternalValueQueryHandler.Handle [L54–L126]
      └─ uses_service ICirrusQueryService (AddScoped)
        └─ method GetDataset [L121]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L104]
      └─ uses_service IControlledRepository<Worksheet>
        └─ method ReadQuery [L89]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L110]

