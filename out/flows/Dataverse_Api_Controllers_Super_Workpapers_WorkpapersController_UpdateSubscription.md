[web] PUT /api/super/workpapers/{tenantId}/subscription  (Dataverse.Api.Controllers.Super.Workpapers.WorkpapersController.UpdateSubscription)  [L118–L143] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to TenantLightDto [L135]
  └─ uses_client WorkpapersClient [L137]
  └─ calls TenantRepository.WriteTable [L125]
  └─ write Tenant [L125]
    └─ reads_from Tenants
  └─ uses_service IMapper
    └─ method Map [L135]
      └─ ... (no implementation details available)
  └─ uses_service TenantRepository
    └─ method WriteTable [L125]
      └─ implementation Dataverse.Tenants.Data.TenantRepository.WriteTable [L15-L180]
  └─ sends_request CreateOrUpdateSubscriptionCommand [L139]
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

