[web] GET /api/account-types/{id:int}  (Workpapers.Next.API.Controllers.AccountTypesController.Get)  [L42–L50] status=200 [auth=AuthorizationPolicies.User]
  └─ maps_to AccountTypeDto [L45]
    └─ automapper.registration WorkpapersMappingProfile (AccountType->AccountTypeDto) [L709]
  └─ calls AccountTypeRepository.ReadQuery [L45]
  └─ query AccountType [L45]
    └─ reads_from AccountTypes
  └─ uses_service AccountTypeRepository
    └─ method ReadQuery [L45]
      └─ implementation Workpapers.Next.Data.Repository.Workpapers.AccountTypeRepository.ReadQuery [L8-L38]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ AccountType writes=0 reads=1
    └─ services 1
      └─ AccountTypeRepository
    └─ mappings 1
      └─ AccountTypeDto

