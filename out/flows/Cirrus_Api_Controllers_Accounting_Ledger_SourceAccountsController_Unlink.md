[web] PUT /api/accounting/ledger/source-accounts/unlink  (Cirrus.Api.Controllers.Accounting.Ledger.SourceAccountsController.Unlink)  [L201–L206] status=200 [auth=user]
  └─ sends_request UnlinkSourceAccountCommand [L204]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.UnlinkSourceAccountCommandHandler.Handle [L30–L72]
      └─ uses_service IControlledRepository<Account>
        └─ method WriteQuery [L50]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method WriteQuery [L46]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L56]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)

