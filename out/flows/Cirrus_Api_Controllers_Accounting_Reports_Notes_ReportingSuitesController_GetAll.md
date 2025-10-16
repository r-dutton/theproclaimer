[web] GET /api/accounting/reports/notes/reporting-suites/admin  (Cirrus.Api.Controllers.Accounting.Reports.Notes.ReportingSuitesController.GetAll)  [L41–L55] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ maps_to ReportingSuiteLightDto [L45]
    └─ automapper.registration CirrusMappingProfile (ReportingSuite->ReportingSuiteLightDto) [L760]
  └─ calls ReportingSuiteRepository.ReadQuery [L45]
  └─ query ReportingSuite [L45]
    └─ reads_from PolicySuites
  └─ uses_service IControlledRepository<ReportingSuite>
    └─ method ReadQuery [L45]
      └─ ... (no implementation details available)
  └─ uses_service IJurisdictionService (AddScoped)
    └─ method GetFirmJurisdictions [L44]
      └─ implementation IJurisdictionService.GetFirmJurisdictions [L17-L17]
      └─ ... (no implementation details available)

