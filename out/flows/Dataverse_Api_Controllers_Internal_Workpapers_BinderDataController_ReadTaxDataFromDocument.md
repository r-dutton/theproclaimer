[web] GET /api/internal/binder-data/tax  (Dataverse.Api.Controllers.Internal.Workpapers.BinderDataController.ReadTaxDataFromDocument)  [L27–L31] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request GetDocumentDataQuery -> GetDocumentDataQueryHandler [L30]
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.GetDocumentDataQueryHandler.Handle [L28–L63]
      └─ uses_service DocumentServiceFactory
        └─ method GetDocumentWithService [L54]
          └─ implementation DocumentServiceFactory.GetDocumentWithService
      └─ uses_service IControlledRepository<DocumentVersion> (Scoped (inferred))
        └─ method WriteQuery [L43]
          └─ implementation Dataverse.Data.Repository.Documents.DocumentVersionRepository.WriteQuery
  └─ impact_summary
    └─ requests 1
      └─ GetDocumentDataQuery
    └─ handlers 1
      └─ GetDocumentDataQueryHandler

