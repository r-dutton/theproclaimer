[web] GET /api/internal/binder-data/tax  (Dataverse.Api.Controllers.Internal.Workpapers.BinderDataController.ReadTaxDataFromDocument)  [L27–L31] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request GetDocumentDataQuery [L30]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.GetDocumentDataQueryHandler.Handle [L28–L62]
      └─ uses_service DocumentServiceFactory
        └─ method GetDocumentWithService [L54]
      └─ uses_service IControlledRepository<DocumentVersion>
        └─ method WriteQuery [L43]

