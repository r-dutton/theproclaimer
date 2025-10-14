[web] GET /api/accounting/reports/page-layouts/  (Cirrus.Api.Controllers.Accounting.Reports.Layout.ReportPageLayoutController.GetAll)  [L55–L62] [auth=Authentication.UserPolicy]
  └─ maps_to ReportPageLayoutLightDto [L58]
    └─ automapper.registration CirrusMappingProfile (ReportPageLayout->ReportPageLayoutLightDto) [L646]
  └─ calls ReportPageLayoutRepository.ReadQuery [L58]
  └─ queries ReportPageLayout [L58]
    └─ reads_from ReportPageLayouts
  └─ uses_service IControlledRepository<ReportPageLayout>
    └─ method ReadQuery [L58]

