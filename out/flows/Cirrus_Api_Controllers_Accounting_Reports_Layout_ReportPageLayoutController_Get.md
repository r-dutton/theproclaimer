[web] GET /api/accounting/reports/page-layouts/{id}  (Cirrus.Api.Controllers.Accounting.Reports.Layout.ReportPageLayoutController.Get)  [L45–L49] [auth=Authentication.UserPolicy]
  └─ sends_request GetReportPageLayoutQuery [L48]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.GetReportPageLayoutsQueryHandler.Handle [L47–L197]
      └─ maps_to ReportPageLayoutDto [L124]
      └─ maps_to ReportPageLayoutDto [L95]
        └─ automapper.registration CirrusMappingProfile (ReportPageLayout->ReportPageLayoutDto) [L644]
      └─ maps_to ReportPageLayoutDto [L101]
      └─ uses_service IControlledRepository<ReportPageLayout>
        └─ method ReadQuery [L95]
      └─ uses_service IMapper
        └─ method Map [L101]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L70]

