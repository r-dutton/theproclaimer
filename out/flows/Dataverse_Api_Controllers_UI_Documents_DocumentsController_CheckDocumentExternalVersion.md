[web] GET /api/ui/documents/documents/{documentId}/check-external-version  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.CheckDocumentExternalVersion)  [L460–L465] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request CheckNewVersionFromExternalSourceCommand [L463]
    └─ handled_by Dataverse.Connections.DataGet.Commands.CheckNewVersionFromExternalSourceCommandHandler.Handle [L36–L161]
      └─ maps_to IntegrationSettingsDto [L118]
        └─ automapper.registration DataverseMappingProfile (IntegrationSettings->IntegrationSettingsDto) [L294]
      └─ uses_service DataGetFyiService
        └─ method GetExternalDocumentAsync [L104]
          └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiService.GetExternalDocumentAsync [L19-L291]
      └─ uses_service DataGetImanageService
        └─ method GetDocumentVersion [L125]
          └─ implementation Dataverse.Connections.DataGet.Services.DataGetImanageService.GetDocumentVersion [L19-L225]
      └─ uses_service IControlledRepository<ExternalEntityVersion>
        └─ method ReadQuery [L73]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<IntegrationSettings>
        └─ method ReadQuery [L118]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L119]
          └─ ... (no implementation details available)
      └─ uses_cache IDistributedCache.SetRecordAsync [write] [L90]
      └─ uses_cache IDistributedCache.TryGetRecordAsync [read] [L68]

