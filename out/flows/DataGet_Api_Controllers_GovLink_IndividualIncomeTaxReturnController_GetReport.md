[web] GET /api/gov-link/individual-income-tax-returns/  (DataGet.Api.Controllers.GovLink.IndividualIncomeTaxReturnController.GetReport)  [L23–L32] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ uses_service AtoService
    └─ method GetIncomeTaxReport [L28]
      └─ implementation GovLink.Application.Services.AtoService.GetIncomeTaxReport [L12-L145]
  └─ impact_summary
    └─ services 1
      └─ AtoService

