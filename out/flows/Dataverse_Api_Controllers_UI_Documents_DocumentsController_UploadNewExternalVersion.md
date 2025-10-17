[web] POST /api/ui/documents/documents/upload-new-external-version  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.UploadNewExternalVersion)  [L452–L458] status=201 [auth=Authentication.UserPolicy]
  └─ sends_request UploadNewExternalVersionCommand -> UploadNewExternalVersionCommandHandler [L456]
    └─ handled_by Dataverse.Connections.DataGet.Commands.UploadNewExternalVersionCommandHandler.Handle [L57–L223]
      └─ maps_to IntegrationSettingsDto [L168]
        └─ automapper.registration DataverseMappingProfile (IntegrationSettings->IntegrationSettingsDto) [L294]
      └─ uses_service DataGetImanageService
        └─ method GetDocumentVersion [L175]
          └─ implementation Dataverse.Connections.DataGet.Services.DataGetImanageService.GetDocumentVersion [L19-L225]
      └─ uses_service IControlledRepository<IntegrationSettings> (Scoped (inferred))
        └─ method ReadQuery [L168]
          └─ implementation Dataverse.Data.Repository.Firm.IntegrationSettingsRepository.ReadQuery
      └─ uses_service DataGetFyiService
        └─ method GetDocumentAsync [L142]
          └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiService.GetDocumentAsync [L19-L291]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L129]
          └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
      └─ uses_service IControlledRepository<Document> (Scoped (inferred))
        └─ method ReadQuery [L112]
          └─ implementation Dataverse.Data.Repository.Documents.DocumentRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ UploadNewExternalVersionCommand
    └─ handlers 1
      └─ UploadNewExternalVersionCommandHandler
    └─ mappings 1
      └─ IntegrationSettingsDto

