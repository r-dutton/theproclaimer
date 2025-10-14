[web] GET /api/accounting/ledger/accounts/header/{id}  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.GetHeaderAccount)  [L127–L131] [auth=user]
  └─ sends_request GetHeaderAccountQuery [L130]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetAccountQueryHandler.Handle [L37–L82]
      └─ maps_to ChildAccountDto [L66]
      └─ maps_to HeaderAccountDto [L55]
      └─ uses_service IControlledRepository<Account>
        └─ method ReadQuery [L55]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L77]

