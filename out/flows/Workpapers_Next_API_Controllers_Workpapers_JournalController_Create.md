[web] POST /api/journals/balancing-journal/{binderColumnId:guid}  (Workpapers.Next.API.Controllers.Workpapers.JournalController.Create)  [L208–L215] status=201 [auth=AuthorizationPolicies.User]
  └─ sends_request ExportBalancingJournalCommand -> ExportBalancingJournalCommandHandler [L212]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.ExportBalancingJournalCommandHandler.Handle [L45–L182]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L177]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
      └─ uses_service ApiService (SingleInstance)
        └─ method GetFeatures [L172]
          └─ implementation Cirrus.ApplicationService.Accounting.Services.ApiService.GetFeatures [L14-L75]
      └─ uses_service IControlledRepository<Journal> (Scoped (inferred))
        └─ method Add [L98]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.JournalRepository.Add
      └─ uses_service IControlledRepository<Dataset> (Scoped (inferred))
        └─ method WriteQuery [L66]
          └─ implementation Cirrus.Data.Repository.Accounting.DatasetRepository.WriteQuery
  └─ returns JournalExportResponse [L212]
  └─ impact_summary
    └─ requests 1
      └─ ExportBalancingJournalCommand
    └─ handlers 1
      └─ ExportBalancingJournalCommandHandler
    └─ mappings 1
      └─ JournalExportResponse

