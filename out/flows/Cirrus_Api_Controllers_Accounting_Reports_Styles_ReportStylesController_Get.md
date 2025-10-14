[web] GET /api/accounting/reports/styles/{id}  (Cirrus.Api.Controllers.Accounting.Reports.Styles.ReportStylesController.Get)  [L41–L53] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ maps_to ReportStyleDto [L44]
    └─ automapper.registration CirrusMappingProfile (ReportStyle->ReportStyleDto) [L580]
  └─ calls ReportStyleRepository.ReadQuery [L44]
  └─ queries ReportStyle [L44]
    └─ reads_from ReportStyles
  └─ uses_service IControlledRepository<ReportStyle>
    └─ method ReadQuery [L44]
  └─ sends_request GetCustomCssQuery [L49]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.ReportStyles.GetCustomCssQueryHandler.Handle [L30–L86]
      └─ uses_service IControlledRepository<ReportStyle>
        └─ method ReadQuery [L43]
      └─ uses_service IStorageService (InstancePerLifetimeScope)
        └─ method GetFileBytes [L53]

