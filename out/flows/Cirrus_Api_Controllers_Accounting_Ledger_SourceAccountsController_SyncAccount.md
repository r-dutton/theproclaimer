[web] PUT /api/accounting/ledger/source-accounts/{id:guid}/sync  (Cirrus.Api.Controllers.Accounting.Ledger.SourceAccountsController.SyncAccount)  [L223–L228] status=200 [auth=user]
  └─ sends_request SyncSourceAccountCommand -> SyncSourceAccountCommandHandler [L226]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.SyncSourceAccountCommandHandler.Handle [L21–L50]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L46]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
      └─ uses_service ApiService (SingleInstance)
        └─ method GetApiMethods [L44]
          └─ implementation Cirrus.ApplicationService.Accounting.Services.ApiService.GetApiMethods [L14-L75]
      └─ uses_service IControlledRepository<SourceAccount> (Scoped (inferred))
        └─ method WriteQuery [L32]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.SourceAccountRepository.WriteQuery
  └─ impact_summary
    └─ requests 1
      └─ SyncSourceAccountCommand
    └─ handlers 1
      └─ SyncSourceAccountCommandHandler

