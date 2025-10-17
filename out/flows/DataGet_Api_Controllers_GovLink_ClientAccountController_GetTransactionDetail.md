[web] GET /api/gov-link/client-accounts/detail  (DataGet.Api.Controllers.GovLink.ClientAccountController.GetTransactionDetail)  [L22–L31] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ uses_service AtoService
    └─ method GetClientAccountTransactions [L27]
      └─ implementation GovLink.Application.Services.AtoService.GetClientAccountTransactions [L12-L145]
  └─ impact_summary
    └─ services 1
      └─ AtoService

