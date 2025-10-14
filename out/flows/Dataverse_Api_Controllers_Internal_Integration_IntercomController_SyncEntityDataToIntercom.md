[web] PUT /api/internal/intercom/sync-entity  (Dataverse.Api.Controllers.Internal.Integration.IntercomController.SyncEntityDataToIntercom)  [L41–L53] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request UpdateEntityDataToIntercomCommand [L46]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Intercom.UpdateEntityDataToIntercomCommandHandler.Handle [L40–L113]
      └─ calls TenantRepository.ReadTable [L105]
      └─ maps_to TenantLightDto [L105]
      └─ maps_to UserWithLicensesDto [L81]
        └─ automapper.registration DataverseMappingProfile (User->UserWithLicensesDto) [L89]
      └─ uses_service IControlledRepository<User>
        └─ method ReadQuery [L81]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L84]
      └─ uses_service IntercomService
        └─ method GetContactByExternalId [L89]
      └─ uses_service ITenantIdentificationService (AddScoped)
        └─ method AssignActiveTenant [L64]

