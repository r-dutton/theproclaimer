[web] POST /api/sources/export/  (Workpapers.Next.API.Controllers.Workpapers.ExportSourcesController.Create)  [L79–L86] status=201 [auth=AuthorizationPolicies.User]
  └─ sends_request CreateExportSourceCommand -> CreateExportSourceCommandHandler [L84]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.SourceData.ExportSources.CreateExportSourceCommandHandler.Handle [L28–L76]
      └─ calls SourceTypesRepository.ReadQuery [L65]
      └─ uses_service IControlledRepository<ExportSource> (Scoped (inferred))
        └─ method Add [L45]
          └─ implementation Workpapers.Next.Data.Repository.Workpapers.ExportSourceRepository.Add
  └─ impact_summary
    └─ requests 1
      └─ CreateExportSourceCommand
    └─ handlers 1
      └─ CreateExportSourceCommandHandler

