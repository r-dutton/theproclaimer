[web] POST /api/accounting/files/  (Cirrus.Api.Controllers.Accounting.FilesController.Create)  [L200–L210] [auth=user]
  └─ calls FileRepository.Add [L208]
  └─ calls EntityRepository.WriteQuery [L205]
  └─ writes_to File [L208]
    └─ reads_from Files
  └─ writes_to Entity [L205]
  └─ uses_service IControlledRepository<Entity>
    └─ method WriteQuery [L205]
  └─ uses_service IControlledRepository<File>
    └─ method Add [L208]
  └─ sends_request GetDefaultStandardChartIdQuery [L203]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetDefaultStandardChartIdQueryHandler.Handle [L24–L48]
      └─ uses_service IControlledRepository<StandardChart>
        └─ method ReadQuery [L36]

