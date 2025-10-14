[web] GET /api/accounting/reports/images/{id}  (Cirrus.Api.Controllers.Accounting.Reports.Images.ImagesController.Get)  [L40–L44] [auth=Authentication.UserPolicy]
  └─ sends_request GetImageQuery [L43]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.Images.GetImageQueryHandler.Handle [L23–L57]
      └─ maps_to ImageDto [L43]
        └─ automapper.registration CirrusMappingProfile (Image->ImageDto) [L589]
      └─ uses_service IControlledRepository<Image>
        └─ method ReadQuery [L43]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L53]
      └─ uses_service IStorageService (InstancePerLifetimeScope)
        └─ method CreateDownloadUrl [L50]

