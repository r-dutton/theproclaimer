[web] PUT /api/internal/intercom/sync-entity  (Dataverse.Api.Controllers.Internal.Integration.IntercomController.SyncEntityDataToIntercom)  [L41–L53] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request UpdateEntityDataToIntercomCommand [L46]
    └─ handled_by Dataverse.ApplicationService.Commands.Intercom.UpdateEntityDataToIntercomCommandHandler.Handle [L40–L113]
      └─ calls TenantRepository.ReadTable [L105]
      └─ maps_to TenantLightDto [L105]
      └─ maps_to UserWithLicensesDto [L81]
        └─ automapper.registration DataverseMappingProfile (User->UserWithLicensesDto) [L90]
      └─ uses_service IntercomService
        └─ method GetContactByExternalId [L89]
          └─ implementation Dataverse.Services.ModuleIntegrations.Services.IntercomService.GetContactByExternalId [L17-L124]
      └─ uses_service IControlledRepository<User>
        └─ method ReadQuery [L81]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L84]
          └─ ... (no implementation details available)
      └─ uses_service ITenantIdentificationService (AddScoped)
        └─ method AssignActiveTenant [L64]
          └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.AssignActiveTenant [L27-L149]

