[web] GET /api/accounting/ledger/source-accounts/for-source/{sourceId}/from-api  (Cirrus.Api.Controllers.Accounting.Ledger.SourceAccountsController.GetAllForSourceFromApi)  [L139–L143] [auth=user]
  └─ sends_request GetAccountsFromApiQuery [L142]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.ImportFromApiCommandHandler.Handle [L34–L61]
      └─ uses_service ApiService (SingleInstance)
        └─ method GetApiMethods [L56]
      └─ uses_service IControlledRepository<Source>
        └─ method ReadQuery [L51]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L54]

