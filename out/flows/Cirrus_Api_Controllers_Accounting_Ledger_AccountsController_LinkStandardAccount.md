[web] PUT /api/accounting/ledger/accounts/{id:Guid}/link-standard  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.LinkStandardAccount)  [L525–L529] status=200 [auth=user]
  └─ sends_request LinkToStandardAccountCommand -> LinkToStandardAccountCommandHandler [L528]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Ledger.LinkToStandardAccountCommandHandler.Handle [L45–L183]
      └─ uses_service IControlledRepository<SourceAccount> (Scoped (inferred))
        └─ method ReadQuery [L101]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.SourceAccountRepository.ReadQuery
      └─ uses_service IControlledRepository<StandardAccount> (Scoped (inferred))
        └─ method ReadQuery [L69]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.StandardAccountRepository.ReadQuery
      └─ uses_service IControlledRepository<Account> (Scoped (inferred))
        └─ method WriteQuery [L65]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.AccountRepository.WriteQuery
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L63]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
  └─ impact_summary
    └─ requests 1
      └─ LinkToStandardAccountCommand
    └─ handlers 1
      └─ LinkToStandardAccountCommandHandler

