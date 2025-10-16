[web] GET /api/accounting/ledger/accounttypes/{id}  (Cirrus.Api.Controllers.Accounting.Ledger.AccountTypesController.Get)  [L41–L47] status=200 [auth=user]
  └─ maps_to AccountTypeLightDto [L44]
    └─ automapper.registration CirrusMappingProfile (AccountType->AccountTypeLightDto) [L246]
    └─ automapper.registration WorkpapersMappingProfile (AccountType->AccountTypeLightDto) [L707]
  └─ calls AccountTypeRepository.ReadQuery [L44]
  └─ query AccountType [L44]
    └─ reads_from AccountTypes
  └─ uses_service IControlledRepository<AccountType>
    └─ method ReadQuery [L44]
      └─ ... (no implementation details available)

