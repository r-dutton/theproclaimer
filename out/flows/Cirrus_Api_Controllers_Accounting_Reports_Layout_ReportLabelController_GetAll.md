[web] GET /api/accounting/reports/labels/  (Cirrus.Api.Controllers.Accounting.Reports.Layout.ReportLabelController.GetAll)  [L42–L49] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to ReportLabelWithAccountDto [L45]
    └─ automapper.registration CirrusMappingProfile (ReportLabel->ReportLabelWithAccountDto) [L650]
  └─ calls ReportLabelRepository.ReadQuery [L45]
  └─ query ReportLabel [L45]
    └─ reads_from ReportLabels
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ ReportLabel writes=0 reads=1
    └─ mappings 1
      └─ ReportLabelWithAccountDto

