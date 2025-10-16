[web] GET /api/imanage/customers/{customerId:int}/libraries/{library}/documents/{documentId}/profile  (DataGet.Api.Controllers.Integrations.IManageController.GetDocumentProfile)  [L270–L278] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetIManageDocumentProfileQuery [L277]
    └─ handled_by DataGet.Integrations.iManage.Api.Queries.GetIManageDocumentProfileQueryHandler.Handle [L24–L40]
      └─ uses_service IManageService
        └─ method GetDocumentProfile [L35]
          └─ implementation DataGet.Integrations.iManage.Api.Services.IManageService.GetDocumentProfile [L12-L223]

