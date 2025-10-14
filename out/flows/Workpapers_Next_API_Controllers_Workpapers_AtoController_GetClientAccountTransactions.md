[web] GET /api/ato/client-account-transactions  (Workpapers.Next.API.Controllers.Workpapers.AtoController.GetClientAccountTransactions)  [L50–L60] [auth=AuthorizationPolicies.User]
  └─ sends_request GetClientAccountTransactionsQuery [L56]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.GovLink.GetClientAccountTransactionsQueryHandler.Handle [L36–L77]
      └─ uses_client DataGetClient [L64]
        └─ calls DeleteJournal (DELETE /api/accounting-file/{fileId}/journal/{sourceJournalId}?apiType={*}&password={*}&username={*}, target=DataGet.Api, query=apiType={*}&password={*}&username={*}) [L141]
          └─ target_service DataGet.Api
            └─ [web] DELETE /api/accounting-file/{fileId}/journal/{sourceJournalId}  (DataGet.Api.Controllers.Connections.AccountingFileController.DeleteJournal)  [L99–L107] [auth=Authentication.MachineToMachinePolicy]
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
        └─ calls GetAccounts (GET /api/accounting-file/{fileId}/accounts?apiType={*}&password={*}&username={*}, target=DataGet.Api, query=apiType={*}&password={*}&username={*}) [L65]
          └─ target_service DataGet.Api
            └─ [web] GET /api/accounting-file/{fileId}/accounts  (DataGet.Api.Controllers.Connections.AccountingFileController.GetAccounts)  [L44–L52] [auth=Authentication.MachineToMachinePolicy]
              └─ sends_request GetAccountsQuery [L48]
                └─ generic_pipeline_behaviors 2
                  └─ DatagetTokenSyncBehaviour
                  └─ DatagetTokenSyncBehaviour
                └─ handled_by Cirrus.Connections.DataGet.Queries.GetAccountsQueryHandler.Handle [L25–L59]
                  └─ uses_client DataGetClient [L40]
                    └─ calls DeleteJournal (DELETE /api/accounting-file/{fileId}/journal/{sourceJournalId}?apiType={*}&password={*}&username={*}, target=DataGet.Api, query=apiType={*}&password={*}&username={*}) [L141]
                      └─ target_service DataGet.Api (see previous expansion)
                    └─ calls GetAccounts (GET /api/accounting-file/{fileId}/accounts?apiType={*}&password={*}&username={*}, target=DataGet.Api, query=apiType={*}&password={*}&username={*}) [L65]
                      └─ target_service DataGet.Api (see previous expansion)
                    └─ calls GetCreditors (GET /api/accounting-file/{fileId}/creditors?apiType={*}&endDate={*}&password={*}&sourceAccountSystemId={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&password={*}&sourceAccountSystemId={*}&username={*}) [L156]
                      └─ target_service DataGet.Api
                        └─ [web] GET /api/accounting-file/{fileId}/creditors  (DataGet.Api.Controllers.Connections.AccountingFileController.GetCreditors)  [L109–L117] [auth=Authentication.MachineToMachinePolicy]
                          └─ sends_request GetCreditorsQuery [L113]
                            └─ generic_pipeline_behaviors 2
                              └─ DatagetTokenSyncBehaviour
                              └─ DatagetTokenSyncBehaviour
                            └─ handled_by Cirrus.Connections.DataGet.Queries.GetCreditorsQueryHandler.Handle [L25–L57]
                              └─ uses_client DataGetClient [L53]
                                └─ calls DeleteJournal (DELETE /api/accounting-file/{fileId}/journal/{sourceJournalId}?apiType={*}&password={*}&username={*}, target=DataGet.Api, query=apiType={*}&password={*}&username={*}) [L141]
                                  └─ target_service DataGet.Api (see previous expansion)
                                └─ calls GetAccounts (GET /api/accounting-file/{fileId}/accounts?apiType={*}&password={*}&username={*}, target=DataGet.Api, query=apiType={*}&password={*}&username={*}) [L65]
                                  └─ target_service DataGet.Api (see previous expansion)
                                └─ calls GetCreditors (GET /api/accounting-file/{fileId}/creditors?apiType={*}&endDate={*}&password={*}&sourceAccountSystemId={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&password={*}&sourceAccountSystemId={*}&username={*}) [L156]
                                  └─ target_service DataGet.Api (see previous expansion)
                                └─ calls GetDebtors (GET /api/accounting-file/{fileId}/debtors?apiType={*}&endDate={*}&password={*}&sourceAccountSystemId={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&password={*}&sourceAccountSystemId={*}&username={*}) [L171]
                                  └─ target_service DataGet.Api
                                    └─ [web] GET /api/accounting-file/{fileId}/debtors  (DataGet.Api.Controllers.Connections.AccountingFileController.GetDebtors)  [L119–L127] [auth=Authentication.MachineToMachinePolicy]
                                      └─ sends_request GetDebtorsQuery [L123]
                                        └─ generic_pipeline_behaviors 2
                                          └─ DatagetTokenSyncBehaviour
                                          └─ DatagetTokenSyncBehaviour
                                        └─ handled_by Cirrus.Connections.DataGet.Queries.GetDebtorsQueryHandler.Handle [L25–L56]
                                          └─ uses_client DataGetClient [L52]
                                            └─ calls DeleteJournal (DELETE /api/accounting-file/{fileId}/journal/{sourceJournalId}?apiType={*}&password={*}&username={*}, target=DataGet.Api, query=apiType={*}&password={*}&username={*}) [L141]
                                              └─ target_service DataGet.Api (see previous expansion)
                                            └─ calls GetAccounts (GET /api/accounting-file/{fileId}/accounts?apiType={*}&password={*}&username={*}, target=DataGet.Api, query=apiType={*}&password={*}&username={*}) [L65]
                                              └─ target_service DataGet.Api (see previous expansion)
                                            └─ calls GetCreditors (GET /api/accounting-file/{fileId}/creditors?apiType={*}&endDate={*}&password={*}&sourceAccountSystemId={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&password={*}&sourceAccountSystemId={*}&username={*}) [L156]
                                              └─ target_service DataGet.Api (see previous expansion)
                                            └─ calls GetDebtors (GET /api/accounting-file/{fileId}/debtors?apiType={*}&endDate={*}&password={*}&sourceAccountSystemId={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&password={*}&sourceAccountSystemId={*}&username={*}) [L171]
                                              └─ target_service DataGet.Api (see previous expansion)
                                            └─ calls GetMovements (GET /api/accounting-file/{fileId}/movement-journals?apiType={*}&endDate={*}&importFrequency={*}&importMovementOnly={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&importFrequency={*}&importMovementOnly={*}&password={*}&startDate={*}&username={*}) [L93]
                                              └─ target_service DataGet.Api
                                                └─ [web] GET /api/accounting-file/{fileId}/movement-journals  (DataGet.Api.Controllers.Connections.AccountingFileController.GetMovements)  [L54–L63] [auth=Authentication.MachineToMachinePolicy]
                                                  └─ sends_request GetMovementJournalsQuery [L59]
                                                    └─ generic_pipeline_behaviors 2
                                                      └─ DatagetTokenSyncBehaviour
                                                      └─ DatagetTokenSyncBehaviour
                                                    └─ handled_by Cirrus.Connections.DataGet.Queries.GetMovementJournalsQueryHandler.Handle [L49–L204]
                                                      └─ uses_client DataGetClient [L81]
                                                        └─ calls DeleteJournal (DELETE /api/accounting-file/{fileId}/journal/{sourceJournalId}?apiType={*}&password={*}&username={*}, target=DataGet.Api, query=apiType={*}&password={*}&username={*}) [L141]
                                                          └─ target_service DataGet.Api (see previous expansion)
                                                        └─ calls GetAccounts (GET /api/accounting-file/{fileId}/accounts?apiType={*}&password={*}&username={*}, target=DataGet.Api, query=apiType={*}&password={*}&username={*}) [L65]
                                                          └─ target_service DataGet.Api (see previous expansion)
                                                        └─ calls GetCreditors (GET /api/accounting-file/{fileId}/creditors?apiType={*}&endDate={*}&password={*}&sourceAccountSystemId={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&password={*}&sourceAccountSystemId={*}&username={*}) [L156]
                                                          └─ target_service DataGet.Api (see previous expansion)
                                                        └─ calls GetDebtors (GET /api/accounting-file/{fileId}/debtors?apiType={*}&endDate={*}&password={*}&sourceAccountSystemId={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&password={*}&sourceAccountSystemId={*}&username={*}) [L171]
                                                          └─ target_service DataGet.Api (see previous expansion)
                                                        └─ calls GetMovements (GET /api/accounting-file/{fileId}/movement-journals?apiType={*}&endDate={*}&importFrequency={*}&importMovementOnly={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&importFrequency={*}&importMovementOnly={*}&password={*}&startDate={*}&username={*}) [L93]
                                                          └─ target_service DataGet.Api (see previous expansion)
                                                        └─ calls GetTransactions (GET /api/accounting-file/{fileId}/transactions?apiType={*}&endDate={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&password={*}&startDate={*}&username={*}) [L116]
                                                          └─ target_service DataGet.Api
                                                            └─ [web] GET /api/accounting-file/{fileId}/transactions  (DataGet.Api.Controllers.Connections.AccountingFileController.GetTransactions)  [L65–L77] [auth=Authentication.MachineToMachinePolicy]
                                                              └─ sends_request GetTransactionsQuery [L73]
                                                                └─ generic_pipeline_behaviors 2
                                                                  └─ DatagetTokenSyncBehaviour
                                                                  └─ DatagetTokenSyncBehaviour
                                                                └─ handled_by Cirrus.Connections.DataGet.Queries.GetTransactionsQueryHandler.Handle [L34–L73]
                                                                  └─ uses_client DataGetClient [L50]
                                                                    └─ calls DeleteJournal (DELETE /api/accounting-file/{fileId}/journal/{sourceJournalId}?apiType={*}&password={*}&username={*}, target=DataGet.Api, query=apiType={*}&password={*}&username={*}) [L141]
                                                                      └─ target_service DataGet.Api (see previous expansion)
                                                                    └─ calls GetAccounts (GET /api/accounting-file/{fileId}/accounts?apiType={*}&password={*}&username={*}, target=DataGet.Api, query=apiType={*}&password={*}&username={*}) [L65]
                                                                      └─ target_service DataGet.Api (see previous expansion)
                                                                    └─ calls GetCreditors (GET /api/accounting-file/{fileId}/creditors?apiType={*}&endDate={*}&password={*}&sourceAccountSystemId={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&password={*}&sourceAccountSystemId={*}&username={*}) [L156]
                                                                      └─ target_service DataGet.Api (see previous expansion)
                                                                    └─ calls GetDebtors (GET /api/accounting-file/{fileId}/debtors?apiType={*}&endDate={*}&password={*}&sourceAccountSystemId={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&password={*}&sourceAccountSystemId={*}&username={*}) [L171]
                                                                      └─ target_service DataGet.Api (see previous expansion)
                                                                    └─ calls GetMovements (GET /api/accounting-file/{fileId}/movement-journals?apiType={*}&endDate={*}&importFrequency={*}&importMovementOnly={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&importFrequency={*}&importMovementOnly={*}&password={*}&startDate={*}&username={*}) [L93]
                                                                      └─ target_service DataGet.Api (see previous expansion)
                                                                    └─ calls GetTransactions (GET /api/accounting-file/{fileId}/transactions?apiType={*}&endDate={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&password={*}&startDate={*}&username={*}) [L116]
                                                                      └─ target_service DataGet.Api (see previous expansion)
                                                                    └─ calls GetWages (GET /api/accounting-file/{fileId}/wages?apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}) [L189]
                                                                      └─ target_service DataGet.Api
                                                                        └─ [web] GET /api/accounting-file/{fileId}/wages  (DataGet.Api.Controllers.Connections.AccountingFileController.GetWages)  [L129–L145] [auth=Authentication.MachineToMachinePolicy]
                                                                          └─ sends_request GetWagesQuery [L134]
                                                                            └─ generic_pipeline_behaviors 2
                                                                              └─ DatagetTokenSyncBehaviour
                                                                              └─ DatagetTokenSyncBehaviour
                                                                            └─ handled_by Cirrus.Connections.DataGet.Queries.GetWagesQueryHandler.Handle [L26–L45]
                                                                              └─ uses_client DataGetClient [L41]
                                                                                └─ calls DeleteJournal (DELETE /api/accounting-file/{fileId}/journal/{sourceJournalId}?apiType={*}&password={*}&username={*}, target=DataGet.Api, query=apiType={*}&password={*}&username={*}) [L141]
                                                                                  └─ target_service DataGet.Api (see previous expansion)
                                                                                └─ calls GetAccounts (GET /api/accounting-file/{fileId}/accounts?apiType={*}&password={*}&username={*}, target=DataGet.Api, query=apiType={*}&password={*}&username={*}) [L65]
                                                                                  └─ target_service DataGet.Api (see previous expansion)
                                                                                └─ calls GetCreditors (GET /api/accounting-file/{fileId}/creditors?apiType={*}&endDate={*}&password={*}&sourceAccountSystemId={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&password={*}&sourceAccountSystemId={*}&username={*}) [L156]
                                                                                  └─ target_service DataGet.Api (see previous expansion)
                                                                                └─ calls GetDebtors (GET /api/accounting-file/{fileId}/debtors?apiType={*}&endDate={*}&password={*}&sourceAccountSystemId={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&password={*}&sourceAccountSystemId={*}&username={*}) [L171]
                                                                                  └─ target_service DataGet.Api (see previous expansion)
                                                                                └─ calls GetMovements (GET /api/accounting-file/{fileId}/movement-journals?apiType={*}&endDate={*}&importFrequency={*}&importMovementOnly={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&importFrequency={*}&importMovementOnly={*}&password={*}&startDate={*}&username={*}) [L93]
                                                                                  └─ target_service DataGet.Api (see previous expansion)
                                                                                └─ calls GetTransactions (GET /api/accounting-file/{fileId}/transactions?apiType={*}&endDate={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&password={*}&startDate={*}&username={*}) [L116]
                                                                                  └─ target_service DataGet.Api (see previous expansion)
                                                                                └─ calls GetWages (GET /api/accounting-file/{fileId}/wages?apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}) [L189]
                                                                                  └─ target_service DataGet.Api (see previous expansion)
                                                                                └─ calls GetWages (GET /api/accounting-file/{fileId}/wages?apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}) [L190]
                                                                                  └─ target_service DataGet.Api (see previous expansion)
                                                                                └─ calls GetWages (GET /api/accounting-file/{fileId}/wages?apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}) [L191]
                                                                                  └─ target_service DataGet.Api (see previous expansion)
                                                                                └─ calls GetWages (GET /api/accounting-file/{fileId}/wages?apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}) [L192]
                                                                                  └─ target_service DataGet.Api (see previous expansion)
                                                                                └─ calls GetWages (GET /api/accounting-file/{fileId}/wages?apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}) [L193]
                                                                                  └─ target_service DataGet.Api (see previous expansion)
                                                                                └─ calls PostJournal (POST /api/accounting-file/{fileId}/journal?apiType={*}, target=DataGet.Api, query=apiType={*}) [L127]
                                                                                  └─ target_service DataGet.Api
                                                                                    └─ [web] POST /api/accounting-file/{fileId}/journal  (DataGet.Api.Controllers.Connections.AccountingFileController.PostJournal)  [L79–L87] [auth=Authentication.MachineToMachinePolicy]
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
                                                                                └─ calls Disconnect (DELETE /api/connection/{apiType}/disconnect?fileId={*}, target=DataGet.Api, query=fileId={*}) [L459]
                                                                                  └─ target_service DataGet.Api
                                                                                    └─ [web] DELETE /api/connection/{apiType}/disconnect  (DataGet.Api.Controllers.Connections.ConnectionController.Disconnect)  [L90–L98] [auth=Authentication.MachineToMachinePolicy]
                                                                                      └─ sends_request DisconnectCommand [L94]
                                                                                        └─ generic_pipeline_behaviors 2
                                                                                          └─ DatagetTokenSyncBehaviour
                                                                                          └─ DatagetTokenSyncBehaviour
                                                                                        └─ handled_by DataGet.Connections.App.Commands.DisconnectCommandHandler.Handle [L26–L99]
                                                                                          └─ uses_service IConnectionContextProvider (InstancePerLifetimeScope)
                                                                                            └─ method ConnectionContext [L64]
                                                                                          └─ uses_service IControlledRepository<Connection>
                                                                                            └─ method WriteQuery [L77]
                                                                                          └─ uses_service IControlledRepository<FileToken>
                                                                                            └─ method WriteQuery [L92]
                                                                                          └─ uses_service IControlledRepository<UserToken>
                                                                                            └─ method WriteQuery [L70]
                                                                                          └─ uses_service IExternalApiServiceFactory (InstancePerLifetimeScope)
                                                                                            └─ method GetExternalApiFromApiType [L47]
                                                                                          └─ uses_service RequestProcessor
                                                                                            └─ method ProcessAsync [L68]
                                                                                └─ calls DisconnectFile (DELETE /api/connection/{apiType}/disconnect-file?fileId={*}, target=DataGet.Api, query=fileId={*}) [L503]
                                                                                  └─ target_service DataGet.Api
                                                                                    └─ [web] DELETE /api/connection/{apiType}/disconnect-file  (DataGet.Api.Controllers.Connections.ConnectionController.DisconnectFile)  [L150–L157] [auth=Authentication.MachineToMachinePolicy]
                                                                                      └─ sends_request DisconnectFileCommand [L154]
                                                                                        └─ generic_pipeline_behaviors 2
                                                                                          └─ DatagetTokenSyncBehaviour
                                                                                          └─ DatagetTokenSyncBehaviour
                                                                                        └─ handled_by Cirrus.Connections.DataGet.Commands.DisconnectFileCommandHandler.Handle [L21–L35]
                                                                                          └─ uses_client IDatagetClient [L32]
                                                                                          └─ uses_service IDatagetClient (InstancePerLifetimeScope)
                                                                                            └─ method DisconnectFileAsync [L32]
                                                                                └─ calls GetAccountingFiles (GET /api/connection/{apiType}/accounting-files, target=DataGet.Api) [L52]
                                                                                  └─ target_service DataGet.Api
                                                                                    └─ [web] GET /api/connection/{apiType}/accounting-files  (DataGet.Api.Controllers.Connections.ConnectionController.GetAccountingFiles)  [L100–L108] [auth=Authentication.MachineToMachinePolicy]
                                                                                      └─ sends_request GetAccountingFilesQuery [L104]
                                                                                        └─ generic_pipeline_behaviors 2
                                                                                          └─ DatagetTokenSyncBehaviour
                                                                                          └─ DatagetTokenSyncBehaviour
                                                                                        └─ handled_by Cirrus.Connections.DataGet.Queries.GetAccountingFilesQueryHandler.Handle [L15–L38]
                                                                                          └─ uses_client DataGetClient [L26]
                                                                                            └─ calls DeleteJournal (DELETE /api/accounting-file/{fileId}/journal/{sourceJournalId}?apiType={*}&password={*}&username={*}, target=DataGet.Api, query=apiType={*}&password={*}&username={*}) [L141]
                                                                                              └─ target_service DataGet.Api (see previous expansion)
                                                                                            └─ calls GetAccounts (GET /api/accounting-file/{fileId}/accounts?apiType={*}&password={*}&username={*}, target=DataGet.Api, query=apiType={*}&password={*}&username={*}) [L65]
                                                                                              └─ target_service DataGet.Api (see previous expansion)
                                                                                            └─ calls GetCreditors (GET /api/accounting-file/{fileId}/creditors?apiType={*}&endDate={*}&password={*}&sourceAccountSystemId={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&password={*}&sourceAccountSystemId={*}&username={*}) [L156]
                                                                                              └─ target_service DataGet.Api (see previous expansion)
                                                                                            └─ calls GetDebtors (GET /api/accounting-file/{fileId}/debtors?apiType={*}&endDate={*}&password={*}&sourceAccountSystemId={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&password={*}&sourceAccountSystemId={*}&username={*}) [L171]
                                                                                              └─ target_service DataGet.Api (see previous expansion)
                                                                                            └─ calls GetMovements (GET /api/accounting-file/{fileId}/movement-journals?apiType={*}&endDate={*}&importFrequency={*}&importMovementOnly={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&importFrequency={*}&importMovementOnly={*}&password={*}&startDate={*}&username={*}) [L93]
                                                                                              └─ target_service DataGet.Api (see previous expansion)
                                                                                            └─ calls GetTransactions (GET /api/accounting-file/{fileId}/transactions?apiType={*}&endDate={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&password={*}&startDate={*}&username={*}) [L116]
                                                                                              └─ target_service DataGet.Api (see previous expansion)
                                                                                            └─ calls GetWages (GET /api/accounting-file/{fileId}/wages?apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}) [L189]
                                                                                              └─ target_service DataGet.Api (see previous expansion)
                                                                                            └─ calls GetWages (GET /api/accounting-file/{fileId}/wages?apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}) [L190]
                                                                                              └─ target_service DataGet.Api (see previous expansion)
                                                                                            └─ calls GetWages (GET /api/accounting-file/{fileId}/wages?apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}) [L191]
                                                                                              └─ target_service DataGet.Api (see previous expansion)
                                                                                            └─ calls GetWages (GET /api/accounting-file/{fileId}/wages?apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}) [L192]
                                                                                              └─ target_service DataGet.Api (see previous expansion)
                                                                                            └─ calls GetWages (GET /api/accounting-file/{fileId}/wages?apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}) [L193]
                                                                                              └─ target_service DataGet.Api (see previous expansion)
                                                                                            └─ calls PostJournal (POST /api/accounting-file/{fileId}/journal?apiType={*}, target=DataGet.Api, query=apiType={*}) [L127]
                                                                                              └─ target_service DataGet.Api (see previous expansion)
                                                                                            └─ calls Disconnect (DELETE /api/connection/{apiType}/disconnect?fileId={*}, target=DataGet.Api, query=fileId={*}) [L459]
                                                                                              └─ target_service DataGet.Api (see previous expansion)
                                                                                            └─ calls DisconnectFile (DELETE /api/connection/{apiType}/disconnect-file?fileId={*}, target=DataGet.Api, query=fileId={*}) [L503]
                                                                                              └─ target_service DataGet.Api (see previous expansion)
                                                                                            └─ calls GetAccountingFiles (GET /api/connection/{apiType}/accounting-files, target=DataGet.Api) [L52]
                                                                                              └─ target_service DataGet.Api (see previous expansion)
                                                                                            └─ calls GetAuthorisationUrl (GET /api/connection/{apiType}/authorisation-url?callbackUrl={*}, target=DataGet.Api, query=callbackUrl={*}) [L449]
                                                                                              └─ target_service DataGet.Api
                                                                                                └─ [web] GET /api/connection/{apiType}/authorisation-url  (DataGet.Api.Controllers.Connections.ConnectionController.GetAuthorisationUrl)  [L46–L54] [auth=Authentication.MachineToMachinePolicy]
                                                                                                  └─ sends_request GetAuthorizationUrlQuery [L50]
                                                                                                    └─ generic_pipeline_behaviors 2
                                                                                                      └─ DatagetTokenSyncBehaviour
                                                                                                      └─ DatagetTokenSyncBehaviour
                                                                                                    └─ handled_by DataGet.Connections.App.Queries.GetAuthorizationUrlQueryHandler.Handle [L23–L60]
                                                                                                      └─ uses_service ConnectionContextProvider
                                                                                                        └─ method ConnectionContext [L39]
                                                                                                      └─ uses_service ExternalApiServiceFactory
                                                                                                        └─ method GetExternalApiFromApiType [L51]
                                                                                                      └─ uses_service RequestProcessor
                                                                                                        └─ method Process [L53]
                                                                                            └─ calls StoreExistingFileTokens (POST /api/connection/{apiType}/file-token?callbackUrl={*}, target=DataGet.Api, query=callbackUrl={*}) [L493]
                                                                                              └─ target_service DataGet.Api
                                                                                                └─ [web] POST /api/connection/{apiType}/file-token  (DataGet.Api.Controllers.Connections.ConnectionController.StoreExistingFileTokens)  [L141–L148] [auth=Authentication.MachineToMachinePolicy]
                                                                                                  └─ sends_request StoreExistingFileTokensCommand [L145]
                                                                                                    └─ generic_pipeline_behaviors 2
                                                                                                      └─ DatagetTokenSyncBehaviour
                                                                                                      └─ DatagetTokenSyncBehaviour
                                                                                                    └─ handled_by DataGet.Connections.App.Commands.StoreExistingFileTokensCommandHandler.Handle [L24–L79]
                                                                                                      └─ uses_service FileTokenService
                                                                                                        └─ method GetTokenAsync [L41]
                                                                                                      └─ uses_service IControlledRepository<FileToken>
                                                                                                        └─ method ReadQuery [L52]
                                                                                                      └─ uses_service ILogger<StoreExistingFileTokensCommandHandler>
                                                                                                        └─ method LogError [L48]
                                                                                            └─ calls StoreExistingToken (POST /api/connection/{apiType}/token?callbackUrl={*}, target=DataGet.Api, query=callbackUrl={*}) [L483]
                                                                                              └─ target_service DataGet.Api
                                                                                                └─ [web] POST /api/connection/{apiType}/token  (DataGet.Api.Controllers.Connections.ConnectionController.StoreExistingToken)  [L132–L139] [auth=Authentication.MachineToMachinePolicy]
                                                                                                  └─ sends_request StoreExistingTokenCommand [L136]
                                                                                                    └─ generic_pipeline_behaviors 2
                                                                                                      └─ DatagetTokenSyncBehaviour
                                                                                                      └─ DatagetTokenSyncBehaviour
                                                                                                    └─ handled_by DataGet.Connections.App.Commands.StoreExistingTokenCommandHandler.Handle [L17–L34]
                                                                                                      └─ uses_service UserTokenService
                                                                                                        └─ method GetTokenAsync [L28]
                                                                                            └─ calls TestConnection (GET /api/connection/{apiType}/test-connection?fileId={*}&password={*}&username={*}, target=DataGet.Api, query=fileId={*}&password={*}&username={*}) [L472]
                                                                                              └─ target_service DataGet.Api
                                                                                                └─ [web] GET /api/connection/{apiType}/test-connection  (DataGet.Api.Controllers.Connections.ConnectionController.TestConnection)  [L110–L119] [auth=Authentication.MachineToMachinePolicy]
                                                                                                  └─ sends_request TestConnectionCommand [L114]
                                                                                                    └─ generic_pipeline_behaviors 2
                                                                                                      └─ DatagetTokenSyncBehaviour
                                                                                                      └─ DatagetTokenSyncBehaviour
                                                                                                    └─ handled_by DataGet.Connections.App.Commands.TestConnectionCommandHandler.Handle [L25–L61]
                                                                                                      └─ uses_service IExternalApiServiceFactory (InstancePerLifetimeScope)
                                                                                                        └─ method GetExternalApiFromApiType [L38]
                                                                                                      └─ uses_service RequestProcessor
                                                                                                        └─ method ProcessAsync [L47]
                                                                                            └─ calls GetActivityStatementsDetail (GET /api/gov-link/activity-statements/detail?clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}) [L214]
                                                                                              └─ target_service DataGet.Api
                                                                                                └─ [web] GET /api/gov-link/activity-statements/detail  (DataGet.Api.Controllers.GovLink.ActivityStatementController.GetActivityStatementsDetail)  [L33–L42] [auth=Authentication.MachineToMachinePolicy]
                                                                                            └─ calls GetActivityStatementSummary (GET /api/gov-link/activity-statements/summary?clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}) [L231]
                                                                                              └─ target_service DataGet.Api
                                                                                                └─ [web] GET /api/gov-link/activity-statements/summary  (DataGet.Api.Controllers.GovLink.ActivityStatementController.GetActivityStatementSummary)  [L44–L53] [auth=Authentication.MachineToMachinePolicy]
                                                                                            └─ calls GetTransactionDetail (GET /api/gov-link/client-accounts/detail?clientABN={*}&clientABNBranch={*}&clientAccountType={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientAccountType={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}) [L250]
                                                                                              └─ target_service DataGet.Api
                                                                                                └─ [web] GET /api/gov-link/client-accounts/detail  (DataGet.Api.Controllers.GovLink.ClientAccountController.GetTransactionDetail)  [L22–L31] [auth=Authentication.MachineToMachinePolicy]
                                                                                            └─ calls GetTransactionSummary (GET /api/gov-link/client-accounts/summary?clientABN={*}&clientABNBranch={*}&clientAccountType={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientAccountType={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}) [L269]
                                                                                              └─ target_service DataGet.Api
                                                                                                └─ [web] GET /api/gov-link/client-accounts/summary  (DataGet.Api.Controllers.GovLink.ClientAccountController.GetTransactionSummary)  [L33–L42] [auth=Authentication.MachineToMachinePolicy]
                                                                                            └─ calls GetProfileComparison (GET /api/gov-link/individual-income-tax-returns/profile-compare?clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&targetFinancialYear={*}&taxAgentABN={*}&taxAgentNumber={*}, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&targetFinancialYear={*}&taxAgentABN={*}&taxAgentNumber={*}) [L325]
                                                                                              └─ target_service DataGet.Api
                                                                                                └─ [web] GET /api/gov-link/individual-income-tax-returns/profile-compare  (DataGet.Api.Controllers.GovLink.IndividualIncomeTaxReturnController.GetProfileComparison)  [L44–L53] [auth=Authentication.MachineToMachinePolicy]
                                                                                            └─ calls GetReportSummary (GET /api/gov-link/individual-income-tax-returns/summary?clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&targetFinancialYear={*}&taxAgentABN={*}&taxAgentNumber={*}, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&targetFinancialYear={*}&taxAgentABN={*}&taxAgentNumber={*}) [L307]
                                                                                              └─ target_service DataGet.Api
                                                                                                └─ [web] GET /api/gov-link/individual-income-tax-returns/summary  (DataGet.Api.Controllers.GovLink.IndividualIncomeTaxReturnController.GetReportSummary)  [L34–L42] [auth=Authentication.MachineToMachinePolicy]
                                                                                            └─ calls GetFraudHeaderFeedback (GET /api/hmrc/feedback?apiResource={*}, target=DataGet.Api, query=apiResource={*}) [L387]
                                                                                              └─ target_service DataGet.Api
                                                                                                └─ [web] GET /api/hmrc/feedback  (DataGet.Api.Controllers.Integrations.HmrcController.GetFraudHeaderFeedback)  [L79–L89] [auth=Authentication.MachineToMachinePolicy]
                                                                                                  └─ uses_service HmrcApiService (AddScoped)
                                                                                                    └─ method GetFraudHeaderFeedback [L84]
                                                                                            └─ calls GetVatObligations (POST /api/hmrc/vat/obligations?endDate={*}&startDate={*}&status={*}&vrn={*}, target=DataGet.Api, query=endDate={*}&startDate={*}&status={*}&vrn={*}) [L358]
                                                                                              └─ target_service DataGet.Api
                                                                                                └─ [web] POST /api/hmrc/vat/obligations  (DataGet.Api.Controllers.Integrations.HmrcController.GetVatObligations)  [L42–L53] [auth=Authentication.MachineToMachinePolicy]
                                                                                                  └─ uses_service HmrcApiService (AddScoped)
                                                                                                    └─ method GetVatObligations [L48]
                                                                                            └─ calls GetVatPackage (POST /api/hmrc/vat/package?endDate={*}&startDate={*}&vrn={*}, target=DataGet.Api, query=endDate={*}&startDate={*}&vrn={*}) [L343]
                                                                                              └─ target_service DataGet.Api
                                                                                                └─ [web] POST /api/hmrc/vat/package  (DataGet.Api.Controllers.Integrations.HmrcController.GetVatPackage)  [L29–L40] [auth=Authentication.MachineToMachinePolicy]
                                                                                                  └─ uses_service HmrcApiService (AddScoped)
                                                                                                    └─ method GetVatPackage [L35]
                                                                                            └─ calls SubmitVatReturn (POST /api/hmrc/vat/submit, target=DataGet.Api) [L367]
                                                                                              └─ target_service DataGet.Api
                                                                                                └─ [web] POST /api/hmrc/vat/submit  (DataGet.Api.Controllers.Integrations.HmrcController.SubmitVatReturn)  [L55–L65] [auth=Authentication.MachineToMachinePolicy]
                                                                                                  └─ uses_service HmrcApiService (AddScoped)
                                                                                                    └─ method SubmitVatReturn [L60]
                                                                                            └─ calls ValidateFraudHeaders (POST /api/hmrc/validate, target=DataGet.Api) [L377]
                                                                                              └─ target_service DataGet.Api
                                                                                                └─ [web] POST /api/hmrc/validate  (DataGet.Api.Controllers.Integrations.HmrcController.ValidateFraudHeaders)  [L67–L77] [auth=Authentication.MachineToMachinePolicy]
                                                                                                  └─ uses_service HmrcApiService (AddScoped)
                                                                                                    └─ method ValidateFraudHeaders [L72]
                                                                                            └─ calls GetFraudHeaderFeedback (GET /api/hmrc/feedback?apiResource={*}, target=DataGet.Api, query=apiResource={*}) [L387]
                                                                                              └─ target_service DataGet.Api (see previous expansion)
                                                                                            └─ calls GetVatObligations (POST /api/hmrc/vat/obligations?endDate={*}&startDate={*}&status={*}&vrn={*}, target=DataGet.Api, query=endDate={*}&startDate={*}&status={*}&vrn={*}) [L358]
                                                                                              └─ target_service DataGet.Api (see previous expansion)
                                                                                            └─ calls SubmitVatReturn (POST /api/hmrc/vat/submit, target=DataGet.Api) [L367]
                                                                                              └─ target_service DataGet.Api (see previous expansion)
                                                                                            └─ calls ValidateFraudHeaders (POST /api/hmrc/validate, target=DataGet.Api) [L377]
                                                                                              └─ target_service DataGet.Api (see previous expansion)
                                                                                          └─ uses_service DataGetClient
                                                                                            └─ method GetAccountingFilesAsync [L26]
                                                                                └─ calls GetAuthorisationUrl (GET /api/connection/{apiType}/authorisation-url?callbackUrl={*}, target=DataGet.Api, query=callbackUrl={*}) [L449]
                                                                                  └─ target_service DataGet.Api (see previous expansion)
                                                                                └─ calls StoreExistingFileTokens (POST /api/connection/{apiType}/file-token?callbackUrl={*}, target=DataGet.Api, query=callbackUrl={*}) [L493]
                                                                                  └─ target_service DataGet.Api (see previous expansion)
                                                                                └─ calls StoreExistingToken (POST /api/connection/{apiType}/token?callbackUrl={*}, target=DataGet.Api, query=callbackUrl={*}) [L483]
                                                                                  └─ target_service DataGet.Api (see previous expansion)
                                                                                └─ calls TestConnection (GET /api/connection/{apiType}/test-connection?fileId={*}&password={*}&username={*}, target=DataGet.Api, query=fileId={*}&password={*}&username={*}) [L472]
                                                                                  └─ target_service DataGet.Api (see previous expansion)
                                                                                └─ calls GetActivityStatementsDetail (GET /api/gov-link/activity-statements/detail?clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}) [L214]
                                                                                  └─ target_service DataGet.Api (see previous expansion)
                                                                                └─ calls GetActivityStatementSummary (GET /api/gov-link/activity-statements/summary?clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}) [L231]
                                                                                  └─ target_service DataGet.Api (see previous expansion)
                                                                                └─ calls GetTransactionDetail (GET /api/gov-link/client-accounts/detail?clientABN={*}&clientABNBranch={*}&clientAccountType={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientAccountType={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}) [L250]
                                                                                  └─ target_service DataGet.Api (see previous expansion)
                                                                                └─ calls GetTransactionSummary (GET /api/gov-link/client-accounts/summary?clientABN={*}&clientABNBranch={*}&clientAccountType={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientAccountType={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}) [L269]
                                                                                  └─ target_service DataGet.Api (see previous expansion)
                                                                                └─ calls GetProfileComparison (GET /api/gov-link/individual-income-tax-returns/profile-compare?clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&targetFinancialYear={*}&taxAgentABN={*}&taxAgentNumber={*}, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&targetFinancialYear={*}&taxAgentABN={*}&taxAgentNumber={*}) [L325]
                                                                                  └─ target_service DataGet.Api (see previous expansion)
                                                                                └─ calls GetReportSummary (GET /api/gov-link/individual-income-tax-returns/summary?clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&targetFinancialYear={*}&taxAgentABN={*}&taxAgentNumber={*}, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&targetFinancialYear={*}&taxAgentABN={*}&taxAgentNumber={*}) [L307]
                                                                                  └─ target_service DataGet.Api (see previous expansion)
                                                                                └─ calls GetFraudHeaderFeedback (GET /api/hmrc/feedback?apiResource={*}, target=DataGet.Api, query=apiResource={*}) [L387]
                                                                                  └─ target_service DataGet.Api (see previous expansion)
                                                                                └─ calls GetVatObligations (POST /api/hmrc/vat/obligations?endDate={*}&startDate={*}&status={*}&vrn={*}, target=DataGet.Api, query=endDate={*}&startDate={*}&status={*}&vrn={*}) [L358]
                                                                                  └─ target_service DataGet.Api (see previous expansion)
                                                                                └─ calls GetVatPackage (POST /api/hmrc/vat/package?endDate={*}&startDate={*}&vrn={*}, target=DataGet.Api, query=endDate={*}&startDate={*}&vrn={*}) [L343]
                                                                                  └─ target_service DataGet.Api (see previous expansion)
                                                                                └─ calls SubmitVatReturn (POST /api/hmrc/vat/submit, target=DataGet.Api) [L367]
                                                                                  └─ target_service DataGet.Api (see previous expansion)
                                                                                └─ calls ValidateFraudHeaders (POST /api/hmrc/validate, target=DataGet.Api) [L377]
                                                                                  └─ target_service DataGet.Api (see previous expansion)
                                                                                └─ calls GetFraudHeaderFeedback (GET /api/hmrc/feedback?apiResource={*}, target=DataGet.Api, query=apiResource={*}) [L387]
                                                                                  └─ target_service DataGet.Api (see previous expansion)
                                                                                └─ calls GetVatObligations (POST /api/hmrc/vat/obligations?endDate={*}&startDate={*}&status={*}&vrn={*}, target=DataGet.Api, query=endDate={*}&startDate={*}&status={*}&vrn={*}) [L358]
                                                                                  └─ target_service DataGet.Api (see previous expansion)
                                                                                └─ calls SubmitVatReturn (POST /api/hmrc/vat/submit, target=DataGet.Api) [L367]
                                                                                  └─ target_service DataGet.Api (see previous expansion)
                                                                                └─ calls ValidateFraudHeaders (POST /api/hmrc/validate, target=DataGet.Api) [L377]
                                                                                  └─ target_service DataGet.Api (see previous expansion)
                                                                              └─ uses_service DataGetClient
                                                                                └─ method GetWagesAsync [L41]
                                                                              └─ uses_service DatagetFileIdService
                                                                                └─ method GetFileIdFromSource [L39]
                                                                    └─ calls GetWages (GET /api/accounting-file/{fileId}/wages?apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}) [L190]
                                                                      └─ target_service DataGet.Api (see previous expansion)
                                                                    └─ calls GetWages (GET /api/accounting-file/{fileId}/wages?apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}) [L191]
                                                                      └─ target_service DataGet.Api (see previous expansion)
                                                                    └─ calls GetWages (GET /api/accounting-file/{fileId}/wages?apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}) [L192]
                                                                      └─ target_service DataGet.Api (see previous expansion)
                                                                    └─ calls GetWages (GET /api/accounting-file/{fileId}/wages?apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}) [L193]
                                                                      └─ target_service DataGet.Api (see previous expansion)
                                                                    └─ calls PostJournal (POST /api/accounting-file/{fileId}/journal?apiType={*}, target=DataGet.Api, query=apiType={*}) [L127]
                                                                      └─ target_service DataGet.Api (see previous expansion)
                                                                    └─ calls Disconnect (DELETE /api/connection/{apiType}/disconnect?fileId={*}, target=DataGet.Api, query=fileId={*}) [L459]
                                                                      └─ target_service DataGet.Api (see previous expansion)
                                                                    └─ calls DisconnectFile (DELETE /api/connection/{apiType}/disconnect-file?fileId={*}, target=DataGet.Api, query=fileId={*}) [L503]
                                                                      └─ target_service DataGet.Api (see previous expansion)
                                                                    └─ calls GetAccountingFiles (GET /api/connection/{apiType}/accounting-files, target=DataGet.Api) [L52]
                                                                      └─ target_service DataGet.Api (see previous expansion)
                                                                    └─ calls GetAuthorisationUrl (GET /api/connection/{apiType}/authorisation-url?callbackUrl={*}, target=DataGet.Api, query=callbackUrl={*}) [L449]
                                                                      └─ target_service DataGet.Api (see previous expansion)
                                                                    └─ calls StoreExistingFileTokens (POST /api/connection/{apiType}/file-token?callbackUrl={*}, target=DataGet.Api, query=callbackUrl={*}) [L493]
                                                                      └─ target_service DataGet.Api (see previous expansion)
                                                                    └─ calls StoreExistingToken (POST /api/connection/{apiType}/token?callbackUrl={*}, target=DataGet.Api, query=callbackUrl={*}) [L483]
                                                                      └─ target_service DataGet.Api (see previous expansion)
                                                                    └─ calls TestConnection (GET /api/connection/{apiType}/test-connection?fileId={*}&password={*}&username={*}, target=DataGet.Api, query=fileId={*}&password={*}&username={*}) [L472]
                                                                      └─ target_service DataGet.Api (see previous expansion)
                                                                    └─ calls GetActivityStatementsDetail (GET /api/gov-link/activity-statements/detail?clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}) [L214]
                                                                      └─ target_service DataGet.Api (see previous expansion)
                                                                    └─ calls GetActivityStatementSummary (GET /api/gov-link/activity-statements/summary?clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}) [L231]
                                                                      └─ target_service DataGet.Api (see previous expansion)
                                                                    └─ calls GetTransactionDetail (GET /api/gov-link/client-accounts/detail?clientABN={*}&clientABNBranch={*}&clientAccountType={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientAccountType={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}) [L250]
                                                                      └─ target_service DataGet.Api (see previous expansion)
                                                                    └─ calls GetTransactionSummary (GET /api/gov-link/client-accounts/summary?clientABN={*}&clientABNBranch={*}&clientAccountType={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientAccountType={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}) [L269]
                                                                      └─ target_service DataGet.Api (see previous expansion)
                                                                    └─ calls GetProfileComparison (GET /api/gov-link/individual-income-tax-returns/profile-compare?clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&targetFinancialYear={*}&taxAgentABN={*}&taxAgentNumber={*}, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&targetFinancialYear={*}&taxAgentABN={*}&taxAgentNumber={*}) [L325]
                                                                      └─ target_service DataGet.Api (see previous expansion)
                                                                    └─ calls GetReportSummary (GET /api/gov-link/individual-income-tax-returns/summary?clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&targetFinancialYear={*}&taxAgentABN={*}&taxAgentNumber={*}, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&targetFinancialYear={*}&taxAgentABN={*}&taxAgentNumber={*}) [L307]
                                                                      └─ target_service DataGet.Api (see previous expansion)
                                                                    └─ calls GetFraudHeaderFeedback (GET /api/hmrc/feedback?apiResource={*}, target=DataGet.Api, query=apiResource={*}) [L387]
                                                                      └─ target_service DataGet.Api (see previous expansion)
                                                                    └─ calls GetVatObligations (POST /api/hmrc/vat/obligations?endDate={*}&startDate={*}&status={*}&vrn={*}, target=DataGet.Api, query=endDate={*}&startDate={*}&status={*}&vrn={*}) [L358]
                                                                      └─ target_service DataGet.Api (see previous expansion)
                                                                    └─ calls GetVatPackage (POST /api/hmrc/vat/package?endDate={*}&startDate={*}&vrn={*}, target=DataGet.Api, query=endDate={*}&startDate={*}&vrn={*}) [L343]
                                                                      └─ target_service DataGet.Api (see previous expansion)
                                                                    └─ calls SubmitVatReturn (POST /api/hmrc/vat/submit, target=DataGet.Api) [L367]
                                                                      └─ target_service DataGet.Api (see previous expansion)
                                                                    └─ calls ValidateFraudHeaders (POST /api/hmrc/validate, target=DataGet.Api) [L377]
                                                                      └─ target_service DataGet.Api (see previous expansion)
                                                                    └─ calls GetFraudHeaderFeedback (GET /api/hmrc/feedback?apiResource={*}, target=DataGet.Api, query=apiResource={*}) [L387]
                                                                      └─ target_service DataGet.Api (see previous expansion)
                                                                    └─ calls GetVatObligations (POST /api/hmrc/vat/obligations?endDate={*}&startDate={*}&status={*}&vrn={*}, target=DataGet.Api, query=endDate={*}&startDate={*}&status={*}&vrn={*}) [L358]
                                                                      └─ target_service DataGet.Api (see previous expansion)
                                                                    └─ calls SubmitVatReturn (POST /api/hmrc/vat/submit, target=DataGet.Api) [L367]
                                                                      └─ target_service DataGet.Api (see previous expansion)
                                                                    └─ calls ValidateFraudHeaders (POST /api/hmrc/validate, target=DataGet.Api) [L377]
                                                                      └─ target_service DataGet.Api (see previous expansion)
                                                                  └─ uses_service DataGetClient
                                                                    └─ method GetTransactionsAsync [L50]
                                                                  └─ uses_service DatagetFileIdService
                                                                    └─ method GetFileIdFromSource [L48]
                                                        └─ calls GetWages (GET /api/accounting-file/{fileId}/wages?apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}) [L189]
                                                          └─ target_service DataGet.Api (see previous expansion)
                                                        └─ calls GetWages (GET /api/accounting-file/{fileId}/wages?apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}) [L190]
                                                          └─ target_service DataGet.Api (see previous expansion)
                                                        └─ calls GetWages (GET /api/accounting-file/{fileId}/wages?apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}) [L191]
                                                          └─ target_service DataGet.Api (see previous expansion)
                                                        └─ calls GetWages (GET /api/accounting-file/{fileId}/wages?apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}) [L192]
                                                          └─ target_service DataGet.Api (see previous expansion)
                                                        └─ calls GetWages (GET /api/accounting-file/{fileId}/wages?apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}) [L193]
                                                          └─ target_service DataGet.Api (see previous expansion)
                                                        └─ calls PostJournal (POST /api/accounting-file/{fileId}/journal?apiType={*}, target=DataGet.Api, query=apiType={*}) [L127]
                                                          └─ target_service DataGet.Api (see previous expansion)
                                                        └─ calls Disconnect (DELETE /api/connection/{apiType}/disconnect?fileId={*}, target=DataGet.Api, query=fileId={*}) [L459]
                                                          └─ target_service DataGet.Api (see previous expansion)
                                                        └─ calls DisconnectFile (DELETE /api/connection/{apiType}/disconnect-file?fileId={*}, target=DataGet.Api, query=fileId={*}) [L503]
                                                          └─ target_service DataGet.Api (see previous expansion)
                                                        └─ calls GetAccountingFiles (GET /api/connection/{apiType}/accounting-files, target=DataGet.Api) [L52]
                                                          └─ target_service DataGet.Api (see previous expansion)
                                                        └─ calls GetAuthorisationUrl (GET /api/connection/{apiType}/authorisation-url?callbackUrl={*}, target=DataGet.Api, query=callbackUrl={*}) [L449]
                                                          └─ target_service DataGet.Api (see previous expansion)
                                                        └─ calls StoreExistingFileTokens (POST /api/connection/{apiType}/file-token?callbackUrl={*}, target=DataGet.Api, query=callbackUrl={*}) [L493]
                                                          └─ target_service DataGet.Api (see previous expansion)
                                                        └─ calls StoreExistingToken (POST /api/connection/{apiType}/token?callbackUrl={*}, target=DataGet.Api, query=callbackUrl={*}) [L483]
                                                          └─ target_service DataGet.Api (see previous expansion)
                                                        └─ calls TestConnection (GET /api/connection/{apiType}/test-connection?fileId={*}&password={*}&username={*}, target=DataGet.Api, query=fileId={*}&password={*}&username={*}) [L472]
                                                          └─ target_service DataGet.Api (see previous expansion)
                                                        └─ calls GetActivityStatementsDetail (GET /api/gov-link/activity-statements/detail?clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}) [L214]
                                                          └─ target_service DataGet.Api (see previous expansion)
                                                        └─ calls GetActivityStatementSummary (GET /api/gov-link/activity-statements/summary?clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}) [L231]
                                                          └─ target_service DataGet.Api (see previous expansion)
                                                        └─ calls GetTransactionDetail (GET /api/gov-link/client-accounts/detail?clientABN={*}&clientABNBranch={*}&clientAccountType={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientAccountType={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}) [L250]
                                                          └─ target_service DataGet.Api (see previous expansion)
                                                        └─ calls GetTransactionSummary (GET /api/gov-link/client-accounts/summary?clientABN={*}&clientABNBranch={*}&clientAccountType={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientAccountType={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}) [L269]
                                                          └─ target_service DataGet.Api (see previous expansion)
                                                        └─ calls GetProfileComparison (GET /api/gov-link/individual-income-tax-returns/profile-compare?clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&targetFinancialYear={*}&taxAgentABN={*}&taxAgentNumber={*}, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&targetFinancialYear={*}&taxAgentABN={*}&taxAgentNumber={*}) [L325]
                                                          └─ target_service DataGet.Api (see previous expansion)
                                                        └─ calls GetReportSummary (GET /api/gov-link/individual-income-tax-returns/summary?clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&targetFinancialYear={*}&taxAgentABN={*}&taxAgentNumber={*}, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&targetFinancialYear={*}&taxAgentABN={*}&taxAgentNumber={*}) [L307]
                                                          └─ target_service DataGet.Api (see previous expansion)
                                                        └─ calls GetFraudHeaderFeedback (GET /api/hmrc/feedback?apiResource={*}, target=DataGet.Api, query=apiResource={*}) [L387]
                                                          └─ target_service DataGet.Api (see previous expansion)
                                                        └─ calls GetVatObligations (POST /api/hmrc/vat/obligations?endDate={*}&startDate={*}&status={*}&vrn={*}, target=DataGet.Api, query=endDate={*}&startDate={*}&status={*}&vrn={*}) [L358]
                                                          └─ target_service DataGet.Api (see previous expansion)
                                                        └─ calls GetVatPackage (POST /api/hmrc/vat/package?endDate={*}&startDate={*}&vrn={*}, target=DataGet.Api, query=endDate={*}&startDate={*}&vrn={*}) [L343]
                                                          └─ target_service DataGet.Api (see previous expansion)
                                                        └─ calls SubmitVatReturn (POST /api/hmrc/vat/submit, target=DataGet.Api) [L367]
                                                          └─ target_service DataGet.Api (see previous expansion)
                                                        └─ calls ValidateFraudHeaders (POST /api/hmrc/validate, target=DataGet.Api) [L377]
                                                          └─ target_service DataGet.Api (see previous expansion)
                                                        └─ calls GetFraudHeaderFeedback (GET /api/hmrc/feedback?apiResource={*}, target=DataGet.Api, query=apiResource={*}) [L387]
                                                          └─ target_service DataGet.Api (see previous expansion)
                                                        └─ calls GetVatObligations (POST /api/hmrc/vat/obligations?endDate={*}&startDate={*}&status={*}&vrn={*}, target=DataGet.Api, query=endDate={*}&startDate={*}&status={*}&vrn={*}) [L358]
                                                          └─ target_service DataGet.Api (see previous expansion)
                                                        └─ calls SubmitVatReturn (POST /api/hmrc/vat/submit, target=DataGet.Api) [L367]
                                                          └─ target_service DataGet.Api (see previous expansion)
                                                        └─ calls ValidateFraudHeaders (POST /api/hmrc/validate, target=DataGet.Api) [L377]
                                                          └─ target_service DataGet.Api (see previous expansion)
                                                      └─ uses_service ApiService (SingleInstance)
                                                        └─ method GetFeatures [L77]
                                                      └─ uses_service DataGetClient
                                                        └─ method GetAccountsAsync [L81]
                                                      └─ uses_service DatagetFileIdService
                                                        └─ method GetFileIdFromSource [L76]
                                                      └─ uses_service IControlledRepository<CachedSourceAccount>
                                                        └─ method Add [L116]
                                                      └─ uses_service IControlledRepository<SourceDivision>
                                                        └─ method ReadQuery [L187]
                                            └─ calls GetTransactions (GET /api/accounting-file/{fileId}/transactions?apiType={*}&endDate={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&password={*}&startDate={*}&username={*}) [L116]
                                              └─ target_service DataGet.Api (see previous expansion)
                                            └─ calls GetWages (GET /api/accounting-file/{fileId}/wages?apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}) [L189]
                                              └─ target_service DataGet.Api (see previous expansion)
                                            └─ calls GetWages (GET /api/accounting-file/{fileId}/wages?apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}) [L190]
                                              └─ target_service DataGet.Api (see previous expansion)
                                            └─ calls GetWages (GET /api/accounting-file/{fileId}/wages?apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}) [L191]
                                              └─ target_service DataGet.Api (see previous expansion)
                                            └─ calls GetWages (GET /api/accounting-file/{fileId}/wages?apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}) [L192]
                                              └─ target_service DataGet.Api (see previous expansion)
                                            └─ calls GetWages (GET /api/accounting-file/{fileId}/wages?apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}) [L193]
                                              └─ target_service DataGet.Api (see previous expansion)
                                            └─ calls PostJournal (POST /api/accounting-file/{fileId}/journal?apiType={*}, target=DataGet.Api, query=apiType={*}) [L127]
                                              └─ target_service DataGet.Api (see previous expansion)
                                            └─ calls Disconnect (DELETE /api/connection/{apiType}/disconnect?fileId={*}, target=DataGet.Api, query=fileId={*}) [L459]
                                              └─ target_service DataGet.Api (see previous expansion)
                                            └─ calls DisconnectFile (DELETE /api/connection/{apiType}/disconnect-file?fileId={*}, target=DataGet.Api, query=fileId={*}) [L503]
                                              └─ target_service DataGet.Api (see previous expansion)
                                            └─ calls GetAccountingFiles (GET /api/connection/{apiType}/accounting-files, target=DataGet.Api) [L52]
                                              └─ target_service DataGet.Api (see previous expansion)
                                            └─ calls GetAuthorisationUrl (GET /api/connection/{apiType}/authorisation-url?callbackUrl={*}, target=DataGet.Api, query=callbackUrl={*}) [L449]
                                              └─ target_service DataGet.Api (see previous expansion)
                                            └─ calls StoreExistingFileTokens (POST /api/connection/{apiType}/file-token?callbackUrl={*}, target=DataGet.Api, query=callbackUrl={*}) [L493]
                                              └─ target_service DataGet.Api (see previous expansion)
                                            └─ calls StoreExistingToken (POST /api/connection/{apiType}/token?callbackUrl={*}, target=DataGet.Api, query=callbackUrl={*}) [L483]
                                              └─ target_service DataGet.Api (see previous expansion)
                                            └─ calls TestConnection (GET /api/connection/{apiType}/test-connection?fileId={*}&password={*}&username={*}, target=DataGet.Api, query=fileId={*}&password={*}&username={*}) [L472]
                                              └─ target_service DataGet.Api (see previous expansion)
                                            └─ calls GetActivityStatementsDetail (GET /api/gov-link/activity-statements/detail?clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}) [L214]
                                              └─ target_service DataGet.Api (see previous expansion)
                                            └─ calls GetActivityStatementSummary (GET /api/gov-link/activity-statements/summary?clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}) [L231]
                                              └─ target_service DataGet.Api (see previous expansion)
                                            └─ calls GetTransactionDetail (GET /api/gov-link/client-accounts/detail?clientABN={*}&clientABNBranch={*}&clientAccountType={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientAccountType={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}) [L250]
                                              └─ target_service DataGet.Api (see previous expansion)
                                            └─ calls GetTransactionSummary (GET /api/gov-link/client-accounts/summary?clientABN={*}&clientABNBranch={*}&clientAccountType={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientAccountType={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}) [L269]
                                              └─ target_service DataGet.Api (see previous expansion)
                                            └─ calls GetProfileComparison (GET /api/gov-link/individual-income-tax-returns/profile-compare?clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&targetFinancialYear={*}&taxAgentABN={*}&taxAgentNumber={*}, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&targetFinancialYear={*}&taxAgentABN={*}&taxAgentNumber={*}) [L325]
                                              └─ target_service DataGet.Api (see previous expansion)
                                            └─ calls GetReportSummary (GET /api/gov-link/individual-income-tax-returns/summary?clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&targetFinancialYear={*}&taxAgentABN={*}&taxAgentNumber={*}, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&targetFinancialYear={*}&taxAgentABN={*}&taxAgentNumber={*}) [L307]
                                              └─ target_service DataGet.Api (see previous expansion)
                                            └─ calls GetFraudHeaderFeedback (GET /api/hmrc/feedback?apiResource={*}, target=DataGet.Api, query=apiResource={*}) [L387]
                                              └─ target_service DataGet.Api (see previous expansion)
                                            └─ calls GetVatObligations (POST /api/hmrc/vat/obligations?endDate={*}&startDate={*}&status={*}&vrn={*}, target=DataGet.Api, query=endDate={*}&startDate={*}&status={*}&vrn={*}) [L358]
                                              └─ target_service DataGet.Api (see previous expansion)
                                            └─ calls GetVatPackage (POST /api/hmrc/vat/package?endDate={*}&startDate={*}&vrn={*}, target=DataGet.Api, query=endDate={*}&startDate={*}&vrn={*}) [L343]
                                              └─ target_service DataGet.Api (see previous expansion)
                                            └─ calls SubmitVatReturn (POST /api/hmrc/vat/submit, target=DataGet.Api) [L367]
                                              └─ target_service DataGet.Api (see previous expansion)
                                            └─ calls ValidateFraudHeaders (POST /api/hmrc/validate, target=DataGet.Api) [L377]
                                              └─ target_service DataGet.Api (see previous expansion)
                                            └─ calls GetFraudHeaderFeedback (GET /api/hmrc/feedback?apiResource={*}, target=DataGet.Api, query=apiResource={*}) [L387]
                                              └─ target_service DataGet.Api (see previous expansion)
                                            └─ calls GetVatObligations (POST /api/hmrc/vat/obligations?endDate={*}&startDate={*}&status={*}&vrn={*}, target=DataGet.Api, query=endDate={*}&startDate={*}&status={*}&vrn={*}) [L358]
                                              └─ target_service DataGet.Api (see previous expansion)
                                            └─ calls SubmitVatReturn (POST /api/hmrc/vat/submit, target=DataGet.Api) [L367]
                                              └─ target_service DataGet.Api (see previous expansion)
                                            └─ calls ValidateFraudHeaders (POST /api/hmrc/validate, target=DataGet.Api) [L377]
                                              └─ target_service DataGet.Api (see previous expansion)
                                          └─ uses_service DataGetClient
                                            └─ method GetDebtorsAsync [L52]
                                          └─ uses_service DatagetFileIdService
                                            └─ method GetFileIdFromSource [L40]
                                          └─ uses_service IControlledRepository<SourceAccount>
                                            └─ method ReadQuery [L45]
                                      └─ returns Debtors [L123]
                                └─ calls GetMovements (GET /api/accounting-file/{fileId}/movement-journals?apiType={*}&endDate={*}&importFrequency={*}&importMovementOnly={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&importFrequency={*}&importMovementOnly={*}&password={*}&startDate={*}&username={*}) [L93]
                                  └─ target_service DataGet.Api (see previous expansion)
                                └─ calls GetTransactions (GET /api/accounting-file/{fileId}/transactions?apiType={*}&endDate={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&password={*}&startDate={*}&username={*}) [L116]
                                  └─ target_service DataGet.Api (see previous expansion)
                                └─ calls GetWages (GET /api/accounting-file/{fileId}/wages?apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}) [L189]
                                  └─ target_service DataGet.Api (see previous expansion)
                                └─ calls GetWages (GET /api/accounting-file/{fileId}/wages?apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}) [L190]
                                  └─ target_service DataGet.Api (see previous expansion)
                                └─ calls GetWages (GET /api/accounting-file/{fileId}/wages?apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}) [L191]
                                  └─ target_service DataGet.Api (see previous expansion)
                                └─ calls GetWages (GET /api/accounting-file/{fileId}/wages?apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}) [L192]
                                  └─ target_service DataGet.Api (see previous expansion)
                                └─ calls GetWages (GET /api/accounting-file/{fileId}/wages?apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}) [L193]
                                  └─ target_service DataGet.Api (see previous expansion)
                                └─ calls PostJournal (POST /api/accounting-file/{fileId}/journal?apiType={*}, target=DataGet.Api, query=apiType={*}) [L127]
                                  └─ target_service DataGet.Api (see previous expansion)
                                └─ calls Disconnect (DELETE /api/connection/{apiType}/disconnect?fileId={*}, target=DataGet.Api, query=fileId={*}) [L459]
                                  └─ target_service DataGet.Api (see previous expansion)
                                └─ calls DisconnectFile (DELETE /api/connection/{apiType}/disconnect-file?fileId={*}, target=DataGet.Api, query=fileId={*}) [L503]
                                  └─ target_service DataGet.Api (see previous expansion)
                                └─ calls GetAccountingFiles (GET /api/connection/{apiType}/accounting-files, target=DataGet.Api) [L52]
                                  └─ target_service DataGet.Api (see previous expansion)
                                └─ calls GetAuthorisationUrl (GET /api/connection/{apiType}/authorisation-url?callbackUrl={*}, target=DataGet.Api, query=callbackUrl={*}) [L449]
                                  └─ target_service DataGet.Api (see previous expansion)
                                └─ calls StoreExistingFileTokens (POST /api/connection/{apiType}/file-token?callbackUrl={*}, target=DataGet.Api, query=callbackUrl={*}) [L493]
                                  └─ target_service DataGet.Api (see previous expansion)
                                └─ calls StoreExistingToken (POST /api/connection/{apiType}/token?callbackUrl={*}, target=DataGet.Api, query=callbackUrl={*}) [L483]
                                  └─ target_service DataGet.Api (see previous expansion)
                                └─ calls TestConnection (GET /api/connection/{apiType}/test-connection?fileId={*}&password={*}&username={*}, target=DataGet.Api, query=fileId={*}&password={*}&username={*}) [L472]
                                  └─ target_service DataGet.Api (see previous expansion)
                                └─ calls GetActivityStatementsDetail (GET /api/gov-link/activity-statements/detail?clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}) [L214]
                                  └─ target_service DataGet.Api (see previous expansion)
                                └─ calls GetActivityStatementSummary (GET /api/gov-link/activity-statements/summary?clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}) [L231]
                                  └─ target_service DataGet.Api (see previous expansion)
                                └─ calls GetTransactionDetail (GET /api/gov-link/client-accounts/detail?clientABN={*}&clientABNBranch={*}&clientAccountType={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientAccountType={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}) [L250]
                                  └─ target_service DataGet.Api (see previous expansion)
                                └─ calls GetTransactionSummary (GET /api/gov-link/client-accounts/summary?clientABN={*}&clientABNBranch={*}&clientAccountType={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientAccountType={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}) [L269]
                                  └─ target_service DataGet.Api (see previous expansion)
                                └─ calls GetProfileComparison (GET /api/gov-link/individual-income-tax-returns/profile-compare?clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&targetFinancialYear={*}&taxAgentABN={*}&taxAgentNumber={*}, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&targetFinancialYear={*}&taxAgentABN={*}&taxAgentNumber={*}) [L325]
                                  └─ target_service DataGet.Api (see previous expansion)
                                └─ calls GetReportSummary (GET /api/gov-link/individual-income-tax-returns/summary?clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&targetFinancialYear={*}&taxAgentABN={*}&taxAgentNumber={*}, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&targetFinancialYear={*}&taxAgentABN={*}&taxAgentNumber={*}) [L307]
                                  └─ target_service DataGet.Api (see previous expansion)
                                └─ calls GetFraudHeaderFeedback (GET /api/hmrc/feedback?apiResource={*}, target=DataGet.Api, query=apiResource={*}) [L387]
                                  └─ target_service DataGet.Api (see previous expansion)
                                └─ calls GetVatObligations (POST /api/hmrc/vat/obligations?endDate={*}&startDate={*}&status={*}&vrn={*}, target=DataGet.Api, query=endDate={*}&startDate={*}&status={*}&vrn={*}) [L358]
                                  └─ target_service DataGet.Api (see previous expansion)
                                └─ calls GetVatPackage (POST /api/hmrc/vat/package?endDate={*}&startDate={*}&vrn={*}, target=DataGet.Api, query=endDate={*}&startDate={*}&vrn={*}) [L343]
                                  └─ target_service DataGet.Api (see previous expansion)
                                └─ calls SubmitVatReturn (POST /api/hmrc/vat/submit, target=DataGet.Api) [L367]
                                  └─ target_service DataGet.Api (see previous expansion)
                                └─ calls ValidateFraudHeaders (POST /api/hmrc/validate, target=DataGet.Api) [L377]
                                  └─ target_service DataGet.Api (see previous expansion)
                                └─ calls GetFraudHeaderFeedback (GET /api/hmrc/feedback?apiResource={*}, target=DataGet.Api, query=apiResource={*}) [L387]
                                  └─ target_service DataGet.Api (see previous expansion)
                                └─ calls GetVatObligations (POST /api/hmrc/vat/obligations?endDate={*}&startDate={*}&status={*}&vrn={*}, target=DataGet.Api, query=endDate={*}&startDate={*}&status={*}&vrn={*}) [L358]
                                  └─ target_service DataGet.Api (see previous expansion)
                                └─ calls SubmitVatReturn (POST /api/hmrc/vat/submit, target=DataGet.Api) [L367]
                                  └─ target_service DataGet.Api (see previous expansion)
                                └─ calls ValidateFraudHeaders (POST /api/hmrc/validate, target=DataGet.Api) [L377]
                                  └─ target_service DataGet.Api (see previous expansion)
                              └─ uses_service DataGetClient
                                └─ method GetCreditorsAsync [L53]
                              └─ uses_service DatagetFileIdService
                                └─ method GetFileIdFromSource [L40]
                              └─ uses_service IControlledRepository<SourceAccount>
                                └─ method ReadQuery [L46]
                          └─ returns Creditors [L113]
                    └─ calls GetDebtors (GET /api/accounting-file/{fileId}/debtors?apiType={*}&endDate={*}&password={*}&sourceAccountSystemId={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&password={*}&sourceAccountSystemId={*}&username={*}) [L171]
                      └─ target_service DataGet.Api (see previous expansion)
                    └─ calls GetMovements (GET /api/accounting-file/{fileId}/movement-journals?apiType={*}&endDate={*}&importFrequency={*}&importMovementOnly={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&importFrequency={*}&importMovementOnly={*}&password={*}&startDate={*}&username={*}) [L93]
                      └─ target_service DataGet.Api (see previous expansion)
                    └─ calls GetTransactions (GET /api/accounting-file/{fileId}/transactions?apiType={*}&endDate={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&password={*}&startDate={*}&username={*}) [L116]
                      └─ target_service DataGet.Api (see previous expansion)
                    └─ calls GetWages (GET /api/accounting-file/{fileId}/wages?apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}) [L189]
                      └─ target_service DataGet.Api (see previous expansion)
                    └─ calls GetWages (GET /api/accounting-file/{fileId}/wages?apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}) [L190]
                      └─ target_service DataGet.Api (see previous expansion)
                    └─ calls GetWages (GET /api/accounting-file/{fileId}/wages?apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}) [L191]
                      └─ target_service DataGet.Api (see previous expansion)
                    └─ calls GetWages (GET /api/accounting-file/{fileId}/wages?apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}) [L192]
                      └─ target_service DataGet.Api (see previous expansion)
                    └─ calls GetWages (GET /api/accounting-file/{fileId}/wages?apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}) [L193]
                      └─ target_service DataGet.Api (see previous expansion)
                    └─ calls PostJournal (POST /api/accounting-file/{fileId}/journal?apiType={*}, target=DataGet.Api, query=apiType={*}) [L127]
                      └─ target_service DataGet.Api (see previous expansion)
                    └─ calls Disconnect (DELETE /api/connection/{apiType}/disconnect?fileId={*}, target=DataGet.Api, query=fileId={*}) [L459]
                      └─ target_service DataGet.Api (see previous expansion)
                    └─ calls DisconnectFile (DELETE /api/connection/{apiType}/disconnect-file?fileId={*}, target=DataGet.Api, query=fileId={*}) [L503]
                      └─ target_service DataGet.Api (see previous expansion)
                    └─ calls GetAccountingFiles (GET /api/connection/{apiType}/accounting-files, target=DataGet.Api) [L52]
                      └─ target_service DataGet.Api (see previous expansion)
                    └─ calls GetAuthorisationUrl (GET /api/connection/{apiType}/authorisation-url?callbackUrl={*}, target=DataGet.Api, query=callbackUrl={*}) [L449]
                      └─ target_service DataGet.Api (see previous expansion)
                    └─ calls StoreExistingFileTokens (POST /api/connection/{apiType}/file-token?callbackUrl={*}, target=DataGet.Api, query=callbackUrl={*}) [L493]
                      └─ target_service DataGet.Api (see previous expansion)
                    └─ calls StoreExistingToken (POST /api/connection/{apiType}/token?callbackUrl={*}, target=DataGet.Api, query=callbackUrl={*}) [L483]
                      └─ target_service DataGet.Api (see previous expansion)
                    └─ calls TestConnection (GET /api/connection/{apiType}/test-connection?fileId={*}&password={*}&username={*}, target=DataGet.Api, query=fileId={*}&password={*}&username={*}) [L472]
                      └─ target_service DataGet.Api (see previous expansion)
                    └─ calls GetActivityStatementsDetail (GET /api/gov-link/activity-statements/detail?clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}) [L214]
                      └─ target_service DataGet.Api (see previous expansion)
                    └─ calls GetActivityStatementSummary (GET /api/gov-link/activity-statements/summary?clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}) [L231]
                      └─ target_service DataGet.Api (see previous expansion)
                    └─ calls GetTransactionDetail (GET /api/gov-link/client-accounts/detail?clientABN={*}&clientABNBranch={*}&clientAccountType={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientAccountType={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}) [L250]
                      └─ target_service DataGet.Api (see previous expansion)
                    └─ calls GetTransactionSummary (GET /api/gov-link/client-accounts/summary?clientABN={*}&clientABNBranch={*}&clientAccountType={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientAccountType={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}) [L269]
                      └─ target_service DataGet.Api (see previous expansion)
                    └─ calls GetProfileComparison (GET /api/gov-link/individual-income-tax-returns/profile-compare?clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&targetFinancialYear={*}&taxAgentABN={*}&taxAgentNumber={*}, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&targetFinancialYear={*}&taxAgentABN={*}&taxAgentNumber={*}) [L325]
                      └─ target_service DataGet.Api (see previous expansion)
                    └─ calls GetReportSummary (GET /api/gov-link/individual-income-tax-returns/summary?clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&targetFinancialYear={*}&taxAgentABN={*}&taxAgentNumber={*}, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&targetFinancialYear={*}&taxAgentABN={*}&taxAgentNumber={*}) [L307]
                      └─ target_service DataGet.Api (see previous expansion)
                    └─ calls GetFraudHeaderFeedback (GET /api/hmrc/feedback?apiResource={*}, target=DataGet.Api, query=apiResource={*}) [L387]
                      └─ target_service DataGet.Api (see previous expansion)
                    └─ calls GetVatObligations (POST /api/hmrc/vat/obligations?endDate={*}&startDate={*}&status={*}&vrn={*}, target=DataGet.Api, query=endDate={*}&startDate={*}&status={*}&vrn={*}) [L358]
                      └─ target_service DataGet.Api (see previous expansion)
                    └─ calls GetVatPackage (POST /api/hmrc/vat/package?endDate={*}&startDate={*}&vrn={*}, target=DataGet.Api, query=endDate={*}&startDate={*}&vrn={*}) [L343]
                      └─ target_service DataGet.Api (see previous expansion)
                    └─ calls SubmitVatReturn (POST /api/hmrc/vat/submit, target=DataGet.Api) [L367]
                      └─ target_service DataGet.Api (see previous expansion)
                    └─ calls ValidateFraudHeaders (POST /api/hmrc/validate, target=DataGet.Api) [L377]
                      └─ target_service DataGet.Api (see previous expansion)
                    └─ calls GetFraudHeaderFeedback (GET /api/hmrc/feedback?apiResource={*}, target=DataGet.Api, query=apiResource={*}) [L387]
                      └─ target_service DataGet.Api (see previous expansion)
                    └─ calls GetVatObligations (POST /api/hmrc/vat/obligations?endDate={*}&startDate={*}&status={*}&vrn={*}, target=DataGet.Api, query=endDate={*}&startDate={*}&status={*}&vrn={*}) [L358]
                      └─ target_service DataGet.Api (see previous expansion)
                    └─ calls SubmitVatReturn (POST /api/hmrc/vat/submit, target=DataGet.Api) [L367]
                      └─ target_service DataGet.Api (see previous expansion)
                    └─ calls ValidateFraudHeaders (POST /api/hmrc/validate, target=DataGet.Api) [L377]
                      └─ target_service DataGet.Api (see previous expansion)
                  └─ uses_service DataGetClient
                    └─ method GetAccountsAsync [L40]
                  └─ uses_service DatagetFileIdService
                    └─ method GetFileIdFromSource [L38]
        └─ calls GetCreditors (GET /api/accounting-file/{fileId}/creditors?apiType={*}&endDate={*}&password={*}&sourceAccountSystemId={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&password={*}&sourceAccountSystemId={*}&username={*}) [L156]
          └─ target_service DataGet.Api (see previous expansion)
        └─ calls GetDebtors (GET /api/accounting-file/{fileId}/debtors?apiType={*}&endDate={*}&password={*}&sourceAccountSystemId={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&password={*}&sourceAccountSystemId={*}&username={*}) [L171]
          └─ target_service DataGet.Api (see previous expansion)
        └─ calls GetMovements (GET /api/accounting-file/{fileId}/movement-journals?apiType={*}&endDate={*}&importFrequency={*}&importMovementOnly={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&importFrequency={*}&importMovementOnly={*}&password={*}&startDate={*}&username={*}) [L93]
          └─ target_service DataGet.Api (see previous expansion)
        └─ calls GetTransactions (GET /api/accounting-file/{fileId}/transactions?apiType={*}&endDate={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&password={*}&startDate={*}&username={*}) [L116]
          └─ target_service DataGet.Api (see previous expansion)
        └─ calls GetWages (GET /api/accounting-file/{fileId}/wages?apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}) [L189]
          └─ target_service DataGet.Api (see previous expansion)
        └─ calls GetWages (GET /api/accounting-file/{fileId}/wages?apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}) [L190]
          └─ target_service DataGet.Api (see previous expansion)
        └─ calls GetWages (GET /api/accounting-file/{fileId}/wages?apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}) [L191]
          └─ target_service DataGet.Api (see previous expansion)
        └─ calls GetWages (GET /api/accounting-file/{fileId}/wages?apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}) [L192]
          └─ target_service DataGet.Api (see previous expansion)
        └─ calls GetWages (GET /api/accounting-file/{fileId}/wages?apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}, target=DataGet.Api, query=apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}) [L193]
          └─ target_service DataGet.Api (see previous expansion)
        └─ calls PostJournal (POST /api/accounting-file/{fileId}/journal?apiType={*}, target=DataGet.Api, query=apiType={*}) [L127]
          └─ target_service DataGet.Api (see previous expansion)
        └─ calls Disconnect (DELETE /api/connection/{apiType}/disconnect?fileId={*}, target=DataGet.Api, query=fileId={*}) [L459]
          └─ target_service DataGet.Api (see previous expansion)
        └─ calls DisconnectFile (DELETE /api/connection/{apiType}/disconnect-file?fileId={*}, target=DataGet.Api, query=fileId={*}) [L503]
          └─ target_service DataGet.Api (see previous expansion)
        └─ calls GetAccountingFiles (GET /api/connection/{apiType}/accounting-files, target=DataGet.Api) [L52]
          └─ target_service DataGet.Api (see previous expansion)
        └─ calls GetAuthorisationUrl (GET /api/connection/{apiType}/authorisation-url?callbackUrl={*}, target=DataGet.Api, query=callbackUrl={*}) [L449]
          └─ target_service DataGet.Api (see previous expansion)
        └─ calls StoreExistingFileTokens (POST /api/connection/{apiType}/file-token?callbackUrl={*}, target=DataGet.Api, query=callbackUrl={*}) [L493]
          └─ target_service DataGet.Api (see previous expansion)
        └─ calls StoreExistingToken (POST /api/connection/{apiType}/token?callbackUrl={*}, target=DataGet.Api, query=callbackUrl={*}) [L483]
          └─ target_service DataGet.Api (see previous expansion)
        └─ calls TestConnection (GET /api/connection/{apiType}/test-connection?fileId={*}&password={*}&username={*}, target=DataGet.Api, query=fileId={*}&password={*}&username={*}) [L472]
          └─ target_service DataGet.Api (see previous expansion)
        └─ calls GetActivityStatementsDetail (GET /api/gov-link/activity-statements/detail?clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}) [L214]
          └─ target_service DataGet.Api (see previous expansion)
        └─ calls GetActivityStatementSummary (GET /api/gov-link/activity-statements/summary?clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}) [L231]
          └─ target_service DataGet.Api (see previous expansion)
        └─ calls GetTransactionDetail (GET /api/gov-link/client-accounts/detail?clientABN={*}&clientABNBranch={*}&clientAccountType={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientAccountType={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}) [L250]
          └─ target_service DataGet.Api (see previous expansion)
        └─ calls GetTransactionSummary (GET /api/gov-link/client-accounts/summary?clientABN={*}&clientABNBranch={*}&clientAccountType={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientAccountType={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}) [L269]
          └─ target_service DataGet.Api (see previous expansion)
        └─ calls GetProfileComparison (GET /api/gov-link/individual-income-tax-returns/profile-compare?clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&targetFinancialYear={*}&taxAgentABN={*}&taxAgentNumber={*}, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&targetFinancialYear={*}&taxAgentABN={*}&taxAgentNumber={*}) [L325]
          └─ target_service DataGet.Api (see previous expansion)
        └─ calls GetReportSummary (GET /api/gov-link/individual-income-tax-returns/summary?clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&targetFinancialYear={*}&taxAgentABN={*}&taxAgentNumber={*}, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&targetFinancialYear={*}&taxAgentABN={*}&taxAgentNumber={*}) [L307]
          └─ target_service DataGet.Api (see previous expansion)
        └─ calls GetFraudHeaderFeedback (GET /api/hmrc/feedback?apiResource={*}, target=DataGet.Api, query=apiResource={*}) [L387]
          └─ target_service DataGet.Api (see previous expansion)
        └─ calls GetVatObligations (POST /api/hmrc/vat/obligations?endDate={*}&startDate={*}&status={*}&vrn={*}, target=DataGet.Api, query=endDate={*}&startDate={*}&status={*}&vrn={*}) [L358]
          └─ target_service DataGet.Api (see previous expansion)
        └─ calls GetVatPackage (POST /api/hmrc/vat/package?endDate={*}&startDate={*}&vrn={*}, target=DataGet.Api, query=endDate={*}&startDate={*}&vrn={*}) [L343]
          └─ target_service DataGet.Api (see previous expansion)
        └─ calls SubmitVatReturn (POST /api/hmrc/vat/submit, target=DataGet.Api) [L367]
          └─ target_service DataGet.Api (see previous expansion)
        └─ calls ValidateFraudHeaders (POST /api/hmrc/validate, target=DataGet.Api) [L377]
          └─ target_service DataGet.Api (see previous expansion)
        └─ calls GetFraudHeaderFeedback (GET /api/hmrc/feedback?apiResource={*}, target=DataGet.Api, query=apiResource={*}) [L387]
          └─ target_service DataGet.Api (see previous expansion)
        └─ calls GetVatObligations (POST /api/hmrc/vat/obligations?endDate={*}&startDate={*}&status={*}&vrn={*}, target=DataGet.Api, query=endDate={*}&startDate={*}&status={*}&vrn={*}) [L358]
          └─ target_service DataGet.Api (see previous expansion)
        └─ calls SubmitVatReturn (POST /api/hmrc/vat/submit, target=DataGet.Api) [L367]
          └─ target_service DataGet.Api (see previous expansion)
        └─ calls ValidateFraudHeaders (POST /api/hmrc/validate, target=DataGet.Api) [L377]
          └─ target_service DataGet.Api (see previous expansion)
      └─ uses_service DataGetClient
        └─ method GetClientAccountDetailsAsync [L64]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L55]

