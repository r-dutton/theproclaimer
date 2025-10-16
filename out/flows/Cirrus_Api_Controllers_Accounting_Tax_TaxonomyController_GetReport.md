[web] GET /api/accounting/tax/taxonomy/report  (Cirrus.Api.Controllers.Accounting.Tax.TaxonomyController.GetReport)  [L47–L51] status=200 [auth=user]
  └─ sends_request GetTaxonomyReport [L50]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Tax.GetTaxonomyReportQueryHandler.Handle [L43–L148]
      └─ maps_to AccountLightListDto [L135]
      └─ maps_to AccountTypeLightWithTaxonomyDto [L112]
      └─ maps_to TaxonomyDto [L106]
      └─ uses_service GetAccountsQuery
        └─ method Execute [L79]
          └─ implementation Cirrus.Connections.DataGet.Queries.GetAccountsQuery.Execute [L9-L23]
            └─ handled_by Cirrus.Connections.DataGet.Queries.GetAccountsQueryHandler.Handle [L25–L59]
              └─ uses_client DataGetClient [L40]
                └─ calls GetAccounts (GET /api/accounting-file/{fileId}/accounts?apiType={*}&password={*}&username={*}, method=GetAccountsAsync, target=DataGet.Api, query=apiType={*}&password={*}&username={*}) [L65]
                  └─ target_service DataGet.Api
                    └─ [web] GET /api/accounting-file/{fileId}/accounts  (DataGet.Api.Controllers.Connections.AccountingFileController.GetAccounts)  [L44–L52] status=200 [auth=Authentication.MachineToMachinePolicy]
                      └─ sends_request GetAccountsQuery [L48]
                        └─ handled_by Cirrus.Connections.DataGet.Queries.GetAccountsQueryHandler.Handle [L25–L59]
              └─ uses_service DatagetFileIdService
                └─ method GetFileIdFromSource [L38]
                  └─ implementation Cirrus.Connections.DataGet.Services.DatagetFileIdService.GetFileIdFromSource [L14-L37]
              └─ uses_service DataGetClient
                └─ method GetAccountsAsync [L40]
                  └─ implementation Cirrus.Connections.DataGet.Client.DataGetClient.GetAccountsAsync [L15-L302]
                    └─ calls GetAccounts [L106]
      └─ uses_service GetTrialBalanceForDatesQuery
        └─ method Execute [L78]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Dataset>
        └─ method ReadQuery [L66]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L109]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L71]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)

