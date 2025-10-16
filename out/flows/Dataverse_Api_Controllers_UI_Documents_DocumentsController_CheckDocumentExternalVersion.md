[web] GET /api/ui/documents/documents/{documentId}/check-external-version  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.CheckDocumentExternalVersion)  [L460–L465] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request CheckNewVersionFromExternalSourceCommand -> CheckNewVersionFromExternalSourceCommandHandler [L463]
    └─ handled_by Dataverse.Connections.DataGet.Commands.CheckNewVersionFromExternalSourceCommandHandler.Handle [L36–L161]
      └─ maps_to IntegrationSettingsDto [L118]
        └─ automapper.registration DataverseMappingProfile (IntegrationSettings->IntegrationSettingsDto) [L294]
      └─ uses_service DataGetImanageService
        └─ method GetDocumentVersion [L125]
          └─ implementation Dataverse.Connections.DataGet.Services.DataGetImanageService.GetDocumentVersion [L19-L225]
      └─ uses_service IControlledRepository<IntegrationSettings> (Scoped (inferred))
        └─ method ReadQuery [L118]
          └─ implementation Dataverse.Data.Repository.Firm.IntegrationSettingsRepository.ReadQuery
      └─ uses_service DataGetFyiService
        └─ method GetExternalDocumentAsync [L104]
          └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiService.GetExternalDocumentAsync [L19-L291]
      └─ uses_service IControlledRepository<ExternalEntityVersion> (Scoped (inferred))
        └─ method ReadQuery [L73]
          └─ implementation Dataverse.Data.Repository.Documents.ExternalEntityVersionRepository.ReadQuery
      └─ uses_cache IDistributedCache.SetRecordAsync [write] [L90]
      └─ uses_cache IDistributedCache.TryGetRecordAsync [read] [L68]
  └─ impact_summary
    └─ requests 1
      └─ CheckNewVersionFromExternalSourceCommand
    └─ handlers 1
      └─ CheckNewVersionFromExternalSourceCommandHandler
    └─ mappings 1
      └─ IntegrationSettingsDto

