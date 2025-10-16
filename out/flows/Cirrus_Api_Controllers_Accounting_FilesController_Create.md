[web] POST /api/accounting/files/  (Cirrus.Api.Controllers.Accounting.FilesController.Create)  [L200–L210] status=201 [auth=user]
  └─ calls FileRepository.Add [L208]
  └─ calls EntityRepository.WriteQuery [L205]
  └─ insert File [L208]
    └─ reads_from Files
  └─ write Entity [L205]
  └─ sends_request GetDefaultStandardChartIdQuery -> GetDefaultStandardChartIdQueryHandler [L203]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetDefaultStandardChartIdQueryHandler.Handle [L24–L48]
      └─ uses_service IControlledRepository<StandardChart> (Scoped (inferred))
        └─ method ReadQuery [L36]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.StandardChartRepository.ReadQuery
  └─ impact_summary
    └─ entities 2 (writes=2, reads=0)
      └─ Entity writes=1 reads=0
      └─ File writes=1 reads=0
    └─ requests 1
      └─ GetDefaultStandardChartIdQuery
    └─ handlers 1
      └─ GetDefaultStandardChartIdQueryHandler

