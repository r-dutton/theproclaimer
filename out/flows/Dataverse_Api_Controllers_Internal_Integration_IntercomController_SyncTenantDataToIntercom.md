[web] PUT /api/internal/intercom/sync-all  (Dataverse.Api.Controllers.Internal.Integration.IntercomController.SyncTenantDataToIntercom)  [L27–L39] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request SyncTenantDataToIntercomCommand [L32]
    └─ handled_by Dataverse.ApplicationService.Commands.Intercom.SyncTenantDataToIntercomCommandHandler.Handle [L38–L161]
      └─ calls TenantRepository.ReadTable [L74]
      └─ maps_to TenantLightDto [L74]
      └─ maps_to UserWithLicensesDto [L93]
        └─ automapper.registration DataverseMappingProfile (User->UserWithLicensesDto) [L90]
      └─ uses_service AsyncRetryPolicy
        └─ method ExecuteAsync [L124]
          └─ ... (no implementation details available)
      └─ uses_service IntercomService
        └─ method CreateOrUpdateCompany [L88]
          └─ implementation Dataverse.Services.ModuleIntegrations.Services.IntercomService.CreateOrUpdateCompany [L17-L124]
      └─ uses_service IControlledRepository<User>
        └─ method ReadQuery [L93]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L77]
          └─ ... (no implementation details available)
      └─ uses_service ITenantIdentificationService (AddScoped)
        └─ method AssignActiveTenant [L72]
          └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.AssignActiveTenant [L27-L149]

