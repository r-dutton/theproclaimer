[web] GET /api/accounting/reports/images/{id}  (Cirrus.Api.Controllers.Accounting.Reports.Images.ImagesController.Get)  [L40–L44] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request GetImageQuery [L43]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.Images.GetImageQueryHandler.Handle [L23–L57]
      └─ maps_to ImageDto [L43]
        └─ automapper.registration CirrusMappingProfile (Image->ImageDto) [L589]
      └─ uses_service IControlledRepository<Image>
        └─ method ReadQuery [L43]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L53]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)
      └─ uses_service IStorageService (InstancePerLifetimeScope)
        └─ method CreateDownloadUrl [L50]
          └─ implementation IStorageService.CreateDownloadUrl [L9-L9]
          └─ ... (no implementation details available)
      └─ uses_storage IStorageService.CreateDownloadUrl [L50]

