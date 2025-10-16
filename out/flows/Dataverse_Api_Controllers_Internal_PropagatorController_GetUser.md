[web] GET /api/internal/Propagator/user/{id:guid}  (Dataverse.Api.Controllers.Internal.PropagatorController.GetUser)  [L113–L117] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to UserWithLicensesDto [L116]
    └─ automapper.registration DataverseMappingProfile (User->UserWithLicensesDto) [L90]
  └─ calls UserRepository.ReadQuery [L116]
  └─ query User [L116]
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L116]
      └─ ... (no implementation details available)

