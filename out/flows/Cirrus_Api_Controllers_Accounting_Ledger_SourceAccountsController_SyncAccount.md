[web] PUT /api/accounting/ledger/source-accounts/{id:guid}/sync  (Cirrus.Api.Controllers.Accounting.Ledger.SourceAccountsController.SyncAccount)  [L223–L228] [auth=user]
  └─ sends_request SyncSourceAccountCommand [L226]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.SyncSourceAccountCommandHandler.Handle [L21–L50]
      └─ uses_service ApiService (SingleInstance)
        └─ method GetApiMethods [L44]
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method WriteQuery [L32]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L46]

