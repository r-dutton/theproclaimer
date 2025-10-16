[web] GET /api/users/{id:Guid}  (Workpapers.Next.API.Controllers.UsersController.Get)  [L108–L117] status=200
  └─ maps_to UserDto [L111]
    └─ automapper.registration WorkpapersMappingProfile (User->UserDto) [L106]
  └─ calls UserRepository.ReadQuery [L111]
  └─ query User [L111]
  └─ uses_service UserRepository
    └─ method ReadQuery [L111]
      └─ implementation Workpapers.Next.Data.Repository.Firms.UserRepository.ReadQuery [L9-L41]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ User writes=0 reads=1
    └─ services 1
      └─ UserRepository
    └─ mappings 1
      └─ UserDto

