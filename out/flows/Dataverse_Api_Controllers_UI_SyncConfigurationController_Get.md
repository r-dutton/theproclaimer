[web] GET /api/ui/sync-configuration/{id:Guid}  (Dataverse.Api.Controllers.UI.SyncConfigurationController.Get)  [L59–L63] status=200 [auth=Authentication.AdminPolicy]
  └─ sends_request GetSyncConfigurationQuery [L62]
    └─ handled_by Dataverse.ApplicationService.Queries.SyncConfigurations.GetSyncConfigurationQueryHandler.Handle [L34–L90]
      └─ maps_to SyncConfigurationDto [L54]
      └─ uses_service IControlledRepository<SyncConfiguration>
        └─ method ReadQuery [L49]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method Map [L54]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L65]
          └─ implementation Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync [L8-L45]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle

