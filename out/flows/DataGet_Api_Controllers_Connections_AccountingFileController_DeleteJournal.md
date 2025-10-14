[web] DELETE /api/accounting-file/{fileId}/journal/{sourceJournalId}  (DataGet.Api.Controllers.Connections.AccountingFileController.DeleteJournal)  [L99–L107] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request DeleteJournalCommand [L103]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.Connections.DataGet.Commands.DeleteJournalCommandHandler.Handle [L23–L56]
      └─ uses_client IDatagetClient [L52]
      └─ uses_service IControlledRepository<Dataset>
        └─ method ReadQuery [L39]
      └─ uses_service IDatagetClient (InstancePerLifetimeScope)
        └─ method DeleteJournalAsync [L52]
      └─ uses_service IDatagetFileIdService (InstancePerLifetimeScope)
        └─ method GetFileIdFromSource [L44]

