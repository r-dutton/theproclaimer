[web] POST /api/accounting/ledger/accounts/header-matches/siblings  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.GetHeaderMatchSiblings)  [L263–L267] status=201 [auth=user]
  └─ sends_request GetHeaderMatchSiblingsQuery -> GetHeaderMatchSiblingsQueryHandler [L266]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Ledger.GetHeaderMatchSiblingsQueryHandler.Handle [L33–L122]
      └─ maps_to AccountLightWithMappingDto [L94]
        └─ automapper.registration CirrusMappingProfile (Account->AccountLightWithMappingDto) [L323]
      └─ uses_service IControlledRepository<Account> (Scoped (inferred))
        └─ method ReadQuery [L73]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.AccountRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ GetHeaderMatchSiblingsQuery
    └─ handlers 1
      └─ GetHeaderMatchSiblingsQueryHandler
    └─ mappings 1
      └─ AccountLightWithMappingDto

