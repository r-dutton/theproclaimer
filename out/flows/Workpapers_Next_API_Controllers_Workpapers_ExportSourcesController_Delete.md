[web] DELETE /api/sources/export/{id:guid}  (Workpapers.Next.API.Controllers.Workpapers.ExportSourcesController.Delete)  [L108–L116] status=204 [auth=AuthorizationPolicies.User]
  └─ sends_request DeleteExportSourceCommand [L113]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.SourceData.ExportSources.DeleteExportSourceCommandHandler.Handle [L23–L41]
      └─ uses_service IControlledRepository<ExportSource>
        └─ method ReadQuery [L34]
          └─ ... (no implementation details available)

