[web] PUT /api/internal/intercom/sync-all  (Dataverse.Api.Controllers.Internal.Integration.IntercomController.SyncTenantDataToIntercom)  [L27–L39] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request SyncTenantDataToIntercomCommand [L32]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Intercom.SyncTenantDataToIntercomCommandHandler.Handle [L38–L161]
      └─ calls TenantRepository.ReadTable [L74]
      └─ maps_to TenantLightDto [L74]
      └─ maps_to UserWithLicensesDto [L93]
        └─ automapper.registration DataverseMappingProfile (User->UserWithLicensesDto) [L89]
      └─ uses_service AsyncRetryPolicy
        └─ method ExecuteAsync [L124]
      └─ uses_service IControlledRepository<User>
        └─ method ReadQuery [L93]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L77]
      └─ uses_service IntercomService
        └─ method CreateOrUpdateCompany [L88]
      └─ uses_service ITenantIdentificationService (AddScoped)
        └─ method AssignActiveTenant [L72]

