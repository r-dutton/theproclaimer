[web] GET /api/accounting/sourcedata/sources/{id}  (Cirrus.Api.Controllers.Accounting.SourceData.SourcesController.Get)  [L55–L64] [auth=user]
  └─ maps_to UserUltraLightDto [L61]
    └─ automapper.registration DataverseMappingProfile (User->UserUltraLightDto) [L85]
    └─ automapper.registration WorkpapersMappingProfile (User->UserUltraLightDto) [L96]
    └─ automapper.registration CirrusMappingProfile (User->UserUltraLightDto) [L103]
  └─ maps_to SourceDto [L58]
    └─ automapper.registration ExternalApiMappingProfile (Source->SourceDto) [L95]
    └─ automapper.registration CirrusMappingProfile (Source->SourceDto) [L216]
    └─ automapper.registration WorkpapersMappingProfile (Source->SourceDto) [L598]
  └─ calls UserRepository.ReadQuery [L61]
  └─ calls SourceRepository.ReadQuery [L58]
  └─ queries User [L61]
  └─ queries Source [L58]
  └─ uses_service ApiService (SingleInstance)
    └─ method GetFeatures [L62]
  └─ uses_service IControlledRepository<Source>
    └─ method ReadQuery [L58]
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L61]

