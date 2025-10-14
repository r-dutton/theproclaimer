[web] GET /api/accounting/workpapers/starters  (Cirrus.Api.Controllers.Accounting.WorkpapersController.GetStarters)  [L68–L95] [auth=Authentication.UserPolicy]
  └─ uses_service WorkpapersService
    └─ method Get [L76]
  └─ sends_request GetFirmForCurrentRequestQuery [L71]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.Central.Queries.GetFirmForCurrentRequestQueryHandler.Handle [L19–L55]
      └─ uses_service IHttpContextAccessor
        └─ method HttpContext [L38]
      └─ uses_service ILogger<GetFirmForCurrentRequestQueryHandler>
        └─ method LogWarning [L47]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method Process [L51]

