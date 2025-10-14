[web] POST /api/accounting-file/{fileId}/journal  (DataGet.Api.Controllers.Connections.AccountingFileController.PostJournal)  [L79–L87] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request PostJournalCommand [L83]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.Connections.DataGet.Commands.PostJournalCommandHandler.Handle [L30–L135]
      └─ uses_client IDatagetClient [L82]
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method WriteQuery [L56]
      └─ uses_service IControlledRepository<SourceDivision>
        └─ method ReadQuery [L76]
      └─ uses_service IDatagetClient (InstancePerLifetimeScope)
        └─ method CreateJournalAsync [L82]
      └─ uses_service IDatagetFileIdService (InstancePerLifetimeScope)
        └─ method GetFileIdFromSource [L52]
  └─ returns JournalPostResponse [L83]

