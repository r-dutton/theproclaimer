[web] DELETE /api/accounting/ledger/accounts/header/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.Delete)  [L483–L488] [auth=user]
  └─ sends_request DeleteHeaderCommand [L486]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.DeleteHeaderCommandHandler.Handle [L28–L58]
      └─ uses_service IControlledRepository<Account>
        └─ method ReadQuery [L42]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L50]

