[web] DELETE /api/accounting/ledger/accounts/header/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.Delete)  [L483–L488] status=200 [auth=user]
  └─ sends_request DeleteHeaderCommand [L486]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.DeleteHeaderCommandHandler.Handle [L28–L58]
      └─ uses_service IControlledRepository<Account>
        └─ method ReadQuery [L42]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L50]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)

