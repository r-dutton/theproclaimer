[web] DELETE /api/accounting/ledger/accounts/header/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.Delete)  [L483–L488] status=200 [auth=user]
  └─ sends_request DeleteHeaderCommand -> DeleteHeaderCommandHandler [L486]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.DeleteHeaderCommandHandler.Handle [L28–L58]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L50]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
      └─ uses_service IControlledRepository<Account> (Scoped (inferred))
        └─ method ReadQuery [L42]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.AccountRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ DeleteHeaderCommand
    └─ handlers 1
      └─ DeleteHeaderCommandHandler

