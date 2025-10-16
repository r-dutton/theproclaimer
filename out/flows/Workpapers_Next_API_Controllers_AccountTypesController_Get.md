[web] GET /api/account-types/{id:int}  (Workpapers.Next.API.Controllers.AccountTypesController.Get)  [L42–L50] status=200 [auth=AuthorizationPolicies.User]
  └─ maps_to AccountTypeDto [L45]
    └─ automapper.registration CirrusMappingProfile (AccountType->AccountTypeDto) [L244]
    └─ automapper.registration WorkpapersMappingProfile (AccountType->AccountTypeDto) [L709]
  └─ calls AccountTypeRepository.ReadQuery [L45]
  └─ query AccountType [L45]
    └─ reads_from AccountTypes
  └─ uses_service IControlledRepository<AccountType>
    └─ method ReadQuery [L45]
      └─ ... (no implementation details available)

