[web] GET /api/accounting/ledger/master-accounts/{id:int}  (Cirrus.Api.Controllers.Accounting.Ledger.MasterAccountController.GetHeader)  [L31–L37] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to MasterAccountEditingDto [L34]
    └─ automapper.registration CirrusMappingProfile (MasterAccount->MasterAccountEditingDto) [L302]
  └─ calls MasterAccountRepository.ReadQuery [L34]
  └─ query MasterAccount [L34]
    └─ reads_from MasterAccounts
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ MasterAccount writes=0 reads=1
    └─ mappings 1
      └─ MasterAccountEditingDto

