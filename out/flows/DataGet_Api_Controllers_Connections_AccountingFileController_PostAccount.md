[web] POST /api/accounting-file/{fileId}/accounts  (DataGet.Api.Controllers.Connections.AccountingFileController.PostAccount)  [L89–L97] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request PostAccountCommand [L93]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Connections.App.Commands.PostAccountCommandHandler.Handle [L22–L43]
      └─ uses_service IExternalApiServiceFactory (InstancePerLifetimeScope)
        └─ method GetAccountingApiFromApiType [L32]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L41]

