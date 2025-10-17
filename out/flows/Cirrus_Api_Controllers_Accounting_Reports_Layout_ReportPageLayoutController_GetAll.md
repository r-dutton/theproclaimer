[web] GET /api/accounting/reports/page-layouts/  (Cirrus.Api.Controllers.Accounting.Reports.Layout.ReportPageLayoutController.GetAll)  [L55–L62] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to ReportPageLayoutLightDto [L58]
    └─ automapper.registration CirrusMappingProfile (ReportPageLayout->ReportPageLayoutLightDto) [L646]
  └─ calls ReportPageLayoutRepository.ReadQuery [L58]
  └─ query ReportPageLayout [L58]
    └─ reads_from ReportPageLayouts
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ ReportPageLayout writes=0 reads=1
    └─ mappings 1
      └─ ReportPageLayoutLightDto

