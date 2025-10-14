[web] PUT /api/accounting/ledger/source-accounts/unlink  (Cirrus.Api.Controllers.Accounting.Ledger.SourceAccountsController.Unlink)  [L201–L206] [auth=user]
  └─ sends_request UnlinkSourceAccountCommand [L204]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.UnlinkSourceAccountCommandHandler.Handle [L30–L72]
      └─ uses_service IControlledRepository<Account>
        └─ method WriteQuery [L50]
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method WriteQuery [L46]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L56]

