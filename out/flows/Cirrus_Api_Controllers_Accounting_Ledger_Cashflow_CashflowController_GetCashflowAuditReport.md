[web] GET /api/accounting/ledger/cashflow/audit-report  (Cirrus.Api.Controllers.Accounting.Ledger.Cashflow.CashflowController.GetCashflowAuditReport)  [L29–L40] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request GetCashflowAuditQuery [L38]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetCashflowAuditQueryHandler.Handle [L87–L305]
      └─ uses_service GetAccountsQuery
        └─ method Execute [L247]
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
      └─ uses_service GetAccountTypesQuery
        └─ method Execute [L244]
          └─ ... (no implementation details available)
      └─ uses_service GetCashflowCategoriesQuery
        └─ method Execute [L225]
          └─ ... (no implementation details available)
      └─ uses_service GetCashflowJournalLinesQuery
        └─ method Execute [L236]
          └─ ... (no implementation details available)
      └─ uses_service GetCashflowLinesQuery
        └─ method Execute [L226]
          └─ ... (no implementation details available)
      └─ uses_service GetCashflowReconciliationLinesQuery
        └─ method Execute [L233]
          └─ ... (no implementation details available)
      └─ uses_service GetTrialBalanceForDatesQuery
        └─ method Execute [L250]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Dataset>
        └─ method ReadQuery [L213]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L122]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)

