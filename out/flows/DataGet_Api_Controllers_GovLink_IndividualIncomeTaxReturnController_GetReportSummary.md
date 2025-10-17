[web] GET /api/gov-link/individual-income-tax-returns/summary  (DataGet.Api.Controllers.GovLink.IndividualIncomeTaxReturnController.GetReportSummary)  [L34–L42] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ uses_service AtoService
    └─ method GetIncomeTaxReportSummary [L39]
      └─ implementation GovLink.Application.Services.AtoService.GetIncomeTaxReportSummary [L12-L145]
  └─ impact_summary
    └─ services 1
      └─ AtoService

