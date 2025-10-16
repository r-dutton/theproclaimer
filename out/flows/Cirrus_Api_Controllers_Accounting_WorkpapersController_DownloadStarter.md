[web] GET /api/accounting/workpapers/starters/{id}/download  (Cirrus.Api.Controllers.Accounting.WorkpapersController.DownloadStarter)  [L101–L106] status=200 [auth=Authentication.UserPolicy]
  └─ uses_service WorkpapersService
    └─ method Get [L105]
      └─ implementation Cirrus.Central.Features.MachineToMachine.WorkpapersService.Get [L12-L30]
  └─ sends_request GetFirmForCurrentRequestQuery -> GetFirmForCurrentRequestQueryHandler [L104]
    └─ handled_by Cirrus.Central.Queries.GetFirmForCurrentRequestQueryHandler.Handle [L19–L55]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method Process [L51]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.Process [L7-L35]
      └─ logs ILogger<GetFirmForCurrentRequestQueryHandler> [Warning] [L47]
  └─ impact_summary
    └─ services 1
      └─ WorkpapersService
    └─ requests 1
      └─ GetFirmForCurrentRequestQuery
    └─ handlers 1
      └─ GetFirmForCurrentRequestQueryHandler

