[web] POST /api/super/smart-workpapers/{tenantId}/subscription  (Dataverse.Api.Controllers.Super.Workpapers.SmartWorkpapersController.CreateSubscription)  [L80–L118] status=201 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to TenantLightDto [L111]
  └─ maps_to TenantLightDto [L101]
  └─ uses_client WorkpapersClient [L112]
  └─ uses_client WorkpapersClient [L102]
  └─ uses_client WorkpapersClient [L96]
  └─ calls TenantRepository.WriteTable [L87]
  └─ write Tenant [L87]
    └─ reads_from Tenants
  └─ uses_service IMapper
    └─ method Map [L101]
      └─ ... (no implementation details available)
  └─ uses_service TenantRepository
    └─ method WriteTable [L87]
      └─ implementation Dataverse.Tenants.Data.TenantRepository.WriteTable [L15-L180]
  └─ sends_request CreateOrUpdateSubscriptionCommand [L115]
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

