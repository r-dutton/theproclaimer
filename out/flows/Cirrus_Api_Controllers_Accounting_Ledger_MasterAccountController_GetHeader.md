[web] GET /api/accounting/ledger/master-accounts/{id:int}  (Cirrus.Api.Controllers.Accounting.Ledger.MasterAccountController.GetHeader)  [L31–L37] [auth=Authentication.UserPolicy]
  └─ maps_to MasterAccountEditingDto [L34]
    └─ automapper.registration CirrusMappingProfile (MasterAccount->MasterAccountEditingDto) [L302]
  └─ calls MasterAccountRepository.ReadQuery [L34]
  └─ queries MasterAccount [L34]
    └─ reads_from MasterAccounts
  └─ uses_service IControlledRepository<MasterAccount>
    └─ method ReadQuery [L34]

