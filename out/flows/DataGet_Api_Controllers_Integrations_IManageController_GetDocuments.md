[web] GET /api/imanage/customers/{customerId:int}/libraries/{library}/customs/{clientFieldCustomName}/documents  (DataGet.Api.Controllers.Integrations.IManageController.GetDocuments)  [L237–L246] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetIManageDocumentsQuery [L245]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.iManage.Api.Queries.GetIManageDocumentsQueryHandler.Handle [L26–L60]
      └─ uses_service IManageService
        └─ method GetDocuments [L41]
      └─ uses_service IntegrationConfigService
        └─ method GetApiIntegrationConfig [L53]

