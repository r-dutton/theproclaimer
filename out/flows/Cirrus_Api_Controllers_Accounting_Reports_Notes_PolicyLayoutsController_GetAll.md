[web] GET /api/accounting/reports/notes/policy-layouts/  (Cirrus.Api.Controllers.Accounting.Reports.Notes.PolicyLayoutsController.GetAll)  [L39–L49] [auth=Authentication.AdminPolicy]
  └─ maps_to PolicyLayoutLightDto [L44]
    └─ automapper.registration CirrusMappingProfile (PolicyLayout->PolicyLayoutLightDto) [L782]
  └─ calls PolicyLayoutRepository.ReadQuery [L44]
  └─ queries PolicyLayout [L44]
    └─ reads_from PolicyLayouts
  └─ uses_service IControlledRepository<PolicyLayout>
    └─ method ReadQuery [L44]
  └─ uses_service IJurisdictionService (AddScoped)
    └─ method GetFirmJurisdictions [L42]

