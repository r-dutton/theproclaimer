[web] GET /api/accounting/reports/masters/{id}  (Cirrus.Api.Controllers.Accounting.Reports.ReportMastersController.Get)  [L62–L66] status=200 [auth=user,admin]
  └─ sends_request GetReportMasterQuery -> GetReportMasterQueryHandler [L65]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.GetReportMasterQueryHandler.Handle [L28–L73]
      └─ maps_to ReportMasterDepreciationPageDetailDto [L54]
      └─ maps_to ReportMasterFinancialPageDetailDto [L46]
      └─ maps_to ReportMasterDto [L42]
        └─ automapper.registration CirrusMappingProfile (ReportMaster->ReportMasterDto) [L570]
      └─ uses_service IControlledRepository<ReportMaster> (Scoped (inferred))
        └─ method ReadQuery [L42]
          └─ implementation Cirrus.Data.Repository.Accounting.Report.ReportMasterRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ GetReportMasterQuery
    └─ handlers 1
      └─ GetReportMasterQueryHandler
    └─ mappings 3
      └─ ReportMasterDepreciationPageDetailDto
      └─ ReportMasterDto
      └─ ReportMasterFinancialPageDetailDto

