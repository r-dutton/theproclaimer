[web] GET /api/gov-link/activity-statements/  (DataGet.Api.Controllers.GovLink.ActivityStatementController.GetActivityStatements)  [L22–L31] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ uses_service AtoService
    └─ method GetActivityStatementPackage [L27]
      └─ implementation GovLink.Application.Services.AtoService.GetActivityStatementPackage [L12-L145]
  └─ impact_summary
    └─ services 1
      └─ AtoService

