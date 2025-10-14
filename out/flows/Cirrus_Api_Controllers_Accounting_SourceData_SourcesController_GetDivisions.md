[web] GET /api/accounting/sourcedata/sources/{id}/divisions  (Cirrus.Api.Controllers.Accounting.SourceData.SourcesController.GetDivisions)  [L70–L74] [auth=user]
  └─ sends_request GetSourceDivisionsQuery [L73]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Api.GetSourceDivisionsQueryHandler.Handle [L31–L55]
      └─ maps_to SourceLightDto [L48]
        └─ automapper.registration CirrusMappingProfile (Source->SourceLightDto) [L220]
        └─ automapper.registration WorkpapersMappingProfile (Source->SourceLightDto) [L597]
      └─ uses_service ApiService (SingleInstance)
        └─ method GetApiMethods [L49]
      └─ uses_service IControlledRepository<Source>
        └─ method ReadQuery [L48]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L50]

