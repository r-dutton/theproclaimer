[web] GET /api/accounting/sourcedata/sources/forfile/{fileId:Guid}  (Cirrus.Api.Controllers.Accounting.SourceData.SourcesController.GetForFile)  [L80–L103] [auth=user]
  └─ maps_to SourceLightDto [L97]
    └─ automapper.registration CirrusMappingProfile (Source->SourceLightDto) [L220]
    └─ automapper.registration WorkpapersMappingProfile (Source->SourceLightDto) [L597]
  └─ calls SourceRepository.ReadQuery [L97]
  └─ queries Source [L97]
  └─ uses_service ApiService (SingleInstance)
    └─ method GetAllImplementedApiTypeFeatures [L89]
  └─ uses_service IControlledRepository<Source>
    └─ method ReadQuery [L97]

