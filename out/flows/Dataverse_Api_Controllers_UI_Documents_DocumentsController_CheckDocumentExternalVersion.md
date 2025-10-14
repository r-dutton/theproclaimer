[web] GET /api/ui/documents/documents/{documentId}/check-external-version  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.CheckDocumentExternalVersion)  [L460–L465] [auth=Authentication.UserPolicy]
  └─ sends_request CheckNewVersionFromExternalSourceCommand [L463]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.Connections.DataGet.Commands.CheckNewVersionFromExternalSourceCommandHandler.Handle [L36–L161]
      └─ maps_to IntegrationSettingsDto [L118]
        └─ automapper.registration DataverseMappingProfile (IntegrationSettings->IntegrationSettingsDto) [L293]
      └─ uses_service DataGetFyiService
        └─ method GetExternalDocumentAsync [L104]
      └─ uses_service DataGetImanageService
        └─ method GetDocumentVersion [L125]
      └─ uses_service IControlledRepository<ExternalEntityVersion>
        └─ method ReadQuery [L73]
      └─ uses_service IControlledRepository<IntegrationSettings>
        └─ method ReadQuery [L118]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L119]
      └─ uses_cache IDistributedCache [L90]
        └─ method SetRecordAsync [write] [L90]
      └─ uses_cache IDistributedCache [L68]
        └─ method TryGetRecordAsync [read] [L68]

