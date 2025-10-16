[web] GET /api/workpapers/auto-reconcile/debtors  (Cirrus.Api.Controllers.Workpapers.AutoReconcileController.GetDebtors)  [L37–L43] status=200 [auth=Authentication.UserPolicy]
  └─ uses_service ApiService (SingleInstance)
    └─ method GetApiMethods [L41]
      └─ implementation Cirrus.ApplicationService.Accounting.Services.ApiService.GetApiMethods [L14-L75]
  └─ impact_summary
    └─ services 1
      └─ ApiService

