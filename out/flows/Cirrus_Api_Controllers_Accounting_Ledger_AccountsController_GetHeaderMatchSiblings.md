[web] POST /api/accounting/ledger/accounts/header-matches/siblings  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.GetHeaderMatchSiblings)  [L263–L267] [auth=user]
  └─ sends_request GetHeaderMatchSiblingsQuery [L266]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Ledger.GetHeaderMatchSiblingsQueryHandler.Handle [L33–L122]
      └─ maps_to AccountLightWithMappingDto [L94]
        └─ automapper.registration CirrusMappingProfile (Account->AccountLightWithMappingDto) [L323]
      └─ uses_service IControlledRepository<Account>
        └─ method ReadQuery [L73]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L99]

