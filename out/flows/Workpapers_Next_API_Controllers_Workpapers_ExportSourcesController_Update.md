[web] PUT /api/sources/export/{id:guid}  (Workpapers.Next.API.Controllers.Workpapers.ExportSourcesController.Update)  [L98–L103] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request UpdateExportSourceCommand -> UpdateExportSourceCommandHandler [L101]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.SourceData.ExportSources.UpdateExportSourceCommandHandler.Handle [L30–L82]
      └─ calls SourceTypesRepository.ReadQuery [L71]
      └─ uses_service IControlledRepository<ExportSource> (Scoped (inferred))
        └─ method WriteQuery [L43]
          └─ implementation Workpapers.Next.Data.Repository.Workpapers.ExportSourceRepository.WriteQuery
  └─ impact_summary
    └─ requests 1
      └─ UpdateExportSourceCommand
    └─ handlers 1
      └─ UpdateExportSourceCommandHandler

