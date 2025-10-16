[web] GET /api/super/sync-configuration/{id:Guid}  (Dataverse.Api.Controllers.Super.SyncConfigurationController.Get)  [L50–L54] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request GetSyncConfigurationQuery [L53]
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

