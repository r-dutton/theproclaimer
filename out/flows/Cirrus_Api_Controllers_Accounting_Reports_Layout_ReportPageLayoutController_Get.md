[web] GET /api/accounting/reports/page-layouts/{id}  (Cirrus.Api.Controllers.Accounting.Reports.Layout.ReportPageLayoutController.Get)  [L45–L49] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request GetReportPageLayoutQuery [L48]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.GetReportPageLayoutsQueryHandler.Handle [L47–L197]
      └─ maps_to ReportPageLayoutDto [L124]
      └─ maps_to ReportPageLayoutDto [L95]
        └─ automapper.registration CirrusMappingProfile (ReportPageLayout->ReportPageLayoutDto) [L644]
      └─ maps_to ReportPageLayoutDto [L101]
      └─ uses_service IControlledRepository<ReportPageLayout>
        └─ method ReadQuery [L95]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method Map [L101]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L70]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)

