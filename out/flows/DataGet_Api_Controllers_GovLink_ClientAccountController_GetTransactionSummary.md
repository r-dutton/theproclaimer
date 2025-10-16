[web] GET /api/gov-link/client-accounts/summary  (DataGet.Api.Controllers.GovLink.ClientAccountController.GetTransactionSummary)  [L33–L42] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ uses_service AtoService
    └─ method GetClientAccountSummary [L38]
      └─ implementation GovLink.Application.Services.AtoService.GetClientAccountSummary [L12-L145]
  └─ impact_summary
    └─ services 1
      └─ AtoService

