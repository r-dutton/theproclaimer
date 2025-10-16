[web] PUT /api/accounting/ledger/source-accounts/unlink  (Cirrus.Api.Controllers.Accounting.Ledger.SourceAccountsController.Unlink)  [L201–L206] status=200 [auth=user]
  └─ sends_request UnlinkSourceAccountCommand -> UnlinkSourceAccountCommandHandler [L204]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.UnlinkSourceAccountCommandHandler.Handle [L30–L72]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L56]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
      └─ uses_service IControlledRepository<Account> (Scoped (inferred))
        └─ method WriteQuery [L50]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.AccountRepository.WriteQuery
      └─ uses_service IControlledRepository<SourceAccount> (Scoped (inferred))
        └─ method WriteQuery [L46]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.SourceAccountRepository.WriteQuery
  └─ impact_summary
    └─ requests 1
      └─ UnlinkSourceAccountCommand
    └─ handlers 1
      └─ UnlinkSourceAccountCommandHandler

