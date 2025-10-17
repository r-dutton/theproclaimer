[web] DELETE /api/sources/export/{id:guid}  (Workpapers.Next.API.Controllers.Workpapers.ExportSourcesController.Delete)  [L108–L116] status=204 [auth=AuthorizationPolicies.User]
  └─ sends_request DeleteExportSourceCommand -> DeleteExportSourceCommandHandler [L113]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.SourceData.ExportSources.DeleteExportSourceCommandHandler.Handle [L23–L41]
      └─ uses_service IControlledRepository<ExportSource> (Scoped (inferred))
        └─ method ReadQuery [L34]
          └─ implementation Workpapers.Next.Data.Repository.Workpapers.ExportSourceRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ DeleteExportSourceCommand
    └─ handlers 1
      └─ DeleteExportSourceCommandHandler

