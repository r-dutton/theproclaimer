[web] PUT /api/internal/intercom/entity-create  (Dataverse.Api.Controllers.Internal.Integration.IntercomController.CreateEntityInIntercom)  [L55–L67] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request CreateIntercomEntityCommand [L60]
    └─ handled_by Dataverse.ApplicationService.Commands.Intercom.CreateIntercomEntityCommandHandler.Handle [L41–L117]
      └─ calls TenantRepository.ReadTable [L109]
      └─ maps_to TenantLightDto [L109]
      └─ maps_to UserWithLicensesDto [L67]
        └─ automapper.registration DataverseMappingProfile (User->UserWithLicensesDto) [L90]
      └─ uses_service IntercomService
        └─ method GetContactByExternalId [L79]
          └─ implementation Dataverse.Services.ModuleIntegrations.Services.IntercomService.GetContactByExternalId [L17-L124]
      └─ uses_service IControlledRepository<User>
        └─ method ReadQuery [L67]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L70]
          └─ ... (no implementation details available)
      └─ uses_service ITenantIdentificationService (AddScoped)
        └─ method AssignActiveTenant [L65]
          └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.AssignActiveTenant [L27-L149]

