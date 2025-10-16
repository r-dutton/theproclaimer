[web] GET /api/accounting/sourcedata/sources/forfile/{fileId:Guid}  (Cirrus.Api.Controllers.Accounting.SourceData.SourcesController.GetForFile)  [L80–L103] status=200 [auth=user]
  └─ maps_to SourceLightDto [L97]
    └─ automapper.registration CirrusMappingProfile (Source->SourceLightDto) [L220]
    └─ automapper.registration WorkpapersMappingProfile (Source->SourceLightDto) [L597]
  └─ calls SourceRepository.ReadQuery [L97]
  └─ query Source [L97]
  └─ uses_service ApiService (SingleInstance)
    └─ method GetAllImplementedApiTypeFeatures [L89]
      └─ implementation Cirrus.ApplicationService.Accounting.Services.ApiService.GetAllImplementedApiTypeFeatures [L14-L75]
  └─ uses_service IControlledRepository<Source>
    └─ method ReadQuery [L97]
      └─ ... (no implementation details available)

