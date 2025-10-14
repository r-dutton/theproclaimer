[web] PUT /api/accounting/ledger/accounts/{id:Guid}/link-standard  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.LinkStandardAccount)  [L525–L529] [auth=user]
  └─ sends_request LinkToStandardAccountCommand [L528]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Ledger.LinkToStandardAccountCommandHandler.Handle [L45–L183]
      └─ uses_service IControlledRepository<Account>
        └─ method WriteQuery [L65]
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method ReadQuery [L101]
      └─ uses_service IControlledRepository<StandardAccount>
        └─ method ReadQuery [L69]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L63]

