[web] GET /api/accounting/files/{id}  (Cirrus.Api.Controllers.Accounting.FilesController.Get)  [L90–L94] [auth=user]
  └─ sends_request GetFileQuery [L93]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.GetFileQueryHandler.Handle [L25–L49]
      └─ maps_to FileDto [L46]
        └─ automapper.registration CirrusMappingProfile (File->FileDto) [L199]
        └─ automapper.registration ExternalApiMappingProfile (File->FileDto) [L39]
      └─ uses_service IControlledRepository<File>
        └─ method ReadQuery [L44]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L40]

