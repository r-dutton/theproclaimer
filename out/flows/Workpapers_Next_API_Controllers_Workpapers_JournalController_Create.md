[web] POST /api/journals/balancing-journal/{binderColumnId:guid}  (Workpapers.Next.API.Controllers.Workpapers.JournalController.Create)  [L208–L215] status=201 [auth=AuthorizationPolicies.User]
  └─ sends_request ExportBalancingJournalCommand [L212]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.ExportBalancingJournalCommandHandler.Handle [L45–L182]
      └─ uses_service ApiService (SingleInstance)
        └─ method GetFeatures [L172]
          └─ implementation Cirrus.ApplicationService.Accounting.Services.ApiService.GetFeatures [L14-L75]
      └─ uses_service IControlledRepository<Dataset>
        └─ method WriteQuery [L66]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Journal>
        └─ method Add [L98]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L177]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)
  └─ returns JournalExportResponse [L212]

