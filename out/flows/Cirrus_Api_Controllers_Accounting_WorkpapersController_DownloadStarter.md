[web] GET /api/accounting/workpapers/starters/{id}/download  (Cirrus.Api.Controllers.Accounting.WorkpapersController.DownloadStarter)  [L101–L106] status=200 [auth=Authentication.UserPolicy]
  └─ uses_service WorkpapersService
    └─ method Get [L105]
      └─ implementation Cirrus.Central.Features.MachineToMachine.WorkpapersService.Get [L12-L30]
  └─ sends_request GetFirmForCurrentRequestQuery [L104]
    └─ handled_by Cirrus.Central.Queries.GetFirmForCurrentRequestQueryHandler.Handle [L19–L55]
      └─ uses_service IHttpContextAccessor
        └─ method HttpContext [L38]
          └─ ... (no implementation details available)
      └─ uses_service ILogger<GetFirmForCurrentRequestQueryHandler>
        └─ method LogWarning [L47]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method Process [L51]
          └─ implementation IRequestProcessor.Process [L9-L9]
          └─ ... (no implementation details available)
      └─ logs ILogger<GetFirmForCurrentRequestQueryHandler> [Warning] [L47]

