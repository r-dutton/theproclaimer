[web] GET /ledger/v1/standard-charts/{standardChartId:int}/accounts  (Cirrus.Api.External.Controllers.v1.Ledger.StandardChartsController.GetAccountsHierarchy)  [L60–L81] status=200
  └─ uses_service ICirrusProxyService (AddScoped)
    └─ method Get [L80]
      └─ implementation Cirrus.Api.External.Features.CirrusProxy.CirrusProxyService.Get [L10-L73]
        └─ uses_client CirrusClient [L26]
        └─ uses_service CirrusClient
          └─ method Get [L26]
            └─ ... (no implementation details available)

