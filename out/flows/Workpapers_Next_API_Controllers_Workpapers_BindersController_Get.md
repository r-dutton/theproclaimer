[web] GET /api/binders/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.BindersController.Get)  [L137–L174] status=200 [auth=AuthorizationPolicies.User]
  └─ maps_to BinderDto [L151]
    └─ automapper.registration ExternalApiMappingProfile (Binder->BinderDto) [L58]
    └─ automapper.registration WorkpapersMappingProfile (Binder->BinderDto) [L450]
  └─ maps_to BinderUltraLightDto [L145]
    └─ automapper.registration WorkpapersMappingProfile (Binder->BinderUltraLightDto) [L440]
  └─ calls BinderRepository.ReadQuery [L145]
  └─ query Binder [L145]
    └─ reads_from Binders
  └─ uses_service ICirrusQueryService (AddScoped)
    └─ method GetDatasetsForFile [L159]
      └─ implementation Workpapers.Next.CirrusServices.CirrusQueryService.GetDatasetsForFile [L18-L250]
  └─ uses_service IControlledRepository<Binder>
    └─ method ReadQuery [L145]
      └─ ... (no implementation details available)
  └─ sends_request CanIAccessBinderQuery [L141]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Binders.CanIAccessBinderQueryHandler.Handle [L60–L126]
      └─ uses_service UserService
        └─ method GetUserId [L91]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L89]
          └─ implementation Workpapers.Next.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L84]
          └─ implementation Workpapers.Next.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L84]
          └─ implementation Workpapers.Next.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L84]
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L92]
          └─ implementation Workpapers.Next.Services.Features.Tenants.TenantService.GetCurrentTenant [L5-L22]
            └─ uses_service TenantIdentificationService
              └─ method GetCurrentTenant [L20]
                └─ implementation Workpapers.Next.ApplicationService.Services.TenantIdentificationService.GetCurrentTenant [L15-L131]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L101]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L117]
          └─ implementation Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync [L9-L32]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle
      └─ uses_cache IDistributedCache.SetRecordAsync [write] [L121]
      └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L109]
      └─ uses_cache IDistributedCache.CreateAccessKey [write] [L92]

