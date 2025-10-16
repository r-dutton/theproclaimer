[web] GET /api/imanage/customers/{customerId:int}/libraries/{library}/documents/{documentId}/versions  (DataGet.Api.Controllers.Integrations.IManageController.GetDocumentVersions)  [L280–L288] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetIManageDocumentVersionsQuery [L287]
    └─ handled_by DataGet.Integrations.iManage.Api.Queries.GetIManageDocumentVersionsQueryHandler.Handle [L20–L32]
      └─ uses_service IManageService
        └─ method GetVersions [L29]
          └─ implementation DataGet.Integrations.iManage.Api.Services.IManageService.GetVersions [L12-L223]

