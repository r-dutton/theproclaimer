[web] GET /api/accounting/reports/styles/{id}  (Cirrus.Api.Controllers.Accounting.Reports.Styles.ReportStylesController.Get)  [L41–L53] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ maps_to ReportStyleDto [L44]
    └─ automapper.registration CirrusMappingProfile (ReportStyle->ReportStyleDto) [L580]
  └─ calls ReportStyleRepository.ReadQuery [L44]
  └─ query ReportStyle [L44]
    └─ reads_from ReportStyles
  └─ sends_request GetCustomCssQuery -> GetCustomCssQueryHandler [L49]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.ReportStyles.GetCustomCssQueryHandler.Handle [L30–L86]
      └─ uses_service IStorageService (InstancePerLifetimeScope)
        └─ method GetFileBytes [L53]
          └─ implementation DataGet.Data.BlobStorage.StorageService.GetFileBytes [L11-L116]
            └─ uses_service RequestContextProvider
              └─ method GetContainer [L89]
                └─ resolves_request DataGet.ApplicationService.Services.RequestContextProvider.GetContainer
      └─ uses_service IControlledRepository<ReportStyle> (Scoped (inferred))
        └─ method ReadQuery [L43]
          └─ implementation Cirrus.Data.Repository.Accounting.Report.ReportStyleRepository.ReadQuery
      └─ uses_storage IStorageService.GetFileBytes [L53]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ ReportStyle writes=0 reads=1
    └─ requests 1
      └─ GetCustomCssQuery
    └─ handlers 1
      └─ GetCustomCssQueryHandler
    └─ mappings 1
      └─ ReportStyleDto

