[web] PUT /api/import-runs/reinstate  (Workpapers.Next.API.Controllers.Workpapers.ImportRunController.Reinstate)  [L111–L115] [auth=AuthorizationPolicies.User]
  └─ sends_request ReinstateImportRunCommand [L114]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.ImportRuns.ReinstateImportRunCommandHandler.Handle [L25–L72]
      └─ uses_service IControlledRepository<Binder>
        └─ method WriteQuery [L58]
      └─ uses_service IControlledRepository<ImportRun>
        └─ method WriteQuery [L43]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L61]

