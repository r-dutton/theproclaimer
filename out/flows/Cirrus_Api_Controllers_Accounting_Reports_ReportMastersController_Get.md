[web] GET /api/accounting/reports/masters/{id}  (Cirrus.Api.Controllers.Accounting.Reports.ReportMastersController.Get)  [L62–L66] status=200 [auth=user,admin]
  └─ sends_request GetReportMasterQuery [L65]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.GetReportMasterQueryHandler.Handle [L28–L73]
      └─ maps_to ReportMasterDto [L42]
        └─ automapper.registration CirrusMappingProfile (ReportMaster->ReportMasterDto) [L570]
      └─ maps_to ReportMasterDepreciationPageDetailDto [L54]
      └─ maps_to ReportMasterFinancialPageDetailDto [L46]
      └─ uses_service IControlledRepository<ReportMaster>
        └─ method ReadQuery [L42]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L51]
          └─ ... (no implementation details available)

