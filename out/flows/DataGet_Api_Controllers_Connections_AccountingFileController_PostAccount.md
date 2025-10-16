[web] POST /api/accounting-file/{fileId}/accounts  (DataGet.Api.Controllers.Connections.AccountingFileController.PostAccount)  [L89–L97] status=201 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request PostAccountCommand [L93]
    └─ handled_by DataGet.Connections.App.Commands.PostAccountCommandHandler.Handle [L22–L43]
      └─ uses_service IExternalApiServiceFactory (InstancePerLifetimeScope)
        └─ method GetAccountingApiFromApiType [L32]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L41]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle

