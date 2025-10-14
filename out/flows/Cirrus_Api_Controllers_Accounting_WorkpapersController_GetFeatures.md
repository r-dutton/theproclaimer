[web] GET /api/accounting/workpapers/features  (Cirrus.Api.Controllers.Accounting.WorkpapersController.GetFeatures)  [L35–L62] [auth=Authentication.UserPolicy]
  └─ uses_service WorkpapersService
    └─ method Get [L43]
  └─ sends_request GetFirmForCurrentRequestQuery [L38]
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

