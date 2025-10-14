[web] GET /api/accounting/ledger/accounttypes/workpaper-sections  (Cirrus.Api.Controllers.Accounting.Ledger.AccountTypesController.GetWithWorkpaperSections)  [L63–L69] [auth=user]
  └─ maps_to AccountTypeWithWorkpaperSectionDto [L66]
    └─ automapper.registration CirrusMappingProfile (AccountType->AccountTypeWithWorkpaperSectionDto) [L252]
  └─ calls AccountTypeRepository.ReadQuery [L66]
  └─ queries AccountType [L66]
    └─ reads_from AccountTypes
  └─ uses_service IControlledRepository<AccountType>
    └─ method ReadQuery [L66]

