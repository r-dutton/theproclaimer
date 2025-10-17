[web] GET /api/accounting/ledger/source-accounts/for-source/{sourceId}/from-api  (Cirrus.Api.Controllers.Accounting.Ledger.SourceAccountsController.GetAllForSourceFromApi)  [L139–L143] status=200 [auth=user]
  └─ sends_request GetAccountsFromApiQuery -> ImportFromApiCommandHandler [L142]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.ImportFromApiCommandHandler.Handle [L34–L61]
      └─ uses_service ApiService (SingleInstance)
        └─ method GetApiMethods [L56]
          └─ implementation Cirrus.ApplicationService.Accounting.Services.ApiService.GetApiMethods [L14-L75]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L54]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
      └─ uses_service IControlledRepository<Source> (Scoped (inferred))
        └─ method ReadQuery [L51]
          └─ implementation Cirrus.Data.Repository.Accounting.SourceData.SourceRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ GetAccountsFromApiQuery
    └─ handlers 1
      └─ ImportFromApiCommandHandler

