[web] GET /api/accounting/connections/{type}/authorisation-url  (Cirrus.Api.Controllers.Accounting.ConnectionsController.GetAuthorisationUrl)  [L37–L42] status=200 [auth=user]
  └─ uses_service ApiService (SingleInstance)
    └─ method GetApiMethods [L40]
      └─ implementation Cirrus.ApplicationService.Accounting.Services.ApiService.GetApiMethods [L14-L75]
  └─ impact_summary
    └─ services 1
      └─ ApiService

