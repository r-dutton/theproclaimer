[web] GET /api/imanage/customers/{customerId:int}/libraries/{library}/customs/{clientFieldCustomName}/clients  (DataGet.Api.Controllers.Integrations.IManageController.GetClients)  [L186–L195] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetIManageClientsQuery [L194]
    └─ handled_by DataGet.Integrations.iManage.Api.Queries.GetIManageClientsQueryHandler.Handle [L24–L42]
      └─ uses_service IManageService
        └─ method GetClients [L35]
          └─ implementation DataGet.Integrations.iManage.Api.Services.IManageService.GetClients [L12-L223]

