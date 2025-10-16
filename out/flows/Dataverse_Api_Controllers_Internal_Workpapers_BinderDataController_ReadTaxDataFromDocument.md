[web] GET /api/internal/binder-data/tax  (Dataverse.Api.Controllers.Internal.Workpapers.BinderDataController.ReadTaxDataFromDocument)  [L27–L31] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request GetDocumentDataQuery [L30]
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.GetDocumentDataQueryHandler.Handle [L28–L63]
      └─ uses_service DocumentServiceFactory
        └─ method GetDocumentWithService [L54]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<DocumentVersion>
        └─ method WriteQuery [L43]
          └─ ... (no implementation details available)

