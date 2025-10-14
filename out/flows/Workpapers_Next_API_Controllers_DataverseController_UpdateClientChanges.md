[web] PUT /api/dataverse/clients  (Workpapers.Next.API.Controllers.DataverseController.UpdateClientChanges)  [L115–L121] [auth=AuthorizationPolicies.M2M,AuthorizationPolicies.RequireTenantId]
  └─ uses_service TenantService
    └─ method GetCurrentTenant [L119]
  └─ sends_request ProcessDataverseClientCommand [L120]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
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
      └─ uses_service IControlledRepository<Client>
        └─ method WriteQuery [L81]
      └─ uses_service IControlledRepository<Entity>
        └─ method ReadQuery [L114]
      └─ uses_service IControlledRepository<Office>
        └─ method ReadQuery [L178]
      └─ uses_service IControlledRepository<Team>
        └─ method ReadQuery [L223]
      └─ uses_service IControlledRepository<User>
        └─ method ReadQuery [L235]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L216]

