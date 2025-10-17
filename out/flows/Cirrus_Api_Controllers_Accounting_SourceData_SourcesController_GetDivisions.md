[web] GET /api/accounting/sourcedata/sources/{id}/divisions  (Cirrus.Api.Controllers.Accounting.SourceData.SourcesController.GetDivisions)  [L70–L74] status=200 [auth=user]
  └─ sends_request GetSourceDivisionsQuery -> GetSourceDivisionsQueryHandler [L73]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Api.GetSourceDivisionsQueryHandler.Handle [L31–L55]
      └─ maps_to SourceLightDto [L48]
        └─ automapper.registration CirrusMappingProfile (Source->SourceLightDto) [L220]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L50]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
      └─ uses_service ApiService (SingleInstance)
        └─ method GetApiMethods [L49]
          └─ implementation Cirrus.ApplicationService.Accounting.Services.ApiService.GetApiMethods [L14-L75]
      └─ uses_service IControlledRepository<Source> (Scoped (inferred))
        └─ method ReadQuery [L48]
          └─ implementation Cirrus.Data.Repository.Accounting.SourceData.SourceRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ GetSourceDivisionsQuery
    └─ handlers 1
      └─ GetSourceDivisionsQueryHandler
    └─ mappings 1
      └─ SourceLightDto

