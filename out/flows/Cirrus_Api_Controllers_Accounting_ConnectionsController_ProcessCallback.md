[web] POST /api/accounting/connections/{type}/processCallback  (Cirrus.Api.Controllers.Accounting.ConnectionsController.ProcessCallback)  [L47–L52] status=201 [auth=user]
  └─ uses_service ApiService (SingleInstance)
    └─ method GetApiMethods [L50]
      └─ implementation Cirrus.ApplicationService.Accounting.Services.ApiService.GetApiMethods [L14-L75]
  └─ impact_summary
    └─ services 1
      └─ ApiService

