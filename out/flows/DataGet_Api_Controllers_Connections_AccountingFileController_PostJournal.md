[web] POST /api/accounting-file/{fileId}/journal  (DataGet.Api.Controllers.Connections.AccountingFileController.PostJournal)  [L79–L87] status=201 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request PostJournalCommand [L83]
    └─ handled_by Cirrus.Connections.DataGet.Commands.PostJournalCommandHandler.Handle [L30–L135]
      └─ uses_client IDatagetClient [L82]
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method WriteQuery [L56]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<SourceDivision>
        └─ method ReadQuery [L76]
          └─ ... (no implementation details available)
      └─ uses_service IDatagetClient (InstancePerLifetimeScope)
        └─ method CreateJournalAsync [L82]
          └─ ... (no implementation details available)
      └─ uses_service IDatagetFileIdService (InstancePerLifetimeScope)
        └─ method GetFileIdFromSource [L52]
          └─ implementation Cirrus.Connections.DataGet.Services.DatagetFileIdService.GetFileIdFromSource [L14-L37]
  └─ returns JournalPostResponse [L83]

