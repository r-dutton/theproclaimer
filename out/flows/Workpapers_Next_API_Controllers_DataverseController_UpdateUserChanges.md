[web] PUT /api/dataverse/users  (Workpapers.Next.API.Controllers.DataverseController.UpdateUserChanges)  [L92–L99] status=200 [auth=AuthorizationPolicies.M2M,AuthorizationPolicies.RequireTenantId]
  └─ uses_service TenantService
    └─ method GetCurrentTenant [L97]
      └─ implementation Workpapers.Next.Services.Features.Tenants.TenantService.GetCurrentTenant [L5-L22]
        └─ uses_service TenantIdentificationService
          └─ method GetCurrentTenant [L20]
            └─ implementation Workpapers.Next.ApplicationService.Services.TenantIdentificationService.GetCurrentTenant [L15-L131]
  └─ sends_request ProcessDataverseUserCommand [L98]
    └─ handled_by Cirrus.Central.Dataverse.Commands.ProcessDataverseUserCommandHandler.Handle [L28–L89]
      └─ maps_to DataverseEntityMap [L62]
        └─ automapper.registration DataverseMappingProfile (Team->DataverseEntityMap) [L81]
      └─ maps_to DataverseEntityMap [L56]
        └─ automapper.registration DataverseMappingProfile (Office->DataverseEntityMap) [L80]
        └─ automapper.registration DataverseMappingProfile (Office->DataverseEntityMap) [L76]
      └─ uses_service IControlledRepository<Office>
        └─ method ReadQuery [L56]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Team>
        └─ method ReadQuery [L62]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<User>
        └─ method WriteQuery [L46]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L59]
          └─ ... (no implementation details available)

