[web] GET /api/accounting/reports/notes/reporting-suites/  (Cirrus.Api.Controllers.Accounting.Reports.Notes.ReportingSuitesController.GetAllUltraLight)  [L57–L75] [auth=Authentication.UserPolicy]
  └─ maps_to ReportingSuiteUltraLightDto [L64]
    └─ automapper.registration CirrusMappingProfile (ReportingSuite->ReportingSuiteUltraLightDto) [L766]
  └─ calls ReportingSuiteRepository.ReadQuery [L64]
  └─ queries ReportingSuite [L64]
    └─ reads_from PolicySuites
  └─ uses_service IControlledRepository<ReportingSuite>
    └─ method ReadQuery [L64]
  └─ uses_service IJurisdictionService (AddScoped)
    └─ method GetUserJurisdictions [L62]

