[web] DELETE /api/accounting/reports/styles/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.Styles.ReportStylesController.Delete)  [L100–L105] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls ReportStyleRepository.Remove [L104]
  └─ calls ReportStyleRepository.WriteQuery [L103]
  └─ delete ReportStyle [L104]
    └─ reads_from ReportStyles
  └─ write ReportStyle [L103]
    └─ reads_from ReportStyles
  └─ uses_service IControlledRepository<ReportStyle>
    └─ method WriteQuery [L103]
      └─ ... (no implementation details available)

