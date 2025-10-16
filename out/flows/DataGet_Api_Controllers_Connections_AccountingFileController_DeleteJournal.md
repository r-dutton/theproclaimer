[web] DELETE /api/accounting-file/{fileId}/journal/{sourceJournalId}  (DataGet.Api.Controllers.Connections.AccountingFileController.DeleteJournal)  [L99–L107] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request DeleteJournalCommand [L103]
    └─ handled_by Cirrus.Connections.DataGet.Commands.DeleteJournalCommandHandler.Handle [L23–L56]
      └─ uses_client IDatagetClient [L52]
      └─ uses_service IControlledRepository<Dataset>
        └─ method ReadQuery [L39]
          └─ ... (no implementation details available)
      └─ uses_service IDatagetClient (InstancePerLifetimeScope)
        └─ method DeleteJournalAsync [L52]
          └─ ... (no implementation details available)
      └─ uses_service IDatagetFileIdService (InstancePerLifetimeScope)
        └─ method GetFileIdFromSource [L44]
          └─ implementation Cirrus.Connections.DataGet.Services.DatagetFileIdService.GetFileIdFromSource [L14-L37]

