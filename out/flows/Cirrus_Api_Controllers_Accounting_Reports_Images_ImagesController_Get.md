[web] GET /api/accounting/reports/images/{id}  (Cirrus.Api.Controllers.Accounting.Reports.Images.ImagesController.Get)  [L40–L44] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request GetImageQuery -> GetImageQueryHandler [L43]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.Images.GetImageQueryHandler.Handle [L23–L57]
      └─ maps_to ImageDto [L43]
        └─ automapper.registration CirrusMappingProfile (Image->ImageDto) [L589]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L53]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
      └─ uses_service IStorageService (InstancePerLifetimeScope)
        └─ method CreateDownloadUrl [L50]
          └─ implementation DataGet.Data.BlobStorage.StorageService.CreateDownloadUrl [L11-L116]
            └─ uses_service RequestContextProvider
              └─ method GetContainer [L89]
                └─ resolves_request DataGet.ApplicationService.Services.RequestContextProvider.GetContainer
      └─ uses_service IControlledRepository<Image> (Scoped (inferred))
        └─ method ReadQuery [L43]
          └─ implementation Cirrus.Data.Repository.Accounting.Report.ImageRepository.ReadQuery
      └─ uses_storage IStorageService.CreateDownloadUrl [L50]
  └─ impact_summary
    └─ requests 1
      └─ GetImageQuery
    └─ handlers 1
      └─ GetImageQueryHandler
    └─ mappings 1
      └─ ImageDto

