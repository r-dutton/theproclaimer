[web] GET /api/imanage/customers/{customerId:int}/libraries/{library}/documents/{documentId}/download  (DataGet.Api.Controllers.Integrations.IManageController.DownloadDocument)  [L248–L256] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetIManageDocumentDownloadContentQuery [L255]
    └─ handled_by DataGet.Integrations.iManage.Api.Queries.GetIManageDocumentDownloadContentQueryHandler.Handle [L24–L41]
      └─ uses_service IManageService
        └─ method GetDocumentDownloadContent [L36]
          └─ implementation DataGet.Integrations.iManage.Api.Services.IManageService.GetDocumentDownloadContent [L12-L223]

