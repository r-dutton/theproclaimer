[web] POST /api/ui/documents/documents/upload-new-external-version  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.UploadNewExternalVersion)  [L452–L458] status=201 [auth=Authentication.UserPolicy]
  └─ sends_request UploadNewExternalVersionCommand [L456]
    └─ handled_by Dataverse.Connections.DataGet.Commands.UploadNewExternalVersionCommandHandler.Handle [L57–L223]
      └─ maps_to IntegrationSettingsDto [L168]
        └─ automapper.registration DataverseMappingProfile (IntegrationSettings->IntegrationSettingsDto) [L294]
      └─ uses_service DataGetFyiService
        └─ method GetDocumentAsync [L142]
          └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiService.GetDocumentAsync [L19-L291]
      └─ uses_service DataGetImanageService
        └─ method GetDocumentVersion [L175]
          └─ implementation Dataverse.Connections.DataGet.Services.DataGetImanageService.GetDocumentVersion [L19-L225]
      └─ uses_service IControlledRepository<Document>
        └─ method ReadQuery [L112]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<IntegrationSettings>
        └─ method ReadQuery [L168]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L169]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L129]
          └─ implementation Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync [L8-L45]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle

