[web] GET /api/accounting/reports/notes/policy-layouts/  (Cirrus.Api.Controllers.Accounting.Reports.Notes.PolicyLayoutsController.GetAll)  [L39–L49] status=200 [auth=Authentication.AdminPolicy]
  └─ maps_to PolicyLayoutLightDto [L44]
    └─ automapper.registration CirrusMappingProfile (PolicyLayout->PolicyLayoutLightDto) [L782]
  └─ calls PolicyLayoutRepository.ReadQuery [L44]
  └─ query PolicyLayout [L44]
    └─ reads_from PolicyLayouts
  └─ uses_service IControlledRepository<PolicyLayout>
    └─ method ReadQuery [L44]
      └─ ... (no implementation details available)
  └─ uses_service IJurisdictionService (AddScoped)
    └─ method GetFirmJurisdictions [L42]
      └─ implementation IJurisdictionService.GetFirmJurisdictions [L17-L17]
      └─ ... (no implementation details available)

