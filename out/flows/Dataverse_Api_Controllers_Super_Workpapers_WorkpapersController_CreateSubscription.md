[web] POST /api/super/workpapers/{tenantId:Guid}/subscription  (Dataverse.Api.Controllers.Super.Workpapers.WorkpapersController.CreateSubscription)  [L75–L116] status=201 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to TenantLightDto [L109]
  └─ maps_to TenantLightDto [L99]
  └─ uses_client WorkpapersClient [L110]
  └─ uses_client WorkpapersClient [L100]
  └─ uses_client WorkpapersClient [L94]
  └─ calls TenantRepository.WriteTable [L82]
  └─ write Tenant [L82]
    └─ reads_from Tenants
  └─ uses_service IMapper
    └─ method Map [L99]
      └─ ... (no implementation details available)
  └─ uses_service TenantRepository
    └─ method WriteTable [L82]
      └─ implementation Dataverse.Tenants.Data.TenantRepository.WriteTable [L15-L180]
  └─ sends_request CreateOrUpdateSubscriptionCommand [L113]
    └─ handled_by Dataverse.ApplicationService.Commands.Subscriptions.CreateOrUpdateSubscriptionCommandHandler.Handle [L40–L86]
      └─ calls TenantRepository.WriteTable [L55]
      └─ uses_service IControlledRepository<DocumentStore>
        └─ method ReadQuery [L79]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L74]
          └─ implementation Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync [L8-L45]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle

