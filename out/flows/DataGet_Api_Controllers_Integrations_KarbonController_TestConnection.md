[web] GET /api/karbon/test-connection  (DataGet.Api.Controllers.Integrations.KarbonController.TestConnection)  [L53–L62] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request TestConnectionQuery -> TestConnectionQueryHandler [L58]
    └─ handled_by Cirrus.Connections.DataGet.Queries.TestConnectionQueryHandler.Handle [L17–L31]
      └─ uses_client DataGetClient [L28]
        └─ calls TestConnection (GET /api/connection/{apiType}/test-connection?fileId={*}&password={*}&username={*}, method=TestConnectionAsync, target=DataGet.Api, query=fileId={*}&password={*}&username={*}) [L472]
          └─ target_service DataGet.Api
            └─ [web] GET /api/connection/{apiType}/test-connection  (DataGet.Api.Controllers.Connections.ConnectionController.TestConnection)  [L110–L119] status=200 [auth=Authentication.MachineToMachinePolicy]
              └─ sends_request TestConnectionCommand -> TestConnectionCommandHandler [L114]
                └─ handled_by DataGet.Connections.App.Commands.TestConnectionCommandHandler.Handle [L25–L61]
                  └─ uses_service RequestProcessor
                    └─ method ProcessAsync [L47]
                      └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
                      └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
                      └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync
                      └─ +1 additional_requests elided
                  └─ uses_service IExternalApiServiceFactory (InstancePerLifetimeScope)
                    └─ method GetExternalApiFromApiType [L38]
                      └─ ... (no implementation details available)
      └─ uses_service DataGetClient
        └─ method TestConnectionAsync [L28]
          └─ implementation Cirrus.Connections.DataGet.Client.DataGetClient.TestConnectionAsync [L15-L302]
            └─ calls GetAuthorisationUrl [L45]
            └─ calls Disconnect [L55]
            └─ calls DisconnectFile [L65]
            └─ calls GetAccountingFiles [L74]
            └─ calls TestConnection [L84]
            └─ calls GetSourceDivisions [L95]
            └─ calls GetAccounts [L106]
            └─ calls GetMovements [L130]
            └─ calls GetTransactions [L151]
            └─ calls PostJournal [L161]
            └─ calls PostAccount [L171]
            └─ calls DeleteJournal [L182]
            └─ calls GetCreditors [L194]
            └─ calls GetDebtors [L206]
            └─ calls GetWages [L218]
            └─ calls StoreExistingToken [L228]
            └─ calls StoreExistingFileTokens [L238]
            └─ calls RequestLodgementAsync [L244]
  └─ impact_summary
    └─ remote_endpoints 1 (calls=1) scopes=service:1
      └─ GET /api/connection/{apitype}/test-connection -> DataGet.Api via DataGetClient [query] (x1)
    └─ clients 1
      └─ DataGetClient
    └─ requests 2
      └─ TestConnectionCommand
      └─ TestConnectionQuery
    └─ handlers 2
      └─ TestConnectionCommandHandler
      └─ TestConnectionQueryHandler

