[web] PUT /api/accounting/ledger/source-accounts/{id:guid}/sync  (Cirrus.Api.Controllers.Accounting.Ledger.SourceAccountsController.SyncAccount)  [L223–L228] status=200 [auth=user]
  └─ sends_request SyncSourceAccountCommand [L226]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.SyncSourceAccountCommandHandler.Handle [L21–L50]
      └─ uses_service ApiService (SingleInstance)
        └─ method GetApiMethods [L44]
          └─ implementation Cirrus.ApplicationService.Accounting.Services.ApiService.GetApiMethods [L14-L75]
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method WriteQuery [L32]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L46]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)

