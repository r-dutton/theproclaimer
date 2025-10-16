[web] GET /api/accounting/ledger/accounts/header/{id}  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.GetHeaderAccount)  [L127–L131] status=200 [auth=user]
  └─ sends_request GetHeaderAccountQuery [L130]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetAccountQueryHandler.Handle [L37–L82]
      └─ maps_to ChildAccountDto [L66]
      └─ maps_to HeaderAccountDto [L55]
      └─ uses_service IControlledRepository<Account>
        └─ method ReadQuery [L55]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L77]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)

