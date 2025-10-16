[web] POST /api/ui/account-mapping/mapping-codes/for-client-group/{clientGroupId:guid}  (Dataverse.Api.Controllers.UI.AccountMapping.MappingCodesController.CreateMappingCode)  [L92–L100] status=201 [auth=Authentication.UserPolicy,Authentication.UserPolicy]
  └─ sends_request CreateMappingCodeCommand [L97]
    └─ handled_by Dataverse.ApplicationService.Commands.AccountMapping.MappingCodes.CreateMappingCodeCommandHandler.Handle [L41–L75]
      └─ maps_to MappingCodeDto [L73]
      └─ uses_service UserService
        └─ method IsMaster [L58]
          └─ implementation Dataverse.ApplicationService.Services.UserService.IsMaster [L15-L185]
      └─ uses_service IControlledRepository<MappingCode>
        └─ method ReadQuery [L60]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method Map [L73]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L69]
          └─ implementation Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync [L8-L45]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle

