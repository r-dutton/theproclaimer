[web] GET /api/accounting/ledger/accounts/header/{id}  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.GetHeaderAccount)  [L127–L131] status=200 [auth=user]
  └─ sends_request GetHeaderAccountQuery -> GetAccountQueryHandler [L130]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetAccountQueryHandler.Handle [L37–L82]
      └─ maps_to ChildAccountDto [L66]
      └─ maps_to HeaderAccountDto [L55]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L77]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
      └─ uses_service IControlledRepository<Account> (Scoped (inferred))
        └─ method ReadQuery [L55]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.AccountRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ GetHeaderAccountQuery
    └─ handlers 1
      └─ GetAccountQueryHandler
    └─ mappings 2
      └─ ChildAccountDto
      └─ HeaderAccountDto

