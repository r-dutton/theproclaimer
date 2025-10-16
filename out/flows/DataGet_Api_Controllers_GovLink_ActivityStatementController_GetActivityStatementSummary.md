[web] GET /api/gov-link/activity-statements/summary  (DataGet.Api.Controllers.GovLink.ActivityStatementController.GetActivityStatementSummary)  [L44–L53] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ uses_service AtoService
    └─ method GetActivityStatementSummary [L49]
      └─ implementation GovLink.Application.Services.AtoService.GetActivityStatementSummary [L12-L145]
  └─ impact_summary
    └─ services 1
      └─ AtoService

