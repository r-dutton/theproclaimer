[web] POST /api/journals/balancing-journal/{binderColumnId:guid}  (Workpapers.Next.API.Controllers.Workpapers.JournalController.Create)  [L208–L215] [auth=AuthorizationPolicies.User]
  └─ sends_request ExportBalancingJournalCommand [L212]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.ExportBalancingJournalCommandHandler.Handle [L45–L182]
      └─ uses_service ApiService (SingleInstance)
        └─ method GetFeatures [L172]
      └─ uses_service IControlledRepository<Dataset>
        └─ method WriteQuery [L66]
      └─ uses_service IControlledRepository<Journal>
        └─ method Add [L98]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L177]
  └─ returns JournalExportResponse [L212]

