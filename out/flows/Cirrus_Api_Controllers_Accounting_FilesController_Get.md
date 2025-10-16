[web] GET /api/accounting/files/{id}  (Cirrus.Api.Controllers.Accounting.FilesController.Get)  [L90–L94] status=200 [auth=user]
  └─ sends_request GetFileQuery -> GetFileQueryHandler [L93]
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.GetFileQueryHandler.Handle [L25–L49]
      └─ maps_to FileDto [L46]
        └─ automapper.registration ExternalApiMappingProfile (File->FileDto) [L39]
        └─ automapper.registration CirrusMappingProfile (File->FileDto) [L199]
      └─ uses_service IControlledRepository<File> (Scoped (inferred))
        └─ method ReadQuery [L44]
          └─ implementation Cirrus.Data.Repository.Accounting.FileRepository.ReadQuery
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L40]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
  └─ impact_summary
    └─ requests 1
      └─ GetFileQuery
    └─ handlers 1
      └─ GetFileQueryHandler
    └─ mappings 1
      └─ FileDto

