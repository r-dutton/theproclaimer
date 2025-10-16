[web] GET /api/accounting/ledger/accounttypes/{id}  (Cirrus.Api.Controllers.Accounting.Ledger.AccountTypesController.Get)  [L41–L47] status=200 [auth=user]
  └─ maps_to AccountTypeLightDto [L44]
    └─ automapper.registration CirrusMappingProfile (AccountType->AccountTypeLightDto) [L246]
  └─ calls AccountTypeRepository.ReadQuery [L44]
  └─ query AccountType [L44]
    └─ reads_from AccountTypes
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ AccountType writes=0 reads=1
    └─ mappings 1
      └─ AccountTypeLightDto

