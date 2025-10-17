[web] GET /api/gov-link/activity-statements/detail  (DataGet.Api.Controllers.GovLink.ActivityStatementController.GetActivityStatementsDetail)  [L33–L42] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ uses_service AtoService
    └─ method GetActivityStatements [L38]
      └─ implementation GovLink.Application.Services.AtoService.GetActivityStatements [L12-L145]
  └─ impact_summary
    └─ services 1
      └─ AtoService

