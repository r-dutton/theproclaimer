[web] GET /api/accounting/reports/labels/  (Cirrus.Api.Controllers.Accounting.Reports.Layout.ReportLabelController.GetAll)  [L42–L49] [auth=Authentication.UserPolicy]
  └─ maps_to ReportLabelWithAccountDto [L45]
    └─ automapper.registration CirrusMappingProfile (ReportLabel->ReportLabelWithAccountDto) [L650]
  └─ calls ReportLabelRepository.ReadQuery [L45]
  └─ queries ReportLabel [L45]
    └─ reads_from ReportLabels
  └─ uses_service IControlledRepository<ReportLabel>
    └─ method ReadQuery [L45]

