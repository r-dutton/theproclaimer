[web] GET /api/imanage/customers/{customerId:int}/libraries/{library}/documents/{documentId}/versions  (DataGet.Api.Controllers.Integrations.IManageController.GetDocumentVersions)  [L280–L288] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetIManageDocumentVersionsQuery [L287]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.iManage.Api.Queries.GetIManageDocumentVersionsQueryHandler.Handle [L20–L32]
      └─ uses_service IManageService
        └─ method GetVersions [L29]

