[web] POST /api/super/support-users  (Dataverse.Api.Controllers.Super.Core.SupportUsersController.AddNewSupportUserForFirmAsync)  [L36–L41] [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request AddSupportUserCommand [L39]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Users.AddSupportUserCommandHandler.Handle [L34–L149]
      └─ calls UserRepository.ReadQuery [L77]
      └─ calls TenantRepository.ReadTable [L64]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L62]
      └─ uses_service TenantLicenseService
        └─ method GetFirmSubscriptions [L113]
      └─ uses_service TenantService
        └─ method GetCurrentTenantAsync [L61]
      └─ uses_service UnitOfWork
        └─ method Table [L129]

