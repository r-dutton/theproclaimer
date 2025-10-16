[web] GET /api/accounting/sourcedata/sources/types  (Cirrus.Api.Controllers.Accounting.SourceData.SourcesController.GetSourceTypes)  [L109–L122] status=200 [auth=user]
  └─ maps_to SourceTypeDto [L112]
    └─ automapper.registration CirrusMappingProfile (SourceType->SourceTypeDto) [L205]
  └─ query SourceType [L112]
  └─ uses_service ApiService (SingleInstance)
    └─ method GetFeatures [L118]
      └─ implementation Cirrus.ApplicationService.Accounting.Services.ApiService.GetFeatures [L14-L75]
  └─ uses_service IControlledRepository<SourceType>
    └─ method ReadQuery [L112]
      └─ ... (no implementation details available)

