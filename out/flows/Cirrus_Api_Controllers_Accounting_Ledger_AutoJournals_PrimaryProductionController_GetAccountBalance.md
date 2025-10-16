[web] GET /api/accounting/ledger/auto-journals/primary-production/{datasetId}/{tradingAccountId}/account-balance/{sourceAccountId}  (Cirrus.Api.Controllers.Accounting.Ledger.AutoJournals.PrimaryProductionController.GetAccountBalance)  [L57–L62] status=200 [auth=user]
  └─ sends_request GetPrimaryProductionRowsDtoQuery -> GetPrimaryProductionRowsDtoQueryHandler [L60]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Ledger.GetPrimaryProductionRowsDtoQueryHandler.Handle [L47–L147]
      └─ maps_to PrimaryProductionConfigDto [L137]
        └─ automapper.registration CirrusMappingProfile (PrimaryProductionConfig->PrimaryProductionConfigDto) [L501]
      └─ maps_to SourceAccountForPrimaryProductionDto [L79]
        └─ automapper.registration CirrusMappingProfile (SourceAccount->SourceAccountForPrimaryProductionDto) [L276]
        └─ converts_to SourceAccountInJournalModel [L280]
      └─ uses_service IControlledRepository<PrimaryProductionConfig> (Scoped (inferred))
        └─ method ReadQuery [L137]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.AutoJournals.PrimaryProductionConfigRepository.ReadQuery
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L131]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
      └─ uses_service IControlledRepository<Dataset> (Scoped (inferred))
        └─ method ReadQuery [L126]
          └─ implementation Cirrus.Data.Repository.Accounting.DatasetRepository.ReadQuery
      └─ uses_service GetTrialBalanceForDatesQuery
        └─ method Execute [L88]
          └─ resolves_request Workpapers.Next.ApplicationService.Queries.LedgerReports.GetTrialBalanceForDatesQuery.Execute
            └─ handled_by Workpapers.Next.ApplicationService.Queries.LedgerReports.GetTrialBalanceForDatesQueryHandler.Handle [L45–L251]
              └─ calls SourceAccountRepository.ReadQuery [L151]
      └─ uses_service IControlledRepository<SourceAccount> (Scoped (inferred))
        └─ method ReadQuery [L79]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.SourceAccountRepository.ReadQuery
  └─ impact_summary
    └─ requests 2
      └─ GetPrimaryProductionRowsDtoQuery
      └─ GetTrialBalanceForDatesQuery
    └─ handlers 2
      └─ GetPrimaryProductionRowsDtoQueryHandler
      └─ GetTrialBalanceForDatesQueryHandler
    └─ mappings 2
      └─ PrimaryProductionConfigDto
      └─ SourceAccountForPrimaryProductionDto

