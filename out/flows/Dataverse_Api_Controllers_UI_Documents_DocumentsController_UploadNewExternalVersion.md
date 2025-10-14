[web] POST /api/ui/documents/documents/upload-new-external-version  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.UploadNewExternalVersion)  [L452–L458] [auth=Authentication.UserPolicy]
  └─ sends_request UploadNewExternalVersionCommand [L456]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.Connections.DataGet.Commands.UploadNewExternalVersionCommandHandler.Handle [L57–L223]
      └─ maps_to IntegrationSettingsDto [L168]
        └─ automapper.registration DataverseMappingProfile (IntegrationSettings->IntegrationSettingsDto) [L293]
      └─ uses_service DataGetFyiService
        └─ method GetDocumentAsync [L142]
      └─ uses_service DataGetImanageService
        └─ method GetDocumentVersion [L175]
      └─ uses_service IControlledRepository<Document>
        └─ method ReadQuery [L112]
      └─ uses_service IControlledRepository<IntegrationSettings>
        └─ method ReadQuery [L168]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L169]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L129]

