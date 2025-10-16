[web] GET /api/ui/sync-configuration/  (Dataverse.Api.Controllers.UI.SyncConfigurationController.GetAll)  [L53–L57] status=200 [auth=Authentication.AdminPolicy]
  └─ sends_request GetSyncConfigurationsQuery [L56]
    └─ handled_by Dataverse.ApplicationService.Queries.SyncConfigurations.GetSyncConfigurationsQueryHandler.Handle [L32–L97]
      └─ maps_to SyncConfigurationDto [L56]
      └─ maps_to SyncConfigurationLightDto [L60]
      └─ uses_service IControlledRepository<SyncConfiguration>
        └─ method ReadQuery [L49]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method Map [L56]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L69]
          └─ implementation Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync [L8-L45]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle

