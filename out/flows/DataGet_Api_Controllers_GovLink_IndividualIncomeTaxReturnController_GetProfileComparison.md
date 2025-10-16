[web] GET /api/gov-link/individual-income-tax-returns/profile-compare  (DataGet.Api.Controllers.GovLink.IndividualIncomeTaxReturnController.GetProfileComparison)  [L44–L53] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ uses_service AtoService
    └─ method GetIncomeTaxProfileComparison [L49]
      └─ implementation GovLink.Application.Services.AtoService.GetIncomeTaxProfileComparison [L12-L145]
  └─ impact_summary
    └─ services 1
      └─ AtoService

