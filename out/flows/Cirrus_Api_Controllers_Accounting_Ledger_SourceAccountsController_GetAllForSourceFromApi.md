[web] GET /api/accounting/ledger/source-accounts/for-source/{sourceId}/from-api  (Cirrus.Api.Controllers.Accounting.Ledger.SourceAccountsController.GetAllForSourceFromApi)  [L139–L143] status=200 [auth=user]
  └─ sends_request GetAccountsFromApiQuery [L142]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.ImportFromApiCommandHandler.Handle [L34–L61]
      └─ uses_service ApiService (SingleInstance)
        └─ method GetApiMethods [L56]
          └─ implementation Cirrus.ApplicationService.Accounting.Services.ApiService.GetApiMethods [L14-L75]
      └─ uses_service IControlledRepository<Source>
        └─ method ReadQuery [L51]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L54]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)

