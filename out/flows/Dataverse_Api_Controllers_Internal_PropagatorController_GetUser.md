[web] GET /api/internal/Propagator/user/{id:guid}  (Dataverse.Api.Controllers.Internal.PropagatorController.GetUser)  [L113–L117] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to UserWithLicensesDto [L116]
    └─ automapper.registration DataverseMappingProfile (User->UserWithLicensesDto) [L90]
  └─ calls UserRepository.ReadQuery [L116]
  └─ query User [L116]
  └─ uses_service UserRepository
    └─ method ReadQuery [L116]
      └─ implementation Dataverse.Data.Repository.Users.UserRepository.ReadQuery [L8-L51]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ User writes=0 reads=1
    └─ services 1
      └─ UserRepository
    └─ mappings 1
      └─ UserWithLicensesDto

