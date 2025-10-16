[web] POST /api/accounting/files/  (Cirrus.Api.Controllers.Accounting.FilesController.Create)  [L200–L210] status=201 [auth=user]
  └─ calls FileRepository.Add [L208]
  └─ calls EntityRepository.WriteQuery [L205]
  └─ insert File [L208]
    └─ reads_from Files
  └─ write Entity [L205]
  └─ uses_service IControlledRepository<Entity>
    └─ method WriteQuery [L205]
      └─ ... (no implementation details available)
  └─ uses_service IControlledRepository<File>
    └─ method Add [L208]
      └─ ... (no implementation details available)
  └─ sends_request GetDefaultStandardChartIdQuery [L203]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetDefaultStandardChartIdQueryHandler.Handle [L24–L48]
      └─ uses_service IControlledRepository<StandardChart>
        └─ method ReadQuery [L36]
          └─ ... (no implementation details available)

