[web] GET /api/imanage/customers/{customerId:int}/libraries/{library}/customs/{clientFieldCustomName}/clients  (DataGet.Api.Controllers.Integrations.IManageController.GetClients)  [L186–L195] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetIManageClientsQuery [L194]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.iManage.Api.Queries.GetIManageClientsQueryHandler.Handle [L24–L42]
      └─ uses_service IManageService
        └─ method GetClients [L35]

