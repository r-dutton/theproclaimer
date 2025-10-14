[web] PUT /api/internal/intercom/entity-create  (Dataverse.Api.Controllers.Internal.Integration.IntercomController.CreateEntityInIntercom)  [L55–L67] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request CreateIntercomEntityCommand [L60]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Intercom.CreateIntercomEntityCommandHandler.Handle [L41–L117]
      └─ calls TenantRepository.ReadTable [L109]
      └─ maps_to TenantLightDto [L109]
      └─ maps_to UserWithLicensesDto [L67]
        └─ automapper.registration DataverseMappingProfile (User->UserWithLicensesDto) [L89]
      └─ uses_service IControlledRepository<User>
        └─ method ReadQuery [L67]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L70]
      └─ uses_service IntercomService
        └─ method GetContactByExternalId [L79]
      └─ uses_service ITenantIdentificationService (AddScoped)
        └─ method AssignActiveTenant [L65]

