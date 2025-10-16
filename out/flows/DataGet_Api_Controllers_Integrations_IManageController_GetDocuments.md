[web] GET /api/imanage/customers/{customerId:int}/libraries/{library}/customs/{clientFieldCustomName}/documents  (DataGet.Api.Controllers.Integrations.IManageController.GetDocuments)  [L237–L246] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetIManageDocumentsQuery [L245]
    └─ handled_by DataGet.Integrations.iManage.Api.Queries.GetIManageDocumentsQueryHandler.Handle [L26–L60]
      └─ uses_service IntegrationConfigService
        └─ method GetApiIntegrationConfig [L53]
          └─ implementation DataGet.Connections.App.Services.IntegrationConfigService.GetApiIntegrationConfig [L8-L37]
      └─ uses_service IManageService
        └─ method GetDocuments [L41]
          └─ implementation DataGet.Integrations.iManage.Api.Services.IManageService.GetDocuments [L12-L223]

