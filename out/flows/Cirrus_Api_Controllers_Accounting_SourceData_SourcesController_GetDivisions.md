[web] GET /api/accounting/sourcedata/sources/{id}/divisions  (Cirrus.Api.Controllers.Accounting.SourceData.SourcesController.GetDivisions)  [L70–L74] status=200 [auth=user]
  └─ sends_request GetSourceDivisionsQuery [L73]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Api.GetSourceDivisionsQueryHandler.Handle [L31–L55]
      └─ maps_to SourceLightDto [L48]
        └─ automapper.registration CirrusMappingProfile (Source->SourceLightDto) [L220]
        └─ automapper.registration WorkpapersMappingProfile (Source->SourceLightDto) [L597]
      └─ uses_service ApiService (SingleInstance)
        └─ method GetApiMethods [L49]
          └─ implementation Cirrus.ApplicationService.Accounting.Services.ApiService.GetApiMethods [L14-L75]
      └─ uses_service IControlledRepository<Source>
        └─ method ReadQuery [L48]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L50]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)

