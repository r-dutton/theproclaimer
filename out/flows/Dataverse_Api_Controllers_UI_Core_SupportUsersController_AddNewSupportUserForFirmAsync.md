[web] POST /api/ui/support-users  (Dataverse.Api.Controllers.UI.Core.SupportUsersController.AddNewSupportUserForFirmAsync)  [L59–L64] status=201 [auth=Authentication.AdminPolicy]
  └─ sends_request AddSupportUserCommand [L62]
    └─ handled_by Dataverse.ApplicationService.Commands.Users.AddSupportUserCommandHandler.Handle [L34–L149]
      └─ calls UserRepository.ReadQuery [L77]
      └─ calls TenantRepository.ReadTable [L64]
      └─ uses_service TenantService
        └─ method GetCurrentTenantAsync [L61]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
      └─ uses_service TenantLicenseService
        └─ method GetFirmSubscriptions [L113]
          └─ implementation Dataverse.ApplicationService.Services.TenantLicenseService.GetFirmSubscriptions [L22-L185]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L62]
          └─ implementation Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync [L8-L45]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle
      └─ uses_service UnitOfWork
        └─ method Table [L129]
          └─ ... (no implementation details available)

