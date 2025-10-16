[web] PUT /api/dataverse/clients  (Workpapers.Next.API.Controllers.DataverseController.UpdateClientChanges)  [L115–L121] status=200 [auth=AuthorizationPolicies.M2M,AuthorizationPolicies.RequireTenantId]
  └─ uses_service TenantService
    └─ method GetCurrentTenant [L119]
      └─ implementation Workpapers.Next.Services.Features.Tenants.TenantService.GetCurrentTenant [L5-L22]
        └─ uses_service TenantIdentificationService
          └─ method GetCurrentTenant [L20]
            └─ implementation Workpapers.Next.ApplicationService.Services.TenantIdentificationService.GetCurrentTenant [L15-L131]
  └─ sends_request ProcessDataverseClientCommand [L120]
    └─ handled_by Cirrus.Central.Dataverse.Commands.ProcessDataverseClientCommandHandler.Handle [L38–L251]
      └─ calls FileRepository.WriteQueryWithArchived [L106]
      └─ maps_to DataverseEntityMap [L246]
        └─ automapper.registration DataverseMappingProfile (User->DataverseEntityMap) [L73]
        └─ automapper.registration DataverseMappingProfile (User->DataverseEntityMap) [L82]
      └─ maps_to DataverseEntityMap [L235]
        └─ automapper.registration DataverseMappingProfile (User->DataverseEntityMap) [L73]
        └─ automapper.registration DataverseMappingProfile (User->DataverseEntityMap) [L82]
      └─ maps_to DataverseEntityMap [L223]
        └─ automapper.registration DataverseMappingProfile (Team->DataverseEntityMap) [L81]
      └─ maps_to DataverseEntityMap [L214]
        └─ automapper.registration DataverseMappingProfile (Office->DataverseEntityMap) [L80]
        └─ automapper.registration DataverseMappingProfile (Office->DataverseEntityMap) [L76]
      └─ uses_service FirmSettingsService
        └─ method GetFirmJurisdictions [L189]
          └─ implementation Cirrus.ApplicationService.Firm.FirmSettingsService.GetFirmJurisdictions [L15-L112]
      └─ uses_service IControlledRepository<Client>
        └─ method WriteQuery [L81]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Entity>
        └─ method ReadQuery [L114]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Office>
        └─ method ReadQuery [L178]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Team>
        └─ method ReadQuery [L223]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<User>
        └─ method ReadQuery [L235]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L216]
          └─ ... (no implementation details available)

