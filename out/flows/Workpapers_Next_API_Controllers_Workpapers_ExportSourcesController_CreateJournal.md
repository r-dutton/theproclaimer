[web] POST /api/sources/export/{id:guid}/journal  (Workpapers.Next.API.Controllers.Workpapers.ExportSourcesController.CreateJournal)  [L88–L93] status=201 [auth=AuthorizationPolicies.User]
  └─ sends_request PostExportSourceJournalCommand [L91]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.SourceData.ExportSources.PostExportSourceJournalCommandHandler.Handle [L32–L82]
      └─ uses_client DataGetClient [L47]
        └─ calls PostJournal (POST /api/accounting-file/{fileId}/journal?apiType={*}, method=CreateJournalAsync, target=DataGet.Api, query=apiType={*}) [L127]
          └─ target_service DataGet.Api
            └─ [web] POST /api/accounting-file/{fileId}/journal  (DataGet.Api.Controllers.Connections.AccountingFileController.PostJournal)  [L79–L87] status=201 [auth=Authentication.MachineToMachinePolicy]
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
      └─ uses_service DataGetClient
        └─ method CreateJournalAsync [L47]
          └─ implementation Workpapers.Next.DataGet.Client.DataGetClient.CreateJournalAsync [L32-L506]
            └─ calls PostJournal [L127]
      └─ uses_service IControlledRepository<ExportSource>
        └─ method ReadQuery [L45]
          └─ ... (no implementation details available)

